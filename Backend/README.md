# Processo Seletivo Concent Sistemas

Projeto elaborado visando construir uma aplicativo que simula um restaurante.

## Proposta de desenvolvimento
 
 - Desenvolvimento de WebApi RESTful

 - Utilizar .NET e Linguagem C#

 - Framework de Persistência:  Entity Framework

 - Banco de Dados Relacional (SQL)

 - Desenvolver o front-end em Angular

 
## Orientações

Antes de iniciar o projeto, é necessário ter o [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) instalado em sua máquina.

Além disso, é necessário ter o [MS SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), podendo a imagem ser gerada por meio do [Docker](https://www.docker.com/), conforme orientações abaixo.

Tenha instalado algum software para fazer requisições HTTP ao aplicativo criado. São alguns exemplos: Thunder Client (extensão do VS Code), [Insomnia](https://insomnia.rest/download) e [Postman](https://www.postman.com/).

Por fim, é necessário ter o git instalado e inicializado no diretório para clonar o repositório e rodar o projeto localmente.
 
## Como inicializar o projeto

Clone o repositório
```bash
git clone git@github.com:MaxRudim/teste-tecnico-concent-sistemas.git
```
Entre na pasta que você acabou de criar com o comando:
```bash
cd teste-tecnico-concent-sistemas/Backend
```
Restaure as dependências utilizando o comando:
```bash
dotnet restore
```
Inicialize o banco de dados atraves do comando:
```docker
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SenhaSegura123." \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

ou através de do comando:
```docker
docker-compose up
```

Também é necessário adicionar a linha de conexão com o banco de dados no arquivo ConcentKitchen/appsettings.json, da seguinte forma:
```JSON
  "ConnectionStrings": {
      "KitchenDatabase": "Server=127.0.0.1;Database=concent-sistemas;TrustServerCertificate=True;User=SA;Password=SenhaSegura123."
  }
```
.</br></br>
Entre na pasta do aplicativo:
```bash
cd ConcentKitchen
```
Suba o banco
```bash
dotnet ef database update
```
Inicialize a aplicação
```bash
dotnet run
```

## Rotas

O projeto possui as seguintes rotas: `apiurl/client`, `apiurl/dish`, `apiurl/order` e `apiurl/order/dish`. Obs: a url da api é o primeiro endereço informado quando executado o comando `dotnet run`.

Este aplicativo utiliza swagger, portanto, para verificar todas as rotas do servidor, basta digitar no navegador: `apiurl/swagger`.

## Testando a aplicação

Este projeto foi realizado utilizando testes com xUnit. Para rodar os testes, basta entrar na pasta `ConcentKitchen.Test` e, sem estar com o aplicativo rodando (dê um ctrl + c no terminal caso o dotnet run esteja em execução), utilize o comando: `dotnet test`.

## Agradeço a oportunidade!!