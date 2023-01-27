namespace TsundokuLibrary.Repository;

public class BogRepository
{
   List<Bog> bøger = new();

   public async Task<List<Bog>> GetBøgerAsync()
   {
      if (bøger.Any())
         return bøger;

      using var stream = await FileSystem.OpenAppPackageFileAsync("books.json");
      using var reader = new StreamReader(stream);

      var indhold = await reader.ReadToEndAsync();
      bøger = JsonSerializer.Deserialize<List<Bog>>(indhold);

      return bøger;
   }
}

