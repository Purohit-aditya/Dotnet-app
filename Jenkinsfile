pipeline{
    agent {label 'slave'}
    
    tools {
        nodejs 'node20'
        
    }

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
                    sh 'npx eslint . || true'
                }
            }

        }

        stage('Dotnet-Linting'){

            steps{
                dir('src/DevOpsDemoApi'){
                    sh 'dotnet format  --verify-no-changes || true'
                }


            }
        }

        stage('Run tests'){
            
            steps{
                
                dir('tests/DevOpsDemoApi.Tests'){
                    sh 'dotnet test'
                }
            }
        }

        stage('Code Coverage'){

            steps{
                dir('tests/DevOpsDemoApi.Tests'){

                    sh  'dotnet test --collect:"XPlat Code Coverage" '

                }
            }
        }

        stage('Docker Build'){
            steps{

                sh 'docker compose up --build -d'
                 
            }
        }

        stage('Trivy scan'){
            steps{

                sh 'trivy image dotnet-app-backend:latest'
                sh 'trivy image dotnet-app-frontend:latest'
                sh 'trivy image nginx:stable-alpine'

            }

        }

        stage('Verify running'){
            steps{

                sh 'docker ps'
                sh 'curl -f http://localhost:9000/health/ '
                sh 'docker compose down'
            }
        }
    
        stage('Push to DockerHub'){
    steps{
        withCredentials([usernamePassword(credentialsId: 'dockerhub', usernameVariable: 'USER', passwordVariable: 'PASS')]) {
            sh 'echo $PASS | docker login -u $USER --password-stdin'
            sh 'docker tag dotnet-app-backend:latest $USER/dotnet-app-backend:latest'
            sh 'docker tag dotnet-app-frontend:latest $USER/dotnet-app-frontend:latest'
            sh 'docker push $USER/dotnet-app-backend:latest'
            sh 'docker push $USER/dotnet-app-frontend:latest'
        }
    }
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
