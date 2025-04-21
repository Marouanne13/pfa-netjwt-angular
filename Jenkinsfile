pipeline {
  agent any

  // Évite les timeouts trop rapides du wrapper durable
  options {
    durabilityHint('PERFORMANCE_OPTIMIZED')
  }

stage('Checkout') {
  steps {
    git url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
        credentialsId: 'github-deploy-key',
        branch: 'main'
  }
}


    stage('Restore') {
      steps {
        // Restauration des packages NuGet
        sh 'dotnet restore PFA.sln --verbosity minimal'
      }
    }

    stage('Build') {
      steps {
        // Compilation de la solution sans relancer la restauration
        sh 'dotnet build PFA.sln --no-restore --verbosity minimal'
      }
    }

    stage('Test') {
      steps {
        // Exécution des tests unitaires
        sh 'dotnet test PFA.sln --no-build --verbosity normal'
      }
    }
  }

  post {
    success {
      echo '🎉 Build et tests OK !'
    }
    failure {
      echo '❌ Quelque chose a échoué…'
    }
  }
}
