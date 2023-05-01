using CsvPgApi.Database;
using CsvPgApi.Services;
using Microsoft.EntityFrameworkCore;
using static CsvPgApi.Database.CsvUsersDb;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("Users");
if (dbConnectionString == null)
    throw new Exception("Please provide a connection string for the database");

builder.Services.AddControllers();
builder.Services.AddDbContext<UsersDbContext>(opt => opt.UseSqlServer(dbConnectionString), optionsLifetime: ServiceLifetime.Singleton);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICsvParser, CsvParser>();
builder.Services.AddSingleton<ICsvUsersService, CsvUsersService>();
builder.Services.AddSingleton<ICsvUsersDb, CsvUsersDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
