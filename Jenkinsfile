pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    // Liaison du token "JenkinsCI" via l'ID "sonarcloud-token"
    SONAR_TOKEN = credentials('sonarcloud-token')
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
        // Installation du scanner s'il n'est pas déjà installé
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 --verbosity quiet || true
          '''
        }
      }
    }

    stage('SonarCloud: Begin Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            dotnet sonarscanner begin \
              /k:"Marouanne13_pfa-netjwt-angular" \
              /o:"marouanne13" \
              /d:sonar.host.url=https://sonarcloud.io \
              /d:sonar.login=$SONAR_TOKEN \
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

    stage('SonarCloud: End Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
        }
      }
    }
  }

  post {
    success {
      echo '🎉 Build, tests et analyse SonarCloud réussis !'
    }
    failure {
      echo '❌ Échec de la pipeline – vérifiez les logs Jenkins et le Quality Gate SonarCloud.'
    }
  }
}
