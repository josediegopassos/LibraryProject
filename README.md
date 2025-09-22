
# Projeto de Biblioteca - .NET 8 + MongoDB + SQL Server + RabbitMQ + MassTransit

Este projeto é um sistema de gerenciamento de biblioteca que utiliza **.NET 8**, **SQL Server**, **MongoDB**, **RabbitMQ** e **MassTransit**.  
Ele segue princípios de **DDD (Domain-Driven Design)**, **CQRS** e **event-driven architecture**.

---

## Tecnologias Utilizadas

- **.NET 8** (Web API)
- **Entity Framework Core** (SQL Server - escrita)
- **MongoDB** (leitura / projeções)
- **RabbitMQ + MassTransit** (mensageria/eventos)
- **xUnit + FluentAssertions + Moq** (testes unitários)
- **Docker & Docker Compose** (infraestrutura local)

---

## Estrutura do Projeto

```
src/
 ├── LibraryProject.Domain          -> Entidades, agregados e regras de negócio
 ├── LibraryProject.Application     -> Commands, Queries e Handlers (CQRS + MediatR)
 ├── LibraryProject.Infrastructure  -> Repositórios, EF Core, MongoDB, MassTransit
 └── LibraryProject.API             -> Controllers e configuração principal
```

---

## Configuração do Ambiente

### 1. Clonar o repositório

```bash
git clone https://github.com/josediegopassos/LibraryProject.git
cd LibraryProject
```

### 2. Configuração via `docker-compose`

O projeto já possui um arquivo **docker-compose.yml** que sobe a infraestrutura necessária:

```bash
docker-compose up -d
```

Serviços disponíveis:
- **SQL Server** → `localhost:1433`
- **MongoDB** → `localhost:27017`
- **RabbitMQ** → `localhost:5672`  
  Painel de administração em: [http://localhost:15672](http://localhost:15672)  
  (usuário: `guest`, senha: `guest`)

---

## Configuração da Aplicação

A configuração dos serviços está no **appsettings.json**:

```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=localhost,1433;Database=LibraryProjectDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;",
    "MongoDb": "mongodb://root:example@localhost:27017",
    "RabbitMq": "amqp://guest:guest@localhost:5672"
  }
}
```

---

## Rodando Migrations (SQL Server)

1. Criar a migration inicial:
   ```bash
   dotnet ef migrations add InitialCreate -p src/LibraryProject.Infrastructure -s src/LibraryProject.API
   ```

2. Atualizar o banco:
   ```bash
   dotnet ef database update -p src/LibraryProject.Infrastructure -s src/LibraryProject.API
   ```

---

## Mensageria (RabbitMQ + MassTransit)

O **MassTransit** é usado para publicar/consumir eventos entre camadas.  
Exemplo de configuração no `Program.cs`:

```csharp
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(BookCreatedConsumer).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["ConnectionStrings:RabbitMq"]);
        cfg.ConfigureEndpoints(context);
    });
});
```

Exemplo de **Consumer**:

```csharp
public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
{
    private readonly IMongoCollection<BookReadModel> _collection;

    public BookCreatedConsumer(IMongoDatabase database)
    {
        _collection = database.GetCollection<BookReadModel>("Books");
    }

    public async Task Consume(ConsumeContext<BookCreatedEvent> context)
    {
        var readModel = new BookReadModel
        {
            Id = context.Message.Id,
            Title = context.Message.Title,
            Author = context.Message.Author
        };

        await _collection.InsertOneAsync(readModel);
    }
}
```

---

## Testes

Os testes estão no projeto `LibraryProject.Tests`, utilizando:

- **xUnit**
- **FluentAssertions**
- **Moq**

Para rodar os testes:

```bash
dotnet test
```

---

## Endpoints Principais (API)

- `POST /api/books` → Criar um novo livro
- `POST /api/books/{id}/loan` → Emprestar livro
- `GET /api/books` → Listar livros (MongoDB - Read Model)

---

## Acessando os Containers

- **MongoDB** → Recomendado usar [MongoDB Compass](https://www.mongodb.com/try/download/compass)  
- **SQL Server** → Pode ser acessado via [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio) ou `sqlcmd`
- **RabbitMQ** → Painel em [http://localhost:15672](http://localhost:15672)

---

## Licença

Este projeto está sob a licença MIT.  
Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
