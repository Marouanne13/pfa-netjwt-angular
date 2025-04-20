pipeline {
  agent any

  // Remplacez par l’ID Jenkins de votre token SonarCloud
  environment {
    SONAR_TOKEN = credentials('sonarcloud-token')
  }

  options {
    // évite les timeouts sur un FS lent
    durabilityHint('PERFORMANCE_OPTIMIZED')
  }

  stages {
    stage('Checkout') {
      steps {
        git url:      'git@github.com:Marouanne13/pfa-netjwt-angular.git',
            branch:   'main',
            credentialsId: 'jenkins-rsa'
      }
    }

    stage('SonarCloud: begin analysis') {
      steps {
        // Nécessite le plugin SonarCloud et une configuration "SonarCloud" dans Manage Jenkins → Configure System
        withSonarCloud() {
          sh """
            dotnet tool install --global dotnet-sonarscanner --version 5.11.0 || true
            export PATH=\$HOME/.dotnet/tools:\$PATH
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

    stage('Test & Coverage') {
      steps {
        // collecte la couverture OpenCover
        sh """
          dotnet test PFA.sln --no-build \
            /p:CollectCoverage=true \
            /p:CoverletOutputFormat=opencover \
            /p:CoverletOutput=coverage/
        """
      }
    }

    stage('SonarCloud: end analysis') {
      steps {
        withSonarCloud() {
          sh """
            dotnet sonarscanner end \
              /d:sonar.login=\$SONAR_TOKEN \
              /d:sonar.cs.opencover.reportsPaths=\"**/coverage/coverage.opencover.xml\"
          """
        }
      }
    }

    stage('Quality Gate') {
      steps {
        // attend la remontée du résultat SonarCloud et échoue si le gate est rouge
        timeout(time: 5, unit: 'MINUTES') {
          waitForQualityGate abortPipeline: true
        }
      }
    }
  }

  post {
    success { echo '🎉 Tout est OK, SonarCloud a validé votre Quality Gate !' }
    failure { echo '❌ Échec : vérifiez le Quality Gate SonarCloud et vos logs.' }
  }
}
