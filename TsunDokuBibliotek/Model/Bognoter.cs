namespace TsundokuBibliotek.Model;

[Table(Constants.BookNoteTablename)]
public class Bognoter : BaseTable
{
    public int BookId { get; set; }
    public string Note { get; set; }
    public string Uddybning { get; set; }
    public string Reference { get; set; }
}
