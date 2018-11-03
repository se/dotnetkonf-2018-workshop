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
    stage("Send a message") {
        echo "Everything is done."
    }
}