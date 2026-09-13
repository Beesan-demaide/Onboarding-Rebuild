using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=task.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/tasks", (TaskDbContext db) =>
    {
        return db.Tasks.ToList();
    });

app.MapGet("/tasks/{id}",(int id,TaskDbContext db) =>
    {
       var task = db.Tasks.FirstOrDefault(t => t.Id == id);

        if(task is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(task);
    });
app.MapPost("/tasks", (CreateRequest request, TaskDbContext db) =>
{
    var task = new Taskmodel { 
    Title = request.Title,
    IsDone = false
    };
    db.Tasks.Add(task);
    db.SaveChanges();
    return Results.Created($"/tasks/{task.Id}", task);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class CreateRequest 
{ 
    public string Title { get; set; }
}
