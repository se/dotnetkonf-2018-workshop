def monoPushUrl = 'https://webhook.monopush.io/telegram/5bdd1d68ea1b0400014e4e3e'
def registryUrl = 'registry.gitlab.com'
def imageName = 'registry.gitlab.com/selcukermaya/dotnetkonf'
def buildNumber = env.BUILD_NUMBER.toString()

node {
    stage("Git") {
        git credentialsId: 'gitlab', url: 'git@gitlab.com:selcukermaya/dotnetkonf.git'
    }
    stage("Sonar Begin") {
        withCredentials([string(credentialsId: 'sonar-token', variable: 'DOTNETKONF_SONAR_TOKEN')]) {
            sh 'dotnet sonarscanner begin /k:"dotnetkonf-test" /d:sonar.host.url="https://cq-test.monofor.com" /d:sonar.login="$DOTNETKONF_SONAR_TOKEN" /d:sonar.cs.opencover.reportsPaths=".sonarqube/coverage/api.opencover.xml"'
        }
    }
    stage("Build") {
        sh "dotnet build"
    }    
    stage("Test"){
        sh 'dotnet test ./test/dotnetKonf.Web.Test/dotnetKonf.Web.Test.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=../../.sonarqube/coverage/api.opencover.xml'
    }
    stage("Sonar End") {
        withCredentials([string(credentialsId: 'sonar-token', variable: 'DOTNETKONF_SONAR_TOKEN')]) {
            sh 'dotnet sonarscanner end /d:sonar.login="$DOTNETKONF_SONAR_TOKEN"'
        }
    }
    stage("Docker Build") {
        dir("./src") {
            sh "docker build -t registry.gitlab.com/selcukermaya/dotnetkonf ."
            sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf registry.gitlab.com/selcukermaya/dotnetkonf:latest"
            sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf registry.gitlab.com/selcukermaya/dotnetkonf:build-${buildNumber}"
        }
    }
    stage("Docker Push") {
        withCredentials([string(credentialId: "gitlab-token", variable: 'GITLAB_TOKEN')]) {
            sh "docker login registry.gitlab.com -u selcukermaya -p ${GITLAB_TOKEN}"
            sh "docker push registry.gitlab.com/selcukermaya/dotnetkonf:latest"
            sh "docker push registry.gitlab.com/selcukermaya/dotnetkonf:build-${buildNumber}"
        }
    }
}