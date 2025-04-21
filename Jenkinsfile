pipeline {
  agent any

  options {
    durabilityHint('MAX_SURVIVABILITY')
  }

  environment {
    // Liaison du token "JenkinsCI" via l'ID "sonarcloud-token"
    SONAR_TOKEN = credentials('sonarcloud-token')
  }

  stages {

    stage('Checkout') {
      steps {
        git(
          url: 'git@github.com:Marouanne13/pfa-netjwt-angular.git',
          credentialsId: 'jenkins-ssh-deploy',
          branch: 'main'
        )
      }
    }

    stage('Install SonarScanner') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
            dotnet tool install --global dotnet-sonarscanner --version 10.1.2 --verbosity quiet || true
          '''
        }
      }
    }

    stage('SonarCloud: Begin Analysis') {
      steps {
        withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
          sh '''
            dotnet sonarscanner begin \
              /k:"Marouanne13_pfa-netjwt-angular" \
              /o:"marouanne13" \
              /d:sonar.host.url=https://sonarcloud.io \
              /d:sonar.login=$SONAR_TOKEN \
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

    stage('Test') {
      steps {
        sh 'dotnet test PFA.sln --no-build --verbosity minimal'
      }
    }

    stage('SonarCloud: End Analysis') {
      steps {
        script {
          try {
            withEnv(["PATH+DOTNET=${HOME}/.dotnet/tools"]) {
              sh 'dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN'
            }
          } catch (err) {
            echo "❌ Erreur pendant l'étape 'SonarCloud: End Analysis'"
            echo "💥 Détail de l'erreur : ${err}"
            error("SonarScanner end failed.")
          }
        }
      }
    }

    stage('Vérification Quality Gate') {
      steps {
        script {
          echo "🔍 Vérification du Quality Gate via l’API SonarCloud..."

          def projectKey = "Marouanne13_pfa-netjwt-angular"
          def encodedToken = SONAR_TOKEN.bytes.encodeBase64().toString()
          def response = httpRequest(
            url: "https://sonarcloud.io/api/qualitygates/project_status?projectKey=${projectKey}",
            customHeaders: [[name: 'Authorization', value: "Basic ${encodedToken}"]],
            validResponseCodes: '200'
          )

          def json = readJSON text: response.content
          def status = json.projectStatus.status
          echo "📊 Quality Gate Status: ${status}"

          if (status != 'OK') {
            error("❌ Quality Gate échoué : ${status}")
          } else {
            echo "✅ Quality Gate validé !"
          }
        }
      }
    }
  }

  post {
    success {
      echo '🎉 Build, tests et analyse SonarCloud réussis !'
    }
    failure {
      echo '❌ Échec de la pipeline – vérifiez les logs Jenkins et le Quality Gate SonarCloud.'
    }
  }
}
