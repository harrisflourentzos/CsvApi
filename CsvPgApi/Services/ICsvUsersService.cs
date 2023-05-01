using CsvPgApi.Model;

namespace CsvPgApi.Services
{
    public interface ICsvUsersService
    {
        public Task<CsvMetadata> ProcessCsv(string filename, string csv);
        public Task<CsvUsers> GetCsvUsers(Guid guid);
        public Task<List<CsvMetadata>> GetAllCsvs();

    }
}