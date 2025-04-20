pipeline {
  agent any

  /*  
    PERFORMANCE_OPTIMIZED : Jenkins n’écrit plus le log à chaque écriture
    de sortie, plus de timeout “wrapping script not touching the log file”.
  */
  options {
    durabilityHint('PERFORMANCE_OPTIMIZED')
  }

  stages {
    stage('Checkout') {
      steps {
        git url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
            credentialsId: 'jenkins-rsa',
            branch: 'main'
      }
    }

    stage('Restore') {
      steps {
        // affiche un peu plus de sortie pour rassurer Jenkins
        sh 'dotnet restore PFA.sln --verbosity minimal'
      }
    }

    stage('Build') {
      steps {
        // build sans restore (déjà fait), avec progression minimale
        sh 'dotnet build PFA.sln --no-restore --verbosity minimal'
      }
    }

    stage('NPM & Angular') {
      steps {
        sh 'npm install'
        // –verbose/–progress pour un peu de log à chaque étape
        sh 'ng build --configuration production --verbose --progress'
      }
    }

    stage('Test') {
      steps {
        sh 'dotnet test PFA.sln --no-build --verbosity minimal'
      }
    }
  }

  post {
    success { echo '🎉 Build et tests OK !' }
    failure { echo '❌ Quelque chose a échoué…' }
  }
}
