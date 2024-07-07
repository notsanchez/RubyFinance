using Microsoft.EntityFrameworkCore;
using RubyFinance.Api.Data;
using RubyFinance.Api.Handlers;
using RubyFinance.Api.Request.Auth;
using RubyFinance.Api.Request.Category;
using RubyFinance.Api.Response.Auth;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<AuthHandler>();
builder.Services.AddScoped<CategoryHandler>();
builder.Services.AddScoped<TransactionHandler>();
builder.Services.AddSingleton<JWTHandler>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Authentication [Auth Routes]

app.MapPost("/v1/auth/login", async (LoginRequest request, AuthHandler authHandler) =>
{
    var response = await authHandler.Login(request);
    return Results.Ok(response);
});

app.MapPost("/v1/auth/register", async (RegisterRequest request, AuthHandler authHandler) =>
{
    var response = await authHandler.Register(request);
    return Results.Ok(response);
});

// Categories [Authenticated Routes]

app.MapPost("/v1/categories", async (CreateCategoryRequest request, HttpContext httpContext, CategoryHandler categoryHandler) =>
{
    var token = httpContext.Request.Headers["Authorization"];
    var response = await categoryHandler.CreateCategory(request, token);
    return Results.Ok(response);
});

app.MapGet("/v1/categories", async (HttpContext httpContext, CategoryHandler categoryHandler) =>
{
    var token = httpContext.Request.Headers["Authorization"];
    var response = await categoryHandler.ListCategories(token);
    return Results.Ok(response);
});

app.MapGet("/v1/categories/{id}", async (string id, HttpContext httpContext, CategoryHandler categoryHandler) =>
{
    var token = httpContext.Request.Headers["Authorization"];
    var response = await categoryHandler.GetCategoryById(id, token);
    return Results.Ok(response);
});
app.MapDelete("/v1/categories/{id}", async (string id, HttpContext httpContext, CategoryHandler categoryHandler) =>
{
    var token = httpContext.Request.Headers["Authorization"];
    var response = await categoryHandler.DeleteCategoryById(id, token);
    return Results.Ok(response);
});

// Transactions [Authenticated Routes]

app.MapPost("/v1/transactions", () => "Hello World!");
app.MapGet("/v1/transactions", () => "Hello World!");
app.MapGet("/v1/transactions/{id}", (string id) => "Hello World!");
app.MapDelete("/v1/transactions/{id}", (string id) => "Hello World!");

// -- [Open Routes]

app.Run();
