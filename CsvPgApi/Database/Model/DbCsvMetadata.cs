using Microsoft.EntityFrameworkCore;

namespace CsvPgApi.Database.Model
{
    [PrimaryKey("Id")]
    public class DbCsvMetadata
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
