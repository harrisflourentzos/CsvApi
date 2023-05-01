using CsvApi.Model;

namespace CsvPgApi.Model
{
    public class CsvUsers
    {
        public CsvMetadata Metadata { get; set; }
        public List<User> Users { get; set; }
    }
}
