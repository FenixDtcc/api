using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Repository;
using QuantoDemoraApi.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSomee"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoLocal"));
});

builder.Services.AddScoped<IAssociadosRepository, AssociadosRepository>();
builder.Services.AddScoped<IAtendimentosRepository, AtendimentosRepository>();
builder.Services.AddScoped<IContatosRepository, ContatosRepository>();
builder.Services.AddScoped<IEspecialidadesRepository, EspecialidadesRepository>();
builder.Services.AddScoped<IHospitaisRepository, HospitaisRepository>();
builder.Services.AddScoped<IHospitalEspecialidadesRepository, HospitalEspecialidadesRepository>();
builder.Services.AddScoped<IIdentificacaoAtendimentosRepository, IdentificacaoAtendimentosRepository>();
builder.Services.AddScoped<ILogradourosRepository, LogradourosRepository>();
builder.Services.AddScoped<ITiposContatoRepository, TiposContatoRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("ConfiguracaoToken:Chave").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
