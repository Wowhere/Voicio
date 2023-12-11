using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace voicio.Models
{
    public class SearchQuery
    {
        private string _query;
        public string Query { get; set; }
        
    }

    public class Hint
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string? Comment { get; set; }

        public List<string>? Tags { get; set; }

        public Hint(int id, string text, string comment)
        {
            Id = id; Text = text; Comment = comment;
        }
        public Hint(int id, string text, string comment, List<string> tags) {
            Id = id; Text = text; Comment = comment; Tags = tags;
        }
    }
}
