# Resumo Geral do Projeto MiniHelpDesk

## 1. Visão Geral

O MiniHelpDesk é uma aplicação web ASP.NET Core MVC construída com .NET 10.0. O projeto implementa um sistema básico de chamados onde usuários podem:

- cadastrar chamados
- listar chamados
- ver detalhes de chamados
- criar chamados via formulário MVC
- também expor uma rota API simples (`/api/chamados`) para criação de chamados via JSON

A interface usa o padrão MVC com controladores, views e modelos.

## 2. Estrutura principal do projeto

- `Program.cs`
  - configura o pipeline de middleware do ASP.NET Core
  - habilita MVC com `AddControllersWithViews()`
  - configura o `DbContext` para MySQL
  - registra rotas MVC padrão

- `Controllers/ChamadosController.cs`
  - gerencia ações de listagem, detalhes e criação de chamados
  - usa injeção de dependência para receber `AppDbContext`
  - persiste dados chamando `_context.SaveChanges()`

- `Data/AppContext.cs`
  - define `AppDbContext` como `DbContext` do Entity Framework Core
  - expõe `DbSet<Chamado> Chamados`

- `Models/Chamado.cs`
  - representa a entidade `Chamado`
  - inclui campos como `Id`, `Titulo`, `Descricao`, `Status`, `DataAbertura` e `DataFechamento`

- `appsettings.json`
  - guarda a string de conexão com o banco de dados MySQL

- `Migrations/`
  - contém migrações EF Core geradas para criar o esquema inicial no banco

## 3. Conexão com banco de dados MySQL (detalhado)

### 3.1. O que está sendo usado

O projeto usa ORM, ou seja, não há SQL manual para inserir, atualizar ou buscar dados. Ele usa:

- `Microsoft.EntityFrameworkCore` (EF Core)
- `Pomelo.EntityFrameworkCore.MySql` (provider MySQL para EF Core)
- `Microsoft.EntityFrameworkCore.Design` (suporte a migrações/designer)

Esse é o ponto-chave: mesmo que o banco seja MySQL, a aplicação fala com o banco via Entity Framework Core, que gera e executa os comandos SQL internamente.

### 3.2. Por que precisa de framework externo?

EF Core é um framework ORM. Para conectar ao MySQL, o .NET precisa de um provedor compatível. No projeto, o provedor é o pacote `Pomelo.EntityFrameworkCore.MySql`.

Sem esse provedor, o `DbContext` não saberia como traduzir as operações do .NET para instruções SQL MySQL.

### 3.3. Onde a conexão é configurada

1. `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=minihelpdesk;user=root;password="
}
```

2. `Program.cs`

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
```

### 3.4. Passo a passo para rodar localmente com MySQL

1. Instale o MySQL localmente ou use um servidor MySQL acessível.
2. Crie a base de dados desejada, por exemplo `minihelpdesk`.
3. Abra `appsettings.json` e ajuste a string de conexão:
   - `server`
   - `port`
   - `database`
   - `user`
   - `password`
4. Confirme que os pacotes NuGet estão instalados no projeto:
   - `Microsoft.EntityFrameworkCore`
   - `Microsoft.EntityFrameworkCore.Design`
   - `Pomelo.EntityFrameworkCore.MySql`
5. Restaure dependências:
   - `dotnet restore`
6. Se ainda não criou as migrações, gere-as (apenas se alterar o modelo):
   - `dotnet ef migrations add InitialCreate`
7. Aplique as migrações no banco:
   - `dotnet ef database update`

> Observação: o projeto já contém uma pasta `Migrations/` com uma migração inicial, então possivelmente basta executar `dotnet ef database update` se a base estiver criada.

### 3.5. Como o ORM é usado no código

- `AppDbContext` define o conjunto de dados:
  - `public DbSet<Chamado> Chamados { get; set; }`
- O controlador injeta o contexto:
  - `public ChamadosController(AppDbContext context)`
- Operações comuns:
  - busca: `_context.Chamados.ToList()`
  - busca por ID: `_context.Chamados.FirstOrDefault(c => c.Id == id)`
  - inserção: `_context.Chamados.Add(chamado)`
  - gravação: `_context.SaveChanges()`

## 4. O que já foi feito no projeto

- implementação de um CRUD básico para chamados (pelo menos criação e leitura)
- integração de EF Core com MySQL via Pomelo
- rota MVC para visualização e criação de chamados
- rota API para criação via JSON
- uso de migrações para versionamento do esquema de banco
- configuração de middleware, HTTPS, arquivos estáticos e roteamento MVC

## 5. Recomendações rápidas

- verificar se o banco MySQL está rodando antes de iniciar a aplicação
- manter a string de conexão segura (não deixar senha vazia em produção)
- usar migrações EF Core sempre que alterar o modelo `Chamado`
- em produção, revisar `AllowedHosts` e configurações de segurança
