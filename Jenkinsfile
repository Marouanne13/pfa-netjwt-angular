pipeline {
  agent any

  stages {
    stage('Checkout') {
      steps {
        git(
          url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
          credentialsId: 'jenkins-rsa',
          branch: 'main'
        )
      }
    }
    stage('Build') {
      steps {
        sh 'dotnet build PFA.sln'
        sh 'npm install && ng build --prod'
      }
    }
    stage('Test') {
      steps {
        sh 'dotnet test'
      }
    }
  }
}
