pipeline {
  agent any
  options { durabilityHint('PERFORMANCE_OPTIMIZED') }

  environment {
    SONAR_TOKEN = credentials('sonarcloud-token')
  }

  stages {
    stage('Checkout') {
      steps {
        git url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
            credentialsId: 'github-deploy-key',
            branch: 'main'
      }
    }

    stage('SonarCloud: begin analysis') {
      steps {
        withSonarQubeEnv('sonarcloud') {
          sh 'dotnet tool install --global dotnet-sonarscanner --version 5.12.0 --verbosity quiet || true'
          sh 'dotnet sonarscanner begin /k:Marouanne13_pfa-netjwt-angular /o:Marouanne13 /d:sonar.login=$SONAR_TOKEN'
        }
      }
    }

    stage('Restore') {
      steps { sh 'dotnet restore PFA.sln --verbosity minimal' }
    }

    stage('Build') {
      steps { sh 'dotnet build PFA.sln --no-restore --verbosity minimal' }
    }

    stage('Test') {
      steps { sh 'dotnet test PFA.sln --no-build --verbosity minimal' }
    }

    stage('SonarCloud: end analysis') {
      steps {
        sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
      }
    }
  }

  post {
    success { echo '🎉 Build, tests et analyse SonarCloud réussis !' }
    failure { echo '❌ Échec de la pipeline, vérifiez les logs Jenkins et le Quality Gate SonarCloud.' }
  }
}
