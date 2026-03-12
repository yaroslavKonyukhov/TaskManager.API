using TaskManager.API.Interfaces;
using TaskManager.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
