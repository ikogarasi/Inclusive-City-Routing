using Asp.Versioning;
using InclusiveCity.Infrastructure.Extensions;
using InclusiveCity.Infrastructure.Extenstions;
using InclusiveCity.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAnyOrigins", _ =>
    {
        _.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Inclusive City API";
    options.Version = "v1";
    options.Description = "API for Inclusive City project, providing access to structures and their details.";
    options.DocumentName = "v1";
    options.PostProcess = document =>
    {
        document.Info.Version = "v1.0";
        document.Info.Title = "Inclusive City API";
        document.Info.Description = "API for Inclusive City project, providing access to structures and their details.";
    };
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
builder.Services.ConfigureDbContext(configuration);
builder.Services.AddMediatr();
builder.Services.AddInfrastractureServices();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper();
builder.Services.AddAzureBlobStorage();

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseCors("AllowAnyOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
