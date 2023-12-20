using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace voicio.Models
{
    public class Tag
    {
        public int Id { get; set; }
    }
    public class Hints
    {
        public int Id { get; set; }
    }
    public class SqliteDataSource : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Hint> Hints { get; set; }
        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }


}
