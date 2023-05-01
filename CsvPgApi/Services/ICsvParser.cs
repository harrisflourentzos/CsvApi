using CsvApi.Model;

namespace CsvPgApi.Services
{
    public interface ICsvParser
    {
        public List<User> Parse(string csv);
    }
}