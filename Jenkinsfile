pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    SONARQUBE_URL = 'http://sonarqube:9000'
  }

  stages {

    stage('Préparer le réseau Docker') {
      steps {
        sh 'docker network create sonarnet || true'
      }
    }

    stage('Lancer SonarQube (Docker)') {
      steps {
        echo '🚀 Démarrage de SonarQube dans le réseau Docker personnalisé...'
        sh '''
          docker rm -f sonarqube || true
          docker run -d --name sonarqube --network sonarnet -p 9000:9000 sonarqube:lts

          echo "⏳ Attente que SonarQube soit prêt..."
          for i in {1..30}; do
            if curl -s http://localhost:9000/api/system/health | grep -q '"status":"GREEN"'; then
              echo "✅ SonarQube prêt !"
              break
            fi
            echo "⏳ Attente... [$i]"
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

    stage('Analyse SonarQube (via SDK .NET)') {
      steps {
        script {
          withCredentials([string(credentialsId: 'sonarcloud-token', variable: 'SONAR_TOKEN')]) {
            sh """
              docker run --rm \
                --network sonarnet \
                -v \$(pwd):/app \
                -w /app \
                -e SONAR_TOKEN=\$SONAR_TOKEN \
                mcr.microsoft.com/dotnet/sdk:8.0 \
                sh -c '
                  dotnet tool install --global dotnet-sonarscanner &&
                  export PATH="\$PATH:/root/.dotnet/tools" &&

                  dotnet-sonarscanner begin /k:"pfa-netjwt-angular" /d:sonar.login=\$SONAR_TOKEN /d:sonar.host.url="${SONARQUBE_URL}" &&

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
      echo '🧹 Nettoyage...'
      sh 'docker stop sonarqube || true'
    }
    success {
      echo '✅ Pipeline SonarQube complétée avec succès !'
    }
    failure {
      echo '❌ Échec de la pipeline – vérifiez les logs (connexion, réseau, etc.).'
    }
  }
}
