using CompraGamer.Api.Services;
using CompraGamer.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .SetIsOriginAllowed(origin => true) // Permitir cualquier origen en desarrollo
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Register DbContext (MySQL)
var conn = builder.Configuration.GetConnectionString("GestionMicros");
builder.Services.AddDbContext<GestionMicrosContext>(opt =>
    opt.UseMySql(conn, ServerVersion.AutoDetect(conn)));

// Register application services (EF-backed)
builder.Services.AddScoped<IChicoService, EfChicoService>();
builder.Services.AddScoped<IChoferService, EfChoferService>();
builder.Services.AddScoped<IMicroEscolarService, EfMicroEscolarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(); // El middleware de CORS debe ir antes de los endpoints
app.UseAuthorization(); // Asegurarse de que la autorización esté configurada
app.MapControllers();

app.Run();
