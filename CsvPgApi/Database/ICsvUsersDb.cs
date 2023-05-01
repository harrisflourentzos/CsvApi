using CsvApi.Model;
using CsvPgApi.Model;

namespace CsvPgApi.Database
{
    public interface ICsvUsersDb
    {
        public Task<CsvMetadata> SaveCsv(List<User> parsedCsv, string filename);
        public Task<List<CsvMetadata>> GetAllCsvs();
        public Task<CsvUsers> GetCsvUsers(Guid id);
    }
}