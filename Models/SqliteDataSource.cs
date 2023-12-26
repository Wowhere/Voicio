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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace voicio.Models
{
    public class Tag
    {
        [Key, Required]
        public int Id { get; set; }
        public string TagText { get; set; }
        public List<HintTag> HintTag { get; } = new();
    }
    public class Hint
    {
        [Key, Required]
        public int Id { get; set; }
        public string HintText { get; set; }
        public string Comment { get; set; }
        public List<HintTag> HintTag { get; } = new();

        public Hint(int Id, string hintText, string comment)
        {
            Id = Id;
            HintText = hintText;
            Comment = comment;
        }
    }
    public class HintTag
    {
        public int Id { get; set; }
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
            //modelBuilder.Entity<Hint>().
            //    HasMany(b => b.HintTag).
            //    WithOne(b => b.Hint).
            //    HasForeignKey(b => b.HintId).
            //    HasPrincipalKey(b => b.Id);
            //modelBuilder.Entity<Tag>().
            //    HasMany(b => b.HintTag).
            //    WithOne(b => b.Tag).
            //    HasForeignKey(b => b.TagId).
            //    HasPrincipalKey(b => b.Id);

            //modelBuilder.Entity<Hint>()
            //    .HasMany(e => e.HintTag)
            //    .WithMany(e => e.Posts)
            //    .UsingEntity<PostTag>();

            base.OnModelCreating(modelBuilder);
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
