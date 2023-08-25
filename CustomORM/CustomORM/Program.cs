using CustomORM;
using CustomORM.DataBaseContext;
using CustomORM.DataBaseRealization.PostgreSQL;

var mapper = new SqlMapper();
await using var connection = new PgsqlConnectionAdapter("Host=localhost;Database=postgres;User Id=postgres;Password=123");
await connection.OpenAsync();
var context = new TestDbContext(connection);
var result = context.Games.Where(x => x.Id > 1).Select(x => new Game { Id = x.Id, Name = x.Name });
var results = result.ToArray();
foreach (var item in results)
    Console.WriteLine($"{item.Name}, {item.Id}");

public class Game 
{ 
    public int Id { get; set; }
    public string? Name { get; set; }
}
