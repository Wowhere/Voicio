using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Avalonia;

namespace voicio.Models
{
    public class Tag
    {
        [Key, Required]
        public int Id { get; set; }
        public string? TagText { get; set; }
        public List<HintTag> HintTag { get; } = new();
    }
    public class Hint
    {
        [Key, Required]
        public int Id { get; set; }
        public string HintText { get; set; }
        public string Comment { get; set; }
        public List<HintTag> HintTag { get; } = new();
        public void Remove(int id)
        {
            var removed = this.Id;
            using (var DataSource = new HelpContext())
            {
                DataSource.Hints.Attach(this);
                DataSource.Hints.Remove(this);
                DataSource.SaveChanges();
            }
        }

        public void Add()
        {
            using (var DataSource = new HelpContext())
            {
                //DataSource.Hints.Add();
            }
        }
        public Hint(int id)
        {
            Id = id;
        }
        public Hint(int id, string hintText, string comment)
        {
            Id = id;
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
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Hint>? Hints { get; set; }
        public string DbPath { get; }
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