
<!--ts-->
   * [Sobre](#sobre)
   * [Requisitos técnicos](#requisitos-técnicos)
   * [Instalação](#instalacao)
   * [Como usar](#como-usar)
      * [Pre Requisitos](#pre-requisitos)
      * [Local files](#local-files)
      * [Remote files](#remote-files)
      * [Multiple files](#multiple-files)
      * [Combo](#combo)
   * [Tests](#testes)
   * [Tecnologias](#tecnologias)
<!--te-->

# Sobre
O seu objetivo é construir uma WebApi desenvolvida em .net 6.0 para efetuar o registro de clientes que deverão informar o nome completo, e-mail e uma lista de telefones para receber informações.

Para isso você deverá aplicar o conceito de CRUD:


- a. Create;
- b. Read;
- c. Update;
- d. Delete.

Você deverá entregar uma WebApi que tenha as seguintes funções:

- [x] Cadastrar o cliente informando o nome completo, e-mail e uma lista de telefones informando o DDD, número e o tipo [fixo ou celular];
- [x] Permitir consultar todos os clientes com seus respectivos e-mails e telefones;
- [x] Permitir a consulta de um cliente através do DDD e número;
- [x] Permitir a atualização do e-mail do cliente cadastrado;
- [x] Permitir a atualização do telefone do cliente cadastrado;
- [x] Permitir a exclusão de um cliente através do e-mail.

# Requisitos técnicos:

- 1 - Desenvolver em .Net 6.0;
- 2 - Utilizar base de dados local;
- 3 - Criar testes de unidade;
- 4 - Documentar a WebApi via Swagger.

# Instalação

Pré-requisitos
- .NET 6
- VS Code, Visual Studio, Rider
- InSominia (não obrigatório)

Como usar
- Baixe e execute o projeto, se tudo der certo o swagger deve ser aberto.

 ![texto](swagger.png) 

- Em caso de dúvida, importar o arquivo customer_api.json no InSominia, 

# Tecnologias
- .NET 6
- REST
- Banco de dados InMemory/Postgres
- Mediator
- Unit of Work (UoW)
- Fluent API

# Docker

```bash
## Runing docker compose
docker-compose -f docker-compose.yml up

## Update database
dotnet ef database update --project src/Customer.Infra --startup-project src/Customer.Api   

## Add Migration
dotnet ef migrations add NewMigration  --project src/Customer.Infra --startup-project src/Customer.Api 

## Initial state database 
dotnet ef database update 0 --project src/Customer.Infra --startup-project src/Customer.Api

## Remove migration
dotnet ef migrations remove --project src/Customer.Infra --startup-project src/Customer.Api
```
fdfdfd