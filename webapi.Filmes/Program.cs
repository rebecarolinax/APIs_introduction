using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o servi�o de Controller
builder.Services.AddControllers();

//Adiciona o gerador do Swagger � cole��o de servi�os na Program.cs
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Filmes API",
        Description = "API para cadastrar g�neros e filmes",
        Contact = new OpenApiContact
        {
            Name = "Rebeca Carolina",
            Url = new Uri("https://github.com/rebecarolinax")
        }       
    });

    //Configura o Swagger para usar o arquivo XML 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Adiciona mapeamento dos Controllers
var app = builder.Build();

//Habilita o middleware para atender ao documento JSON gerado e � interface do usu�rio do Swagger, tamb�m em Program.cs
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Para atender � interface do usu�rio do Swagger na raiz do aplicativo
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();