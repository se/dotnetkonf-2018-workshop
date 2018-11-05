// You can get your url by messaging with https://t.me/monopushbot
// just type /new Your-Channel-Name
// Example /new Jenkins CI/CD
// You can also add monopush to a group as a user and send push notifications
// to your groups.
def monoPushUrl = 'https://webhook.monopush.io/telegram/XXXXXXXXXXXXX'
def registryUrl = 'registry.gitlab.com'
def imageName = 'registry.gitlab.com/selcukermaya/dotnetkonf'
def buildNumber = env.BUILD_NUMBER.toString()

node {
    stage("Waiting for Approvement"){
        timeout(time: 30, unit: 'SECONDS') {
            // Register for Webhook
            def hook = registerWebhook()
            // Get hook url
            def hookUrl = hook.getURL()
            echo "Waiting for POST to ${hookUrl}"

            def message = """
            {
                "message": "Waiting approvement for build ${buildNumber}.",
                "action": "Do you want me proceed to build?",
                "buttons": [
                    {
                        "id": "APPROVE",
                        "text": "Yes, Please",
                        "webhook": "${hookUrl}",
                        "success_message": "Done. I did it.",
                        "error_message": "Something is wrong with your request."
                    },
                    {
                        "id": "CANCEL",
                        "text": "No",
                        "webhook": "${hookUrl}",
                        "success_message": "OK, I have canceled it.",
                        "error_message": "Something is wrong with your request."
                    }
                ]
            }
            """
            httpRequest url: monoPushUrl, httpMode: 'POST', requestBody: message, contentType: 'APPLICATION_JSON'

            def data = waitForWebhook hook
            def result = data.toString()
            echo "Webhook called with data: ${result}"
            if(result.contains("APPROVE")) {
                echo "OK, We will continue to proceed."
            } else {
                currentBuild.result = "ABORTED";
                echo "OK, I aborted it"
                error('User triggered to skip')
            }
        }
    }
    stage("SCM") {
        git credentialsId: 'gitlab', url: 'git@gitlab.com:selcukermaya/dotnetkonf.git'
    }
    stage("Sonar Begin") {
        withCredentials([string(credentialsId: 'sonar-token', variable: 'DOTNETKONF_SONAR_TOKEN')]) {
            sh 'dotnet sonarscanner begin /k:"dotnetkonf-test" /d:sonar.host.url="https://cq-test.monofor.com" /d:sonar.login="$DOTNETKONF_SONAR_TOKEN" /d:sonar.cs.opencover.reportsPaths=".sonarqube/coverage/api.opencover.xml"' 
        }
    }
    stage("Build"){
        sh 'dotnet build'
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
            sh "docker build -t ${imageName} ."
        }
        sh "docker tag ${imageName}:latest ${imageName}:${buildNumber}"
    }
    stage("Docker Push"){
        withCredentials([string(credentialsId: 'gitlab-token', variable: 'GITLAB_TOKEN')]) {
            sh "docker login ${registryUrl} -u selcukermaya -p $GITLAB_TOKEN"
            sh "docker push ${imageName}:latest"
            sh "docker push ${imageName}:${buildNumber}"
        }
    }
    stage("Send Push Notification") {
        def monoPushMessage = """
        {
            "message": "Your Jenkins build ${buildNumber} has been completed. Also I published docker images to ${imageName}"
        }
        """
        httpRequest contentType: 'APPLICATION_JSON', httpMode: 'POST', requestBody: monoPushMessage, url: monoPushUrl
    }
}
