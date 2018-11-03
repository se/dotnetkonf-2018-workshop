node {
    stage("Inform All") {
        httpRequest url: 'https://webhook.monopush.io/telegram/5bdda0fa87d089000122100f', httpMode: 'POST', requestBody: '{"message":"I started to build your code."}', contentType: 'APPLICATION_JSON'
    }
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
        dir("./src") {
            def buildNumber = env.BUILD_NUMBER.toString()
            sh "docker build -t registry.gitlab.com/selcukermaya/dotnetkonf ."
            sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf:latest"
            sh "docker tag registry.gitlab.com/selcukermaya/dotnetkonf:build-${buildNumber}"
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