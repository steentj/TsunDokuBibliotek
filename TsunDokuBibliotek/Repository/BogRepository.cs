namespace TsundokuBibliotek.Repository;

public class BogRepository
{
    //TODO Skal bruge path fra Settings
    private string dbPath = Path.Combine(FileSystem.AppDataDirectory, Constants.LocalDbFile);
    private SQLiteAsyncConnection cn;

    List<Bog> bøger = new();

    private async Task Init()
    {
        if (cn != null)
            return;
          
        try
        {
            cn = new SQLiteAsyncConnection(dbPath);
            Debug.WriteLine($"dbPath = {dbPath}");
            cn.Tracer = new Action<string>(q => Debug.WriteLine(q));
            cn.Trace = true;

            await CreateTables();
            var count = await CountItems();

            if (count == 0)
            {
                await AddInitialData();
                await CheckData();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public async Task<bool> ExecuteQuery(string query)
    {
        await Init();

        var op = await cn.ExecuteAsync(query);
        return op > 0;
    }

    private async Task CreateTables()
    {
        var createTableStatements = new List<string>()
            {
                Constants.CreateBookTable,
                Constants.CreateBookNoteTable
            };

        foreach (var statement in createTableStatements)
            await ExecuteQuery(statement);
    }

    private async Task AddInitialData()
    {
        var testBøger = await GetBøgerJsonAsync();
        var commands = new List<string>();

        foreach (var bog in testBøger)
        {
            commands.Add($"INSERT INTO {Constants.BookTablename} " +
                         $"(Titel, Forfatter, BilledeLink, Synopsis, Hvorfor, Format, Status) " +
                         $"VALUES ('{bog.Titel}'," +
                         $"        '{bog.Forfatter}'," +
                         $"        '{bog.BilledeLink}'," +
                         $"        '{bog.Synopsis}'," +
                         $"        '{bog.Hvorfor}'," +
                         $"        {(int)bog.Format}," +
                         $"        {(int)bog.Status});");
        }

        foreach (var command in commands)
        {
            var op = await ExecuteQuery(command);
            Debug.WriteLine(op);
        }
    }

    private async Task CheckData()
    {
        var item = await cn.Table<Bog>().Where(v => v.Id == 1).FirstOrDefaultAsync();
        Debug.WriteLine(item?.Titel);
    }

    private async Task<int> CountItems()
    {
        var tables = new string[] { Constants.BookTablename, Constants.BookNoteTablename };
        var count = 0;

        foreach (var table in tables)
        {
            var countQuery = $"SELECT COUNT(*) FROM {table}";
            var tableCount = await CountItemsWithQuery(countQuery);
            Debug.WriteLine($"{table}: {tableCount}");
            count += tableCount;
        }

        return count;
    }

    public async Task<int> CountItemsWithQuery(string query)
    {
        await Init();

        return await cn.ExecuteScalarAsync<int>(query);
    }

    public async Task<IEnumerable<Bog>> GetBøgerAsync() 
    {
        if (bøger.Any())
            return bøger;

        await Init();

        bøger = await cn.Table<Bog>().ToListAsync();

        return bøger;
    }

    public async Task<Bog> GetBogAsync(int id) 
    {
        await Init();

        var query = cn.Table<Bog>().Where(b => b.Id == id);
        var bog = await query.FirstOrDefaultAsync();
        return bog;
    }

    private async Task<IEnumerable<Bog>> GetBøgerJsonAsync() 
    {
        if (bøger.Any())
            return bøger;

        using var stream = await FileSystem.OpenAppPackageFileAsync("books.json");
        using var reader = new StreamReader(stream);

        var indhold = await reader.ReadToEndAsync();
        bøger = JsonSerializer.Deserialize<List<Bog>>(indhold);

        await Init();
        return bøger;
    }
}

