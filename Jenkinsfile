pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    SONARQUBE_URL = 'http://localhost:9010'
  }

  stages {

    stage('Lancer SonarQube (Docker)') {
      steps {
        echo '🚀 Lancement de SonarQube en local sur le port 9010...'
        sh '''
          docker rm -f sonarqube || true
          docker run -d --name sonarqube -p 9010:9000 sonarqube:lts
          echo "⏳ Attente de SonarQube (max 90s)..."
          for i in {1..30}; do
            if curl -s http://localhost:9010/api/system/health | grep -q '"status":"GREEN"'; then
              echo "✅ SonarQube est prêt !"
              break
            fi
            echo "⏳ SonarQube pas prêt... Retry $i"
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
              docker run --rm --network host \
                -v \$(pwd):/app \
                -w /app \
                -e SONAR_TOKEN=\$SONAR_TOKEN \
                mcr.microsoft.com/dotnet/sdk:8.0 sh -c '
                  echo "⌛ Attente que SonarQube soit prêt (sur localhost:9010)..."
                  for i in \$(seq 1 90); do
                    if curl -s http://localhost:9010/api/system/health | grep -q "GREEN"; then
                      echo "✅ SonarQube est prêt (GREEN)"
                      break
                    fi
                    echo "⏳ Retry \$i... toujours en attente"
                    sleep 2
                  done

                  dotnet tool install --global dotnet-sonarscanner
                  export PATH="\$PATH:/root/.dotnet/tools"

                  dotnet-sonarscanner begin /k:"pfa-netjwt-angular" /d:sonar.login=\$SONAR_TOKEN /d:sonar.host.url="${SONARQUBE_URL}"

                  dotnet restore /app/PFA/PFA.sln
                  dotnet build /app/PFA/PFA.sln
                  dotnet test /app/PFA/PFA.sln --no-build

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
      echo '🧹 Nettoyage SonarQube...'
      sh 'docker stop sonarqube || true'
    }
    success {
      echo '✅ Pipeline réussie avec SonarQube local sur port 9010 !'
    }
    failure {
      echo '❌ Pipeline échouée – vérifiez les étapes.'
    }
  }
}
