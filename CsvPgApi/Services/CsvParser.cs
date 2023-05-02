using CsvApi.Model;
using System.Reflection.Metadata.Ecma335;

namespace CsvPgApi.Services
{
    public class CsvParser : ICsvParser
    {
        public List<User> Parse(string csv)
        {
            var lines = csv.Split('\n');
            var header = lines[0];
            var rows = lines[1..];
            var fieldIndexes = ParseLine(header).Select((f, i) => (f, i)).ToDictionary(x => x.f, x => x.i);

            var parsed = ParseRows(fieldIndexes, rows).ToList();

            return parsed;
        }

        private static List<string> ParseLine(string line)
        {
            var result = new List<string>();
            var strings = line.Split(',').ToList();

            // Join back together values that have wrongly split on the comma like "17, Some Street"
            foreach (var s in strings)
            {

                if (s.StartsWith('"') & !s.EndsWith('"'))
                {
                    result.Add(RemoveQuotesAndTrim(s) + " " + RemoveQuotesAndTrim(strings[strings.IndexOf(s) + 1]));
                }
                else if (!s.StartsWith('"') & s.EndsWith('"'))
                {
                    continue;
                }
                else
                {
                    result.Add(RemoveQuotesAndTrim(s));
                }

            }

            return result;
        }

        private static string RemoveQuotesAndTrim(string input)
        {
            return input.Replace("\"", "").Trim();
        }

        private static IEnumerable<User> ParseRows(Dictionary<string, int> fieldIndexes, string[] rows)
        {
            foreach (var row in rows)
            {
                var parsed = ParseLine(row);
                if (parsed.Count < fieldIndexes.Count) continue;
                yield return new User
                {
                    FirstName = parsed[fieldIndexes["first_name"]],
                    LastName = parsed[fieldIndexes["last_name"]],
                    JobTitle = parsed[fieldIndexes["job_title"]],
                    EmailAddress = parsed[fieldIndexes["emailaddress1"]],
                    Department = parsed[fieldIndexes["department"]],
                    ContactType = parsed[fieldIndexes["contact_type"]],
                    CompanyName = parsed[fieldIndexes["company_name"]],
                    BussinessPhone = parsed[fieldIndexes["business_phone"]],
                    Street1 = parsed[fieldIndexes["address1_street1"]],
                    Street2 = parsed[fieldIndexes["address1_street2"]],
                    City = parsed[fieldIndexes["address1_city"]],
                    PostCode = parsed[fieldIndexes["address1_postalcode"]],
                    Country = parsed[fieldIndexes["address1_country"]],
                };
            }
        }
    }
}
