using CsvApi.Model;
using CsvPgApi.Database.Model;
using CsvPgApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CsvPgApi.Database
{
    public class CsvUsersDb : ICsvUsersDb
    {
        private readonly DbContextOptions<UsersDbContext> _options;

        public CsvUsersDb(DbContextOptions<UsersDbContext> options)
        {
            _options = options;
        }

        public async Task<List<CsvMetadata>> GetAllCsvs()
        {
            using var context = new UsersDbContext(_options);

            var dbMetadata = context.Metadata.ToList();

            var metadata = dbMetadata.Select(x => new CsvMetadata
            {
                Id = x.Id,
                Filename = x.Filename,
                Timestamp = x.Timestamp,
            }).ToList();

            return metadata;
        }

        public async Task<CsvUsers> GetCsvUsers(Guid id)
        {
            using var context = new UsersDbContext(_options);

            var dbMetadata = context.Metadata.First(x => x.Id == id);

            var metadata = new CsvMetadata
            {
                Id = id,
                Filename = dbMetadata.Filename,
                Timestamp = dbMetadata.Timestamp,
            };

            var dbUsers = context.Users.Where(x => x.CsvId == id);

            var users = dbUsers.Select(ToUser).ToList();

            return new CsvUsers
            {
                Metadata = metadata,
                Users = users
            };
        }

        public async Task<CsvMetadata> SaveCsv(List<User> parsedCsv, string filename)
        {
            using var context = new UsersDbContext(_options);

            var id = Guid.NewGuid();
            
            var metadata = new CsvMetadata
            {
                Id = id,
                Filename = filename,
                Timestamp = DateTime.UtcNow
            };

            var dbMetadata = new DbCsvMetadata
            {
                Id = id,
                Filename = filename,
                Timestamp = metadata.Timestamp
            };

            var dbUsers = parsedCsv.Select(x => FromUser(x, id));

            await context.Users.AddRangeAsync(dbUsers);
            await context.Metadata.AddAsync(dbMetadata);

            await context.SaveChangesAsync();

            return metadata;
        }

        private static User ToUser(DbUser dbUser) => new()
        {
            FirstName = dbUser.FirstName,
            LastName = dbUser.LastName,
            JobTitle = dbUser.JobTitle,
            EmailAddress = dbUser.EmailAddress,
            Department = dbUser.Department,
            ContactType = dbUser.ContactType,
            CompanyName = dbUser.CompanyName,
            BussinessPhone = dbUser.BussinessPhone,
            Street1 = dbUser.AddressStreet1,
            Street2 = dbUser.AddressStreet2,
            City = dbUser.AddressCity,
            PostCode = dbUser.AddressPostCode,
            Country = dbUser.AddressCountry,
        };

        private static DbUser FromUser(User user, Guid id) => new()
        {
            CsvId = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            JobTitle = user.JobTitle,
            EmailAddress = user.EmailAddress,
            Department = user.Department,
            ContactType = user.ContactType,
            CompanyName = user.CompanyName,
            BussinessPhone = user.BussinessPhone,
            AddressStreet1 = user.Street1,
            AddressStreet2 = user.Street2,
            AddressCity = user.City,
            AddressPostCode = user.PostCode,
            AddressCountry = user.Country,
        };

        public class UsersDbContext : DbContext
        {
            public DbSet<DbCsvMetadata> Metadata { get; set; }
            public DbSet<DbUser> Users { get; set; }

            public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
            {

            }
        }
    }
}
