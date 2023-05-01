using CsvPgApi.Database;
using CsvPgApi.Model;

namespace CsvPgApi.Services
{
    public class CsvUsersService : ICsvUsersService
    {
        private readonly ICsvUsersDb _db;
        private readonly ICsvParser _parser;

        public CsvUsersService(ICsvParser csvParser, ICsvUsersDb csvUsersDb)
        {
            _parser = csvParser;
            _db = csvUsersDb;
            
        }

        public async Task<List<CsvMetadata>> GetAllCsvs()
        {
            var result = await _db.GetAllCsvs();

            return result;
        }

        public async Task<CsvUsers> GetCsvUsers(Guid guid)
        {
            var result = await _db.GetCsvUsers(guid);

            return result;
        }

        public async Task<CsvMetadata> ProcessCsv(string filename, string csv)
        {
            var parsedCsv = _parser.Parse(csv);

            var result = await _db.SaveCsv(parsedCsv, filename);

            return result;
        }
    }
}
