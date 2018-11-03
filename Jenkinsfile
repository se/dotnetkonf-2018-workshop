node {
    stage("Git") {
        git credentialsId: 'gitlab', url: 'git@gitlab.com:selcukermaya/dotnetkonf.git'
    }
    stage("Build") {
        sh "dotnet build"
    }    
    stage("Build") {
        sh "dotnet test ./test/dotnetKonf.Web.Test/dotnetKonf.Web.Test.csproj"
    }    
    stage("Docker Build") {
        def buildNumber = env.BUILD_NUMBER.toString()
        sh "docker build -t registry.gitlab.com/selcukermaya/dotnetkonf ."
        sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf:latest"
        sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf:build-${buildNumber}"
    }
    stage("Docker Push") {
        withCredentials([string(credentialId: "gitlab-token", variable: 'GITLAB_TOKEN')]) {
            sh "docker login registry.gitlab.com -u selcukermaya -p ${GITLAB_TOKEN}"
            sh "docker push registry.gitlab.com/selcukermaya/dotnetkonf:latest"
            sh "docker push registry.gitlab.com/selcukermaya/dotnetkonf:build-${buildNumber}"
        }
    }
}