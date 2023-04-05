namespace TsundokuBibliotek.Repository;

public class BaseTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
