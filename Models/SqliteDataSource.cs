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
using System.Data.Entity.Hierarchy;
using Avalonia.Controls;

namespace voicio.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagText { get; set; }
        public List<HintTag> HintTag { get; } = new();
    }
    public class Hint
    {
        public int Id { get; set; }
        public string HintText { get; set; }
        public int Comment { get; set; }
        public List<HintTag> HintTag { get; } = new();
    }
    public class HintTag
    {
        public int TagId { get; set; }
        public int HintId { get; set; }
        public Tag Tag { get; set; } = null!;
        public Hint Hint { get; set; } = null!;
    }
    public class HelpContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Hint> Hints { get; set; }
        public string DbPath { get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hint>()
                .Property(b => b.HintTag)
                .HasDefaultValue(null);
            modelBuilder.Entity<Tag>()
                .Property(b => b.HintTag)
                .HasDefaultValue(null);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        public HelpContext()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            DbPath = System.IO.Path.Join(path, "helper.db");
        }
    }


}
