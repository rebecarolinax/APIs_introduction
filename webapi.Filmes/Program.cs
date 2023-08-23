var builder = WebApplication.CreateBuilder(args);

//Adiciona o servi�o de Controller
builder.Services.AddControllers();

//Adiciona mapeamento dos Controllers
var app = builder.Build();

app.MapControllers();

app.Run();