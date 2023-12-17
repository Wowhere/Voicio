using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace voicio.Models
{
    public class SqliteDataSource
    {
        private string connection_string {  get; set; }

        private SqliteConnection Connection { get; set; }

        public Dictionary<string, ObservableCollection<Hint>> GetHelp(string searchWord, bool Fuzzy, List<bool> Flags)
        {
            Dictionary<string, ObservableCollection<Hint>> Results = new Dictionary<string, ObservableCollection<Hint>>();
            if (Flags[0])
            {
                var foundByText = "SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts LEFT JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE shortcuts.shortcut LIKE $searchWord";
                Results.Add("Text", ExecQuery(foundByText, searchWord));
            }
            if (Flags[1])
            {
                var foundByComment = "SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts LEFT JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE shortcuts.comment LIKE $searchWord";
                Results.Add("Comment", ExecQuery(foundByComment, searchWord));
            }
            if (Flags[2])
            {
                var foundByTags = "SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts INNER JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE voice_aliases.alias LIKE $searchWord";
                Results.Add("Tags", ExecQuery(foundByTags, searchWord));
            }
            return Results;
        }

        private ObservableCollection<Hint> ExecQuery(string query, string searchWord)
        {
            var command = Connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("$searchWord", searchWord);

            var Hints = new ObservableCollection<Hint>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //var name = reader.Get
                    Hints.Add(new Hint(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    Console.WriteLine($"Hello, {reader.GetString(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}!");
                }
            }
            return Hints;
        }

        public SqliteDataSource(string conn_string= "Data Source=voicio.db;Cache=Shared")
        {
            connection_string = conn_string;
            Connection = new SqliteConnection(connection_string);
            Connection.Open();
        }
    }

}
