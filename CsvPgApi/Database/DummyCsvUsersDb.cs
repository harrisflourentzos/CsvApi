using CsvApi.Model;
using CsvPgApi.Model;

namespace CsvPgApi.Database
{
    public class DummyCsvUsersDb : ICsvUsersDb
    {
        private readonly Dictionary<Guid, CsvUsers> _store = new();

        public async Task<List<CsvMetadata>> GetAllCsvs()
        {
            return _store.Values.Select(x => x.Metadata).ToList();
        }

        public async Task<CsvUsers> GetCsvUsers(Guid id)
        {
            return _store[id];
        }

        public async Task<CsvMetadata> SaveCsv(List<User> parsedCsv, string filename)
        {
            var metadata = new CsvMetadata 
            {
                Id = Guid.NewGuid(),
                Filename = filename,
                Timestamp = DateTime.UtcNow,
            };

            _store[metadata.Id] = new CsvUsers
            {
                Metadata = metadata,
                Users = parsedCsv
            };

            return metadata;
        }
    }
}
