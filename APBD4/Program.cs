using System.Collections;
using APBD4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var _Animals = new LinkedList<Animal>();
var _visits = new Dictionary<int, List<Visits.Visit>>();

app.MapGet("/api/Animals/{id:int}", (int id) =>
    {
        var Animal = _Animals.FirstOrDefault(s => s.Id == id);
        return Animal == null ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(Animal);
    })
    .WithName("AnimalFind")
    .WithOpenApi();

app.MapDelete("/api/Animals/{id:int}", (int id) =>
{
    if (AnimalFind(id) == null)
    {
        return Results.NoContent();
    }
    else
    {
        DeleteAnimalbyId(AnimalFind(id).Id);
        return Results.NoContent();
    }

}).WithName("DeleteAnimal").WithOpenApi();

app.MapPost("/api/Animals/", (Animal i) =>
{
    _Animals.AddFirst(i);
    return Results.StatusCode(StatusCodes.Status201Created);
}).WithName("AddStudent").WithOpenApi();

app.MapPut("/api/Animals/{id:int}", (int id, Animal Animal) =>
    {
        var studentToEdit = _Animals.FirstOrDefault(s => s.Id == id);
        if (studentToEdit == null) {
            return Results.NotFound($"Student with id {id} was not found");
        }
        _Animals.Remove(studentToEdit);
        _Animals.AddFirst(Animal);
        return Results.NoContent();
    })
    .WithName("EditAnimal")
    .WithOpenApi();
app.MapGet("/api/Animals/{id:int}/visits", (int id) =>
{
    if (!_visits.ContainsKey(id))
        return Results.NotFound();
    return Results.Ok(_visits[id]);
}).WithName("GetVisitsByAnimalId").WithOpenApi();

app.MapPost("/api/Visits", (Visits.Visit visit) =>
{
    if (!_visits.ContainsKey(visit.AnimalId))
        _visits[visit.AnimalId] = new List<Visits.Visit>();
    _visits[visit.AnimalId].Add(visit);
     return Results.StatusCode(StatusCodes.Status201Created);
}).WithName("AddVisit").WithOpenApi();

app.Run();

app.Run();

Animal AnimalFind(int id)
{
    foreach (var i in _Animals)
    {
        if (i.Id==id)
        {
            return i;
        } 
    }

    return null;
}

void DeleteAnimalbyId(int id)
{
    foreach(Animal i in _Animals)
    {
        if (i.Id==id)
        {
            _Animals.Remove(i);
        }
    }
}

