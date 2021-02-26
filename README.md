# backend-challenge-itau
Codigo proposto para a challenge url:https://github.com/itidigital/backend-challenge

## Itau Senha
Backend do challenge proposto para validação de senha.

## Como executar o projeto local (WebAPI)
1. Executar o projeto `src\Itau.Senha\Itau.Senha.sln`. Defina o projeto `Itau.Senha.webapi` como projeto de inicialização. Ao executar pelo Visual Studio o navegador padrão será aberto na página do Swagger.
- **OU** Executar via linha de comando e abrir o swagger na url https://localhost:5001/swagger/index.html ou http://localhost:5000/swagger/index.html :
    ```
    dotnet run -p src\Itau.Senha\Itau.Senha.WebApi
    ```
-----
## Quality Gate
- A pasta `src\Itau.Senha\report` contem os arquivos de Quality Gate do projeto, podendo ser acessado pelo arquivo `src\Itau.Senha\report\index.htm` 


## Características do projeto:

- .NET Core 3.1
- Framework de Testes: **NUnit**
- Framework de Assertions: **FluentAssertions**
- Framework de Mock: **AutoMoq**
- Projeto para testes de Unidade
- Projeto para testes de Integração
- Controllers e Actions atendendo os padrões RESTFul
- Framework de logs: **NLog**
-----