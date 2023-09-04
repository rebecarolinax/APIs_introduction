using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Adiciona o servi�o de controllers
builder.Services.AddControllers();

//Adiciona servi�o de autentica��o(JWT Bearer)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

//Define os par�metros de valida��o do token
.AddJwtBearer("JwtBearer",options =>
{
    options.TokenValidationParameters = new TokenValidationParameters 
    { 
        //valida quem est� solicitanto
        ValidateIssuer= true,
        //valida quem est� recebendo
        ValidateAudience= true,
        //define se o tempo de expira��o do Token ser� validado, atrav�s do ValiDateLifeTime
        ValidateLifetime= true,
        //forma de criptografia e ainda a valida��o da chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),
        //valida o tempo de expira��o do Token
        ClockSkew = TimeSpan.FromMinutes(5),

        //de onde est� vindo
        ValidIssuer = "webapi.filmes.tarde",

        //para onde est� indo
        ValidAudience = "webapi.filmes.tarde"
    };
}); 

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "API para cadastrar g�neros e filmes",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Rebeca Carolina GitHub",
            Url = new Uri("https://github.com/rebecarolinax")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


    //configura��o usando a autenticacao do sweggar

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();

app.UseAuthorization();

// Mapear os controllers
app.MapControllers();
app.Run();
