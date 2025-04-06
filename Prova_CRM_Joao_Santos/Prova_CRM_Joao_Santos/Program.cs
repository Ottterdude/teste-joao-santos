using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Domain.Interfaces;
using Prova_CRM_Joao_Santos.Infra;
using Prova_CRM_Joao_Santos.Infrastructure.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VestibularContext>(options =>
{
    options.UseSqlServer("Server=host.docker.internal,1400;Database=CRM;User Id=sa;Password=J@o123456;TrustServerCertificate=True;");
});

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<IOfertaRepository, OfertaRepository>();
builder.Services.AddScoped<IProcessoSeletivoRepository, ProcessoSeletivoRepository>();
builder.Services.AddScoped<IInscricaoRepository, InscricaoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
