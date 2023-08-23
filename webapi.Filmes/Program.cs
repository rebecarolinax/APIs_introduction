var builder = WebApplication.CreateBuilder(args);

//Adiciona o serviço de Controller
builder.Services.AddControllers();

//Adiciona mapeamento dos Controllers
var app = builder.Build();

app.MapControllers();

app.Run();