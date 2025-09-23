using LibraryProject.Application.Consumers;
using LibraryProject.Application.Handlers.Command;
using LibraryProject.Application.Handlers.Query;
using LibraryProject.Domain.Repositories;
using LibraryProject.Infrastructure.EntityFramework;
using LibraryProject.Infrastructure.EntityFramework.Repositories;
using LibraryProject.Infrastructure.Mongo;
using LibraryProject.Infrastructure.Mongo.Repositories;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// SQL Server DbContext
builder.Services.AddDbContext<LibraryProjectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

MongoMappings.RegisterMaps();

// MongoDB Client e Database
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoConnection = builder.Configuration.GetValue<string>("Mongo:ConnectionString");
    return new MongoClient(mongoConnection);
});
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration.GetValue<string>("Mongo:Database");
    return client.GetDatabase(databaseName);
});

//MassTransit + RabbitMQ
var rabbitConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BookCreatedConsumer>();
    x.AddConsumer<BookLoanedConsumer>();
    x.AddConsumer<BookReturnedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitConfig["Host"], ushort.Parse(rabbitConfig["Port"]), "/", h =>
        {
            h.Username(rabbitConfig["Username"]);
            h.Password(rabbitConfig["Password"]);
        });

        cfg.ReceiveEndpoint("book-created-queue", e => e.ConfigureConsumer<BookCreatedConsumer>(context));
        cfg.ReceiveEndpoint("book-loaned-queue", e => e.ConfigureConsumer<BookLoanedConsumer>(context));
        cfg.ReceiveEndpoint("book-returned-queue", e => e.ConfigureConsumer<BookReturnedConsumer>(context));
    });
});
builder.Services.AddMassTransitHostedService();

//var rabbitConfig = builder.Configuration.GetSection("RabbitMQ");
//var host = rabbitConfig["Host"];
//var port = rabbitConfig["Port"];
//var username = rabbitConfig["Username"];
//var password = rabbitConfig["Password"];

//if (string.IsNullOrWhiteSpace(host))
//    throw new InvalidOperationException("RabbitMQ host is not configured correctly.");

//builder.Services.AddMassTransit(x =>
//{
//    // Registrar consumers
//    x.AddConsumer<BookCreatedConsumer>();
//    x.AddConsumer<BookLoanedConsumer>();
//    x.AddConsumer<BookReturnedConsumer>();

//    // Configurar RabbitMQ
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(host, port, "/", h =>
//        {
//            h.Username(username);
//            h.Password(password);
//        });

//        cfg.ReceiveEndpoint("book-created-queue", e =>
//        {
//            e.ConfigureConsumer<BookCreatedConsumer>(context);
//        });

//        cfg.ReceiveEndpoint("book-loaned-queue", e =>
//        {
//            e.ConfigureConsumer<BookLoanedConsumer>(context);
//        });

//        cfg.ReceiveEndpoint("book-returned-queue", e =>
//        {
//            e.ConfigureConsumer<BookReturnedConsumer>(context);
//        });
//    });
//});

builder.Logging.AddConsole();

// Repositórios SQL
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositórios Mongo (Read Models)
builder.Services.AddScoped<IBookReadRepository, BookReadRepository>();
builder.Services.AddScoped<ILoanReadRepository, LoanReadRepository>();

// MediatR (Comandos e Queries)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(BookCreatedCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(RequestLoanCommandHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(ReturnLoanCommandHandler).Assembly);

    cfg.RegisterServicesFromAssemblies(typeof(GetAllBookQueryHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetAllLoanQueryHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetBookByIdQueryHandler).Assembly);
    cfg.RegisterServicesFromAssemblies(typeof(GetLoanByIdQueryHandler).Assembly);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
