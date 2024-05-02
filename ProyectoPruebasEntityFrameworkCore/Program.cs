using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebasEntityFrameworkCore;
using ProyectoPruebasEntityFrameworkCore.Domain.Services;
using ProyectoPruebasEntityFrameworkCore.Domain.Services.Service;
using ProyectoPruebasEntityFrameworkCore.Helpers.PersonaValidator;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.Repository;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .AllowAnyOrigin() 
            .SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
Console.WriteLine("Configuracion swagger");
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Sistema de Personas - CETAP",
        Version = "v1",
        Description = ""
    });

    // Ruta del archivo XML de documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IValidator<PersonaDto>, PersonaValidator>();


builder.Services.AddTransient<IPersonaService, PersonaService>();
builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();

builder.Services.AddTransient<IProvinciaService, ProvinciaService>();
builder.Services.AddTransient<IProvinciaRepository, ProvinciaRepository>();
builder.Services.AddDbContext<AplicationDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
