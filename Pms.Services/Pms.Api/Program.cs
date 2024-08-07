using Pms.Api.Configurations;
using Pms.Core.Abstraction;
using Pms.Core.Api;
using Pms.Core.ApiConfig;
using Pms.Core.Config;
using Pms.Core.Config.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Setup environment variables initially
// builder.Configuration.AddUserSecrets<Program>();

// Setup configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var secretsConfig = builder.Configuration.GetSection("MicroServiceConfig")
    .Get<MicroServiceConfig>(c => c.BindNonPublicProperties = true);

secretsConfig ??= new MicroServiceConfig();
secretsConfig.DatabaseConfig ??= new DatabaseConfig
{
    Host = Environment.GetEnvironmentVariable("PGHOST"),
    Port = Environment.GetEnvironmentVariable("PGPORT"),
    Password = Environment.GetEnvironmentVariable("PGPASSWORD"),
    User = Environment.GetEnvironmentVariable("POSTGRES_USER"),
    DatabaseName = "itsquarehub-pms"
};

var host = Environment.GetEnvironmentVariable("PGHOST");
if (!string.IsNullOrEmpty(host))
    secretsConfig.DatabaseConfig.Host = host;

var port = Environment.GetEnvironmentVariable("PGPORT");
if (!string.IsNullOrEmpty(port))
    secretsConfig.DatabaseConfig.Host = port;

var password = Environment.GetEnvironmentVariable("PGPASSWORD");
if (!string.IsNullOrEmpty(password))
    secretsConfig.DatabaseConfig.Password = password;

var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
if (!string.IsNullOrEmpty(user))
    secretsConfig.DatabaseConfig.User = user;

builder.Services.AddSingleton<IMicroServiceConfig, MicroServiceConfig>(service => secretsConfig);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCoreLogging();
builder.Services.AddCoreControllers();
builder.Services.AddCoreCompression();
builder.Services.AddCoreEntityServices<IEntityService>("Pms.Domain");
builder.Services.AddCoreEntityServices<IDatalayerEntityService>("Pms.Datalayer");
builder.Services.AddPmsDatabase(secretsConfig);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.UseAtsDatabase();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.UseHealthChecks("/health");

app.Run();
