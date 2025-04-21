pipeline {
  agent any

  environment {
    SONAR_TOKEN = 'admin' // Par défaut pour local SonarQube (ou ton token)
    SONAR_HOST_URL = 'http://localhost:9000'
  }

  stages {

    stage('Lancer SonarQube (Docker)') {
      steps {
        echo '🚀 Démarrage de SonarQube en local (Docker)...'
        sh '''
          docker rm -f sonarqube || true
          docker run -d --name sonarqube -p 9000:9000 sonarqube:lts
          echo "⏳ Attente que SonarQube soit prêt..."
          sleep 30
        '''
      }
    }

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
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 --verbosity quiet || true
          '''
        }
      }
    }

    stage('SonarQube: Begin Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh """
            dotnet sonarscanner begin \
              /k:"pfa-netjwt-angular" \
              /d:sonar.login=$SONAR_TOKEN \
              /d:sonar.host.url=$SONAR_HOST_URL \
              /d:sonar.verbose=true
          """
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
        script {
          withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
            def result = sh(script: 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN', returnStatus: true)
            if (result != 0) {
              echo "⚠️ SonarScanner end a retourné ${result}, mais on continue la pipeline."
            } else {
              echo "✅ SonarScanner end terminé avec succès."
            }
          }
        }
      }
    }
  }

  post {
    always {
      echo '🧹 Nettoyage du conteneur SonarQube...'
      sh 'docker stop sonarqube || true'
    }
    success {
      echo '✅ Build et analyse SonarQube locales réussies !'
    }
    failure {
      echo '❌ Pipeline échouée – vérifiez les étapes ou les logs SonarQube.'
    }
  }
}
