using AppLS10.api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios de la aplicación (Infrastructure, Autenticación JWT, Autorización, Swagger, Controladores)
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppLS10 API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();