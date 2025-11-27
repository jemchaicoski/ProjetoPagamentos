using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Infrastructure.Persistence;
using ProjetoPagamentos.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureSqlServer(builder);
ConfigureMongoDB(builder);

var app = builder.Build();

ConfigurePipeline(app);

app.Run();


void ConfigureServices(WebApplicationBuilder builder)
{
    // DI
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    builder.Services.AddScoped<ITransactionService, TransactionService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IUserService, UserService>();

    // Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    });

    builder.Services.AddControllers();
}


void ConfigureSqlServer(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
}


void ConfigureMongoDB(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
    var databaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);

    builder.Services.AddSingleton(database);

    if (!BsonClassMap.IsClassMapRegistered(typeof(CreditTransaction)))
    {
        BsonClassMap.RegisterClassMap<CreditTransaction>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator("CreditTransaction");
        });
    }
}


void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();
}
