using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

//using Pms.Api.Configurations;
using Pms.Core.Abstraction;
using Pms.Core.Api;
using Pms.Core.ApiConfig;
using Pms.Core.Authentication;
using Pms.Core.Config.Database;
using Pms.Domain.Services.Config;
using Pms.ITSquarehub.Authentication;
using Pms.Shared.Constants;

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
    Host = Environment.GetEnvironmentVariable("PGHOST") ?? string.Empty,
    Port = Environment.GetEnvironmentVariable("PGPORT") ?? string.Empty,
    Password = Environment.GetEnvironmentVariable("PGPASSWORD") ?? string.Empty,
    User = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? string.Empty,
    DatabaseName = "itsquarehub-pms"
};

var host = Environment.GetEnvironmentVariable("PGHOST");
if (!string.IsNullOrEmpty(host))
    secretsConfig.DatabaseConfig.Host = host;

var port = Environment.GetEnvironmentVariable("PGPORT");
if (!string.IsNullOrEmpty(port))
    secretsConfig.DatabaseConfig.Port = port;

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
//builder.Services.AddPmsDatabase(secretsConfig);
builder.Services.AddITSAuth(() => secretsConfig.AuthenticationConfig!);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
    options.SwaggerDoc(GlobalConstant.DocumentVersion, new OpenApiInfo
    {
        Version = GlobalConstant.DocumentVersion,
        Title = GlobalConstant.DocumentTitle,
        Description = GlobalConstant.DocumentDescription
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Authentication Setup
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireSignedTokens = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretsConfig.JwtConfig!.Key)),

        ValidateLifetime = true,
        RequireExpirationTime = true,

        ValidateIssuer = true,
        ValidIssuer = secretsConfig.JwtConfig.Issuer,

        ValidateAudience = true,
        ValidAudience = secretsConfig.JwtConfig.Audience,
    };
});
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

//app.UseAtsDatabase();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

// for authentication
app.UseMiddleware<HttpOnlyMiddleware>(secretsConfig.JwtConfig!.CookieName);
app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/health");

app.Run();
