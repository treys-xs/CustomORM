using CustomORM;
using CustomORM.DataBaseRealization.PostgreSQL;
using Npgsql;
using System.ComponentModel.DataAnnotations;

var mapper = new SqlMapper();
await using var connection = new PgsqlConnectionAdapter("Host=localhost;Database=postgres;User Id=postgres;Password=123");
await connection.OpenAsync();
var sql =  """
 SELECT * FROM "Games";
 """;

foreach(var game in await mapper.MapAsync<Game>(connection, sql, CancellationToken.None))
    Console.WriteLine($"{game.Id}, {game.Name}");

public class Game 
{ 
    public int Id { get; set; }
    public string? Name { get; set; }
}
