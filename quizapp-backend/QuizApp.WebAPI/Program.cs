using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using QuizApp.Business;
using QuizApp.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controller service to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "QuizApp Web API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Add DbContext to the container
builder.Services.AddDbContext<QuizAppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("QuizAppConnection");
    if(!builder.Environment.IsDevelopment())
    {
        var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
        System.Console.WriteLine($"MSSQL_SA_PASSWORD: {password}");
        if (connectionString != null)
        {
            connectionString = string.Format(connectionString, password);
            System.Console.WriteLine($"Connection String: {connectionString}");
        }
    }
    options.UseSqlServer(connectionString);
});

// Add Identity to the container
builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<QuizAppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? "congdinh2021@gmail.com"))
    };
});

// Set up Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day);
});

// Add Generic Repository to the container
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Add Unit of Work to the container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Services to the container
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add CORS policy with allowed origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        options.AddPolicy("AllowedOrigins", builder => builder
            .WithOrigins("http://localhost:3000", "https://localhost:3000")
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, HeaderNames.Accept, HeaderNames.XRequestedWith)
            .WithMethods("GET", "POST", "PUT", "DELETE"));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizApp Web API v1");
        options.RoutePrefix = string.Empty;
    });
}

// Error Handling:
// Implement global error handling middleware to catch and log exceptions thrown during request processing.
// Use status codes (e.g., BadRequest, NotFound, InternalServerError) to return appropriate HTTP responses for different error scenarios.
// Check each exception for specific types and return the appropriate status code and message.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var result = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = exception?.Message
        };

        if (exception is KeyNotFoundException)
        {
            result.Status = StatusCodes.Status404NotFound;
            result.Title = "The requested resource was not found.";
            result.Detail = exception.Message;
        }
        else if (exception is ArgumentException)
        {
            result.Status = StatusCodes.Status400BadRequest;
            result.Title = "The request was invalid.";
            result.Detail = exception.Message;
        }

        context.Response.StatusCode = result.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(result);
    });
});

app.UseHttpsRedirection();

app.UseRouting();

// Enable CORS using AllowedOrigins policy
app.UseCors("AllowedOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Automatically perform database migration
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<QuizAppDbContext>();
await dbContext.Database.MigrateAsync();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
SeedData.Initialize(dbContext, userManager, roleManager);

await app.RunAsync();