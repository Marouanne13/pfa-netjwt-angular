pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    // injecte votre token SonarCloud
    SONAR_TOKEN = credentials('sonarcloud-token')
  }

  stages {

    stage('Checkout') {
      steps {
        git(
          url:           'git@github.com:Marouanne13/pfa-netjwt-angular.git',
          credentialsId: 'jenkins-ssh-deploy',
          branch:        'main'
        )
      }
    }

    stage('SonarCloud: begin analysis') {
      steps {
        // On ajoute ~/.dotnet/tools au PATH pour que 'dotnet tool' soit visible
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            # Installe le scanner (ou le met à jour) si besoin
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 --verbosity quiet || true

            # Démarrage de l’analyse SonarCloud
            dotnet sonarscanner begin \
              /k:"Marouanne13_pfa-netjwt-angular" \
              /o:"marouanne13" \
              /d:sonar.login=$SONAR_TOKEN
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

    stage('SonarCloud: end analysis') {
      steps {
        // Même PATH pour que dotnet-sonarscanner reste accessible
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
