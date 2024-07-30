using HrTechniqueServer.Infrastructure.Extensions.StartupExtensions;
using HrTechniqueServer.Infrastructure.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<HrtDataContext>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddMediator();
builder.Services.AddSharedTools();
builder.Services.AddClients();
builder.Services.AddRepositories();
builder.Services.AddResources();
builder.Services.AddAndConfigureCors();
builder.Services.AddServices();
builder.Services.AddAndConfigureSwagger();
builder.Services.AddOptions(builder.Configuration);

var app = builder.Build();
app.SetCors();
app.SetSwagger(app.Environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
