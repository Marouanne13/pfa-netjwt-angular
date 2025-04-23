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
   stage('Print Working Directory') {
            steps {
                sh 'pwd'  // Prints the current directory
                sh 'ls -la'  // Lists the contents of the current directory
            }
   }
   stage('Build Backend Docker Image') {
    steps {
        dir('PFA') {  // Assurez-vous que vous êtes dans le bon répertoire pour le backend
            sh 'ls -la'  // Vérifier que Dockerfile.backend est là
            sh 'docker build -f Dockerfile.backend -t dotnet-backend:latest .'
        }
    }
}
stage('Tag and Push Docker Image') {
    steps {
        script {
            // Taguer l'image Docker avec le nom de votre repository Docker Hub
            sh 'docker tag dotnet-backend:latest marouane1302/pfa-voyage:latest'

            // Pousser l'image vers Docker Hub
            sh 'docker push marouane1302/pfa-voyage:latest'
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
