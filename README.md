# Validador JWT

O projeto foi criado com o objetivo de validar os tokens JWT, porém, para aprimorar a compreensão, foram criados tokens JWT e rotas para verificar as permissões de acesso da API. O projeto apresenta um Swagger para facilitar as análises.

# Tecnologias

Teste Unitário
 - Xunit
 - Moq

Backend
- Dotnet
- DependencyInjection
- JWTBearer

# Getting Started

O projeto foi criado utilizando a versão 8.0 do dotnet core

1. Instalar a versão net8.0
4. Clonar o projeto
5. Ir para o diretório do projeto
6. E realizar os comando conforme **Build and Test**

## Build and Test
1. cd .\validacaoJWT\
1. dotnet build
3. dotnet run
4. Acesse a rota http://localhost:5001/swagger/index.html
5. Para executar os testes de cobertura de código:

``` dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info /p:ExcludeByFile="*/Models/%2c/Model/%2c/Migrations/%2c/Enums/%2c/Migrations/%2c/DbContexts/%2c/Entities/%2c/devops/%2c/Program.cs%2c*/Startup.cs"```

## API

HOST: http://localhost:5001/