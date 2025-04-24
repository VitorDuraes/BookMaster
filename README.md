BookMaster é um sistema completo de gerenciamento de livros e autores desenvolvido com ASP.NET Core, utilizando uma arquitetura em camadas e o padrão MVC, visando escalabilidade, modularidade e manutenibilidade.

🚀 Tecnologias Utilizadas
.NET 8 / ASP.NET Core

Entity Framework Core

SQL Server

AutoMapper

FluentValidation

Swagger / Swashbuckle

JWT Bearer Authentication

🧱 Arquitetura
A aplicação foi construída com uma arquitetura em camadas, dividida da seguinte forma:

Controllers: Recebem e respondem requisições HTTP.

Models: Entidades do domínio, como Livro e Autor.

DTOs: Data Transfer Objects para transporte eficiente de dados entre camadas.

Services: Contêm a lógica de negócio (ex: AutorService, LivroService, UsuarioService).

Data: Camada de persistência usando AppDbContext com EF Core.

Mappings: Configuração do AutoMapper para mapeamento entre DTOs e Models.

Validations: Validações com FluentValidation, como AutorCriacaoDTOValidator.

Middlewares: Tratamento de erros e logging via middlewares personalizados.

🔐 Autenticação
O BookMaster utiliza JWT Bearer Token para autenticação, com validação de:

Issuer

Audience

Chave Secreta

📖 Documentação
A API é totalmente documentada via Swagger, com:

Comentários XML nos endpoints

Autenticação via Bearer Token

Interface interativa no Swagger UI (/swagger)

▶️ Como Executar
Clone o repositório:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/bookmaster.git
cd bookmaster
Configure a string de conexão no appsettings.json.

Aplique as migrations:

bash
Copiar
Editar
dotnet ef database update
Execute a aplicação:

bash
Copiar
Editar
dotnet run
Acesse:

API: https://localhost:5001/api

Swagger UI: https://localhost:5001/swagger

📌 Funcionalidades
CRUD completo de autores e livros

Autenticação e autorização via JWT

Middleware de logging de requisições

Middleware global de tratamento de erros

Validações robustas com FluentValidation

Documentação via Swagger

🛠️ Futuras Melhorias
Implementar paginação e filtros

Upload de imagens de capa dos livros

Interface front-end com Blazor ou React

Controle de usuários e permissões avançado

🧑‍💻 Autor
Desenvolvido por Vitor Durães
