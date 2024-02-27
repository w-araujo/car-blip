using Cars.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars.Database
{
    public class DbConnection : DbContext
    {
        public DbSet<Car> Car { get; set; }
        public string DbPath { get; }

        public DbConnection(DbContextOptions<DbConnection> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "car.db");
            Console.WriteLine($"local banco de dados: {DbPath}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
