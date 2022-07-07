using BidOneTest.Api.Implementations;
using BidOneTest.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add interfaces and services to the container.
builder.Services.AddScoped<IFileHandler>(fileHandler => new FileHandler(builder.Configuration["DirectoryPath"]));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// enable Cors
builder.Services.AddCors(cors =>
    cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Add controllers to the container
builder.Services.AddControllers();

// Build WebApplication Builder
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();