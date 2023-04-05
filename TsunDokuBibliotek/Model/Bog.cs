namespace TsundokuBibliotek.Model;

[Table(Constants.BookTablename)]
public class Bog : BaseTable
{
    public string Forfatter { get; set; }
    public string Titel { get; set; }
    public string BilledeLink { get; set; }
    public string Synopsis { get; set; }
    public string Hvorfor { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Format Format { get; set; }
}

public enum Status
{
   Købt,
   Igang,
   Læst
}

public enum Format
{
   Papirbog,
   Ebog,
   Lydbog
}

