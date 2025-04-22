pipeline {
  agent any

  environment {
    SONAR_TOKEN = 'squ_1ff12c102b3b9c50acdd91aa28d76ba11515b23c' // Or the token you've configured in local SonarQube (default user is 'admin' if not changed)
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
   sh 'pwd'  // Afficher le répertoire après le checkout
    sh 'ls -la' 
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
stage('Verify Build Artifact') {
  steps {
    script {
      def dllExists = fileExists 'PFA/bin/Debug/net8.0/PFA.dll'
      if (!dllExists) {
        error("❌ Le fichier PFA.dll n'a pas été généré. Échec de la compilation.")
      } else {
        echo '✅ Fichier PFA.dll trouvé. Compilation réussie.'
      }
    }
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
stage('Build Front Docker Image') {
  steps {
   dir('pfa-netjwt-angular/PFA/planification-de-voyage') {  // Assurez-vous que vous êtes dans le bon répertoire pour Angular
  sh 'ls -la'
  sh 'docker build -f Dockerfile.frontend -t angular-frontend:latest .'
}

  }
}


    stage('Build Backend Docker Image') {
      steps {
       dir('pfa-netjwt-angular/PFA') {  // Répertoire pour le backend .NET
  sh 'ls -la'
  sh 'docker build -f Dockerfile.backend -t dotnet-backend:latest .'
}

      }
    }

    stage('Build and Run with Docker Compose') { 
      steps {
        script {
          // Assurez-vous que Docker Compose est installé sur le serveur Jenkins
          sh 'docker-compose up --build -d'
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
