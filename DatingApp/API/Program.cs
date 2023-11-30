using API.Errors;
using API.Middleware;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline. 
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));
// if (app.Environment.IsDevelopment())-- Remove those line of code if we don't use Swagger
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

//Add authentication middleware before the map controllers
app.UseAuthentication(); //Ask if you have a valid token
app.UseAuthorization(); //Ok you have the token, so what are you allowed to do?

app.MapControllers();

app.Run();
