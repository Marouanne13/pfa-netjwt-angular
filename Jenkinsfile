pipeline {
  agent any

  environment {
    // SonarQube
    SONAR_TOKEN     = 'squ_1ff12c102b3b9c50acdd91aa28d76ba11515b23c'
    SONAR_HOST_URL  = 'http://localhost:9000'
    // Docker Hub
    DOCKER_REPO     = 'marouane1302/pfa-voyage'
  }

  stages {
    stage('Checkout') {
      steps {
        git(
          url:          'git@github.com:Marouanne13/pfa-netjwt-angular.git',
          branch:       'main',
          credentialsId:'jenkins-ssh-deploy'
        )
        sh 'pwd; ls -la'
      }
    }

    stage('Docker Login') {
      steps {
        withCredentials([usernamePassword(
          credentialsId: 'docker-hub-credentials',
          usernameVariable: 'DOCKER_USER',
          passwordVariable: 'DOCKER_PASS'
        )]) {
          sh "timeout 300 sh -c 'echo \$DOCKER_PASS | docker login -u \$DOCKER_USER --password-stdin'"
        }
      }
    }

    stage('SonarQube Analysis') {
      steps {
        // Begin
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 || true

            dotnet sonarscanner begin \
              /k:"pfa-netjwt-angular" \
              /d:sonar.login=$SONAR_TOKEN \
              /d:sonar.host.url=$SONAR_HOST_URL \
              /d:sonar.verbose=true
          '''
        }
        // Restore & Build
        sh 'dotnet restore PFA.sln --verbosity minimal'
        sh 'dotnet build   PFA.sln --no-restore --verbosity minimal'
      }
    }

    stage('Build & Publish .NET + Docker Image') {
      steps {
        dir('PFA') {
          // Pull pour cache
          sh 'docker pull $DOCKER_REPO:latest || true'

          // Build Docker optimisé
          sh '''
            docker build \
              --cache-from $DOCKER_REPO:latest \
              -f Dockerfile.backend \
              -t dotnet-backend:latest \
              .
          '''

          // Test container
          sh 'docker rm -f test-container || true'
          sh 'docker run -d --name test-container -p 5278:5278 dotnet-backend:latest'
          sh 'sleep 10'
          sh '''
            CODE=$(curl -s -o /dev/null -w "%{http_code}" http://localhost:5278/api/health)
            if [ "$CODE" != "200" ]; then
              echo "❌ Health check failed: HTTP $CODE"
              docker logs test-container
              exit 1
            fi
            echo "✅ Health check OK"
          '''
          sh 'docker ps --filter name=test-container'
          sh 'docker logs test-container'

          // Tag & Push
          sh '''
            docker tag dotnet-backend:latest $DOCKER_REPO:latest
            docker push $DOCKER_REPO:latest
          '''
        }
      }
    }

    stage('End SonarQube Analysis') {
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
      echo '✅ Build, tests, analyse SonarQube et push Docker réussis.'
    }
    failure {
      echo '❌ La pipeline a échoué. Vérifiez les logs.'
    }
  }
}
