pipeline{
    
    agent { label 'docker' }

    environment {
        COMPOSE_PROJECT_NAME = "dotnet-app"

    }

    stages{

        stage('Checkout Code'){
            
            steps{

                git branch: 'main', url: 'https://github.com/Purohit-aditya/Dotnet-app'



            }
        }

        stage('Verify Environment'){

            steps{

                sh 'whoami'
                sh 'docker --version'


            }

        }

        stage('Frontend-Linting'){

            steps{
                dir('frontend'){

                    sh 'npm install'
                    sh 'npx eslint .'
                }
            }

        }

        stage('Dotnet-Linting'){

            steps{
                dir('src/DevOpsDemoApi'){
                    sh 'dotnet format  --verify-no-changes'
                }


            }
        }

        stage('Run tests')}{

            steps{
                dir('tests/DevOpsDemoApi.Tests'){
                    sh 'dotnet test'
                }
            }
        }

        stage('Code Coverage'){

            steps{
                dir('tests/DevOpsDemoApi.Tests'){

                    sh' dotnet test --collect:"XPlat Code Coverage" '

                }
            }
        }

        stage('Docker Build'){
            steps{

                sh 'docker compose up --build'
            }
        }

        stage('Trivy scan'){
            steps{

                sh 'trivy image dotnet-project-backend'
                sh 'trivy image dotnet-project-frontend'
                sh 'trivy image nginx:stable-alpine'

            }

        }

        stage('Verify running'){
            steps{

                sh 'docker ps'
                sh 'curl -f http://localhost:9000/health/ '
                sh 'docker compose down'
                sh 'docker compose build'

            }
        }

        stage('tag images'){
            steps{
                sh 'docker tag dotnet-project-frontend:latest  $FRONTEND_IMAGE'
                sh 'docker tag dotnet-project-app:latest $BACKEND_IMAGE'                    
            }
        }

        stage('Push images'){
            steps{
                sh 'docker push $FRONTEND_IMAGE'
                sh 'docker push $BACKEND_IMAGE'
            }
        }

        post {

            success {
                echo 'Pipeline executed successfully '


            }

            failure {
                echo 'Pipeline failed ! '
            }
        }
    }

}