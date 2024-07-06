using authServer.Extensions.StartupExtensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<authServer.Data.AuthDataContext>();
builder.Services.AddServices();
builder.Services.AddContextData();
builder.Services.AddRepositories();
builder.Services.AddOptions(builder.Configuration);
builder.Services.RegisterHealthChecks();
builder.Services.AddAndConfigureSwagger();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddQuartzCron(builder.Configuration["JwtOptions:TokenExpiryMinutes"]!);
builder.Services.AddAuth(builder.Configuration["JwtOptions:SecretTokenKey"]!);
builder.Services.AddAndConfigureCors();
builder.Services.AddSharedTools();

var app = builder.Build();
app.RunHealthChecks();
app.RunCors();

app.UseForwardedHeaders(
    new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    }
);

app.RunSwagger(builder.Environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
