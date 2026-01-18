// API

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Random r = new();

// in memory storage
List<string> data = new();

// Returns pictures of horses from wwwroot/img
// each pictures of the horses are named in this exact format
// ["1.jpg", "2.jpg", ...]
app.UseStaticFiles();

app.MapGet("/HorseList", (ILogger<Program> logger) =>
{
    var imageList = Directory
        .GetFiles("wwwroot/img")
        .Select(Path.GetFileName);
    
    logger.LogInformation("Program requested list of the horses");

    return Results.Ok(imageList);
});

app.MapGet("/random", (ILogger<Program> logger) =>
{
    var folder = "wwwroot/img";
    var files = Directory.GetFiles(folder);

    var index = Random.Shared.Next(files.Length);
    var file = Path.GetFileName(files[index]);

    logger.LogInformation("Random horse selected: {File}", file);

    return Results.Redirect($"/img/{file}");
});

// GET
app.MapGet("foo", () => "bar");

app.Run("http://0.0.0.0:5000");