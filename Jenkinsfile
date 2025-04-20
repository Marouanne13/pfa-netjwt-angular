pipeline {
    agent any

    // Remplacez par l’ID Jenkins de votre token SonarCloud
    environment {
        SONAR_TOKEN = credentials('sonarcloud-token')
    }

    options {
        // Évite les timeouts trop rapides du wrapper durable
        durabilityHint('PERFORMANCE_OPTIMIZED')
    }

    stages {
        stage('Checkout') {
            steps {
                git(
                    url:           'git@github.com:Marouanne13/pfa-netjwt-angular.git',
                    branch:        'main',
                    credentialsId: 'jenkins-rsa'
                )
            }
        }

        stage('SonarCloud: begin analysis') {
            steps {
                withSonarQubeEnv('SonarCloud') {
                    sh '''
                        dotnet tool install --global dotnet-sonarscanner --version 5.13.1
                        export PATH="$HOME/.dotnet/tools:$PATH"
                        dotnet sonarscanner begin \
                          /k:"Marouanne13_pfa-netjwt-angular" \
                          /d:sonar.login="$SONAR_TOKEN"
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

        stage('SonarCloud: end analysis') {
            steps {
                withSonarQubeEnv('SonarCloud') {
                    sh 'dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"'
                }
            }
        }
    }

    post {
        success {
            echo '🎉 Build, tests et scan SonarCloud OK !'
        }
        failure {
            echo '❌ Échec : vérifiez le Quality Gate SonarCloud et vos logs.'
        }
    }
}
