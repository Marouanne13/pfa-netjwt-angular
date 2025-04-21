pipeline {
  agent any

  environment {
    SONAR_TOKEN = 'admin' // Or the token you've configured in local SonarQube (default user is 'admin' if not changed)
    SONAR_HOST_URL = 'http://localhost:9000'
  }

  stages {

    stage('Checkout') {
      steps {
        git(
          url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
          credentialsId: 'jenkins-ssh-deploy',
          branch: 'main'
        )
      }
    }

    stage('Install SonarScanner') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 || true
          '''
        }
      }
    }

    stage('SonarQube: Begin Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            dotnet sonarscanner begin \
              /k:"pfa-netjwt-angular" \
              /d:sonar.login=$SONAR_TOKEN \
              /d:sonar.host.url=$SONAR_HOST_URL \
              /d:sonar.verbose=true
          '''
        }
      }
    }

    stage('Restore') {
      steps {
        sh 'dotnet restore PFA.sln --verbosity minimal'
      }
    }

    stage('Build') {
      steps {
        sh 'dotnet build PFA.sln --no-restore --verbosity minimal'
      }
    }

    stage('Test') {
      steps {
        sh 'dotnet test PFA.sln --no-build --verbosity minimal'
      }
    }

    stage('SonarQube: End Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
        }
      }
    }
  }

  post {
    always {
      echo '📋 Fin de pipeline (succès ou échec).'
    }
    success {
      echo '✅ Build et analyse SonarQube réussis.'
    }
    failure {
      echo '❌ La pipeline a échoué. Vérifiez les logs.'
    }
  }
}
