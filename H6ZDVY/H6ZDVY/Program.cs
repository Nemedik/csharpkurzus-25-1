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

    Console.WriteLine("Would you like to start a round of Blackjack? (Write Y for yes)");
    string input = Console.ReadLine();
    if(input == "Y" || input == "y")
    {
        Blackjack blackjack = new Blackjack(deck);
        string teszt = blackjack.teszt();
        Console.WriteLine(teszt);
    }
    else
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}