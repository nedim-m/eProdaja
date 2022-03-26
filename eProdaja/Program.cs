using eProdaja.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProizvodiService, ProizvodiService>();// svaki put kada se pozove metoda desi se _proizvodiService= new ProizvodiService()
//builder.Services.AddScoped<IProizvodiService, ProizvodiService>(); //
//builder.Services.AddSingleton<IProizvodiService, ProizvodiService>(); // kada se pokrene metoda ne adi svaki put instaniciranje novog ProizvodServica
                                                                       // radi performansi, za stvari koje se ne mjenjaju skoro nikako npr Opstine, uglv. staticke liste
                                                                      //Jednom ucitat i to je to.
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
