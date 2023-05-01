using Microsoft.EntityFrameworkCore;

namespace CsvPgApi.Database.Model
{
    [PrimaryKey("Id")]
    public class DbUser
    {
        public Guid CsvId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string EmailAddress { get; set; }
        public string Department { get; set; }
        public string ContactType { get; set; }
        public string CompanyName { get; set; }
        public string BussinessPhone { get; set; }

        public string AddressStreet1 { get; set; }
        public string AddressStreet2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressPostCode { get; set; }
        public string AddressCountry { get; set; }
    }
}
