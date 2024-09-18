using Webapidemo.api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Gamedto> games = [

new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 39.99M, new DateOnly(1998, 11, 21)),
new (3, "Super Mario 64", "Platformer", 29.99M, new DateOnly(1996, 9, 29)),
new (4, "Final Fantasy VII", "RPG", 49.99M, new DateOnly(1997, 1, 31)),
new (5, "Metal Gear Solid", "Action", 24.99M, new DateOnly(1998, 9, 3)),


];
//GET /games
app.MapGet("games", () => games);

//GET /games
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

//POST /game
app.MapPost("games", (CreateGameDto newGame) =>
{

    Gamedto game = new(games.Count + 1, 
                       newGame.Name, 
                       newGame.Genre, 
                       newGame.Price, 
                       newGame.ReleaseDate);
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

//PUT /games
app.MapPut("games/{id}", (int id, UpdateGameDto updateGame)=>
{
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new Gamedto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );
    return Results.NoContent();
});

//DELETE /games
app.MapDelete("games/{id}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

app.Run();
