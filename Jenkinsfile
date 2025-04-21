pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    SONARQUBE = 'http://172.17.0.1:9000'
  }

  stages {

    stage('Lancer SonarQube (Docker)') {
      steps {
        echo '🚀 Démarrage de SonarQube en local (Docker)...'
        sh '''
          docker rm -f sonarqube || true
          docker run -d --name sonarqube -p 9000:9000 sonarqube:lts
          echo "⏳ Attente de SonarQube (max 90s)..."
          for i in {1..30}; do
            if curl -s http://localhost:9000/api/system/health | grep -q '"status":"GREEN"'; then
              echo "✅ SonarQube est prêt !"
              break
            fi
            echo "⏳ SonarQube pas prêt, retry... [$i]"
            sleep 3
          done
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

    stage('Analyse SonarQube (via container SDK)') {
      steps {
        script {
          withCredentials([string(credentialsId: 'sonarqube-token', variable: 'SONAR_TOKEN')]) {
            sh """
              docker run --rm \
                -v \$(pwd):/app \
                -w /app \
                -e SONAR_TOKEN=\$SONAR_TOKEN \
                mcr.microsoft.com/dotnet/sdk:8.0 \
                sh -c '
                  dotnet tool install --global dotnet-sonarscanner &&
                  export PATH="\$PATH:/root/.dotnet/tools" &&

                  dotnet-sonarscanner begin /k:"pfa-netjwt-angular" /d:sonar.login=\$SONAR_TOKEN /d:sonar.host.url="${SONARQUBE}" &&

                  dotnet restore /app/PFA/PFA.sln &&
                  dotnet build /app/PFA/PFA.sln &&
                  dotnet test /app/PFA/PFA.sln --no-build &&

                  dotnet-sonarscanner end /d:sonar.login=\$SONAR_TOKEN
                '
            """
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
      echo '✅ Pipeline complète réussie avec analyse SonarQube !'
    }
    failure {
      echo '❌ Pipeline échouée – vérifiez les logs du conteneur dotnet ou SonarQube.'
    }
  }
}
