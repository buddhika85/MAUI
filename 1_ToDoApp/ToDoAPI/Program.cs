using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnetion")));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.MapGet("api/todo", async(AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("api/todo", async (AppDbContext context, ToDo todo) =>
{
    await context.ToDos.AddAsync(todo);
    await context.SaveChangesAsync();
    return Results.Created($"api/todo/{todo.Id}", todo);
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id, ToDo todo) =>
{
    var item = await context.ToDos.SingleOrDefaultAsync(x => x.Id == id);
    if (item == null)
    {
        return Results.NotFound($"To Do with ID {id} not found");
    }

    // update
    item.ToDoName = todo.ToDoName;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/todo/{id}", async (AppDbContext context, int id) =>
{
    var item = await context.ToDos.SingleOrDefaultAsync(x => x.Id == id);
    if (item == null)
    {
        return Results.NotFound($"To Do with ID {id} not found");
    }

    // delete
    context.ToDos.Remove(item);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
