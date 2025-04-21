pipeline {
  agent any

  environment {
    SONARQUBE = 'http://sonarqube:9000'
  }

  options {
    durabilityHint('MAX_SURVIVABILITY')
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

          echo "⏳ Attente de SonarQube côté hôte..."
          for i in {1..30}; do
            if curl -s http://localhost:9000/api/system/health | grep -q '"status":"GREEN"'; then
              echo "✅ SonarQube est prêt côté hôte !"
              break
            fi
            echo "⏳ Attente côté hôte... [$i]"
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
                  echo "⌛ Attente de SonarQube dans le réseau Docker..."
                  for i in \$(seq 1 60); do
                    if curl -s http://sonarqube:9000/api/system/health | grep -q "GREEN"; then
                      echo "✅ SonarQube est prêt dans le conteneur SDK !"
                      break
                    fi
                    echo "⏳ Retry \$i... toujours en attente dans le conteneur"
                    sleep 2
                  done

                  dotnet tool install --global dotnet-sonarscanner &&
                  export PATH="\$PATH:/root/.dotnet/tools" &&

                  dotnet-sonarscanner begin /k:"pfa-netjwt-angular" /d:sonar.login=\$SONAR_TOKEN /d:sonar.host.url="$SONARQUBE" &&

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
      echo '✅ Pipeline complète réussie avec analyse SonarQube dans conteneur !'
    }
    failure {
      echo '❌ Échec de la pipeline – vérifiez les logs (connexion, réseau, etc.).'
    }
  }
}
