using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Tmds.DBus.Protocol;

namespace voicio.Models
{
    public class SqliteDataSource
    {
        private string connection_string {  get; set; }

        private SqliteConnection Connection { get; set; }

        //public Dictionary<string, ObservableCollection<Hint>> GetHelp(string search_word, bool fuzzy, List<bool> flags)
        //{
        //    Dictionary<string, ObservableCollection<Hint>> search_results = new Dictionary<string, ObservableCollection<Hint>>();
        //    if (flags[0])
        //    {
        //        search_results.Add(new <string, List<string>> { });
        //        ExecQuery("SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts LEFT JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE shortcuts.shortcut LIKE (?)");
        //    }
        //    if (flags[1])
        //    {
        //        ExecQuery("SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts LEFT JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE shortcuts.comment LIKE (?)");
        //    }
        //    if (flags[2])
        //    {
        //        ExecQuery("SELECT shortcuts.id, shortcuts.shortcut, shortcuts.comment, voice_aliases.alias FROM shortcuts INNER JOIN voice_aliases ON shortcuts.alias_id = voice_aliases.id WHERE voice_aliases.alias LIKE (?)");
        //    }
        //    return search_results;
        //}

        //private List<string> ExecQuery(string query)
        //{
        //    var command = Connection.CreateCommand();
        //    command.CommandText = query;
        //    return "";
        //}

        private 

        SqliteDataSource(string conn_string="voicio.db")
        {
            connection_string = conn_string;
            Connection = new SqliteConnection(connection_string);
        }
    }

}
