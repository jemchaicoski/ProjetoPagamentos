using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProjetoPagamentos.Application.Repositories;
using ProjetoPagamentos.Application.Services;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Infrastructure.Persistence;
using ProjetoPagamentos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);
ConfigureSqlServer(builder);
ConfigureMongoDB(builder);
ConfigureOpenApi(builder);

var app = builder.Build();

ConfigurePipeline(app);
ConfigureDevelopmentSettings(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    builder.Services.AddScoped<ITransactionService, TransactionService>();
    builder.Services.AddScoped<IAccountService , AccountService>();
    builder.Services.AddScoped<IUserService, UserService>();
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

    BsonClassMap.RegisterClassMap<CreditTransaction>(cm =>
    {
        cm.AutoMap();
        cm.SetDiscriminator("CreditTransaction");
    });
}

void ConfigureOpenApi(WebApplicationBuilder builder)
{
    builder.Services.AddOpenApi();
    builder.Configuration
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
}

void ConfigurePipeline(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

void ConfigureDevelopmentSettings(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
}