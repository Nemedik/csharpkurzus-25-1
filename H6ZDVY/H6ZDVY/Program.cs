using System.Text.Json;
using System.Text.Json;

using H6ZDVY;

string path = Path.Combine(AppContext.BaseDirectory, "Deck.json");
if (!File.Exists(path))
{
    Console.WriteLine("Nem létezik");
    return;
}

try
{
    using FileStream stream = File.OpenRead(path);
    JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    List<JsonCardDto> deserialized = JsonSerializer.Deserialize<List<JsonCardDto>>(stream, options)!;

    var deck = deserialized
        .AsParallel()
        .Select(card => card.ToDomainObject())
        .Where(card => card != null)
        .ToArray();


    Random rand = new Random();
    int random = rand.Next(0, 52);
    Console.WriteLine(deck[random].Rank + " of " + deck[random].Suit + " value: " + deck[random].Value + " is drawn: " + deck[random].Drawn);

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}