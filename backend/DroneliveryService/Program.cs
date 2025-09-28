using DroneliveryService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSingleton<MapaService>(new MapaService(100, 100));
builder.Services.AddSingleton<PathFinderService>();
builder.Services.AddSingleton<GerenciadorDeFrotasService>();



var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();



app.Run();