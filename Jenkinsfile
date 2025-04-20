pipeline {
  agent any

  // Récupère votre token SonarCloud depuis les Credentials Jenkins (ID = sonarcloud-token)
  environment {
    SONAR_TOKEN = credentials('sonarcloud-token')
  }

  options {
    // optimise la durabilité du job pour éviter les timeouts sur gros volumes de log
    durabilityHint('PERFORMANCE_OPTIMIZED')
  }

  stages {
    stage('Checkout') {
      steps {
        // clone votre repo en SSH avec la clé Jenkins (ID = jenkins-rsa)
        git url:          'git@github.com:Marouanne13/pfa-netjwt-angular.git',
            credentialsId: 'jenkins-rsa',
            branch:        'main'
      }
    }

    stage('SonarCloud: begin analysis') {
      steps {
        // injecte la config SonarCloud et le token dans l’environnement de build
        withSonarQubeEnv('SonarCloud') {
          sh """
            dotnet tool install --global dotnet-sonarscanner --version 5.6.0
            export PATH=\"\$PATH:\$HOME/.dotnet/tools\"
            dotnet sonarscanner begin \
              /k:\"Marouanne13_pfa-netjwt-angular\" \
              /o:\"Marouanne13\" \
              /d:sonar.login=\$SONAR_TOKEN
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
        sh 'dotnet test PFA.sln --no-build --verbosity normal'
      }
    }

    stage('SonarCloud: end analysis') {
      steps {
        withSonarQubeEnv('SonarCloud') {
          sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
        }
      }
    }
  }

  post {
    success {
      echo '🎉 Build, tests et analyse SonarCloud réussis !'
    }
    failure {
      echo '❌ Échec – vérifiez les logs Jenkins et le Quality Gate SonarCloud.'
    }
  }
}
