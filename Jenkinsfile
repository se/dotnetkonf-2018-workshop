node {
    stage("Git") {
        git credentialsId: 'gitlab', url: 'git@gitlab.com:selcukermaya/dotnetkonf.git'
    }
    stage("Build") {
        sh "dotnet build"
    }    
    stage("Send a message") {
        echo "Everything is done."
    }
}