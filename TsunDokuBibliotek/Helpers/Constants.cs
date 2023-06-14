namespace TsundokuBibliotek.Helpers
{
	public class Constants
	{
		public const string LocalDbFile = "tsundoku_v01.db";
		public const string BookTablename = "book";
		public const string BookNoteTablename = "booknotes";
		public const string DefaultBookImage = "defaultbook.png";

		public static string CreateBookTable =
			$"CREATE TABLE IF NOT EXISTS {BookTablename} " +
			"(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
			" Forfatter VARCHAR(255)," +
			" Titel VARCHAR(255)," +
			" Billedelink VARCHAR(512)," +
			" Synopsis VARCHAR(2048)," +
			" Hvorfor VARCHAR(2048)," +
			" Status VARCHAR(32)," +
			" Format VARCHAR(32));";

		public static string CreateBookNoteTable =
			$"CREATE TABLE IF NOT EXISTS {BookNoteTablename} " +
			"(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
            " BookId INT, " +
            " Note VARCHAR(2048), " +
            " Uddybning VARCHAR(2048), " +
            " Reference VARCHAR(256), " +
            $"FOREIGN KEY(BookId) REFERENCES {BookTablename}(Id));";
	}
}

