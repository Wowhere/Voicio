using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Data.Sqlite;

namespace voicio.Models
{
    public class Hint
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Comment { get; set; }

        public string? Tags { get; set; }

        public Hint(string text, string comment) { 
            Text = text; Comment = comment;
        }
    }
}
