using ReactiveUI;
using voicio.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Hint> Hints { get; }

        private bool _IsTextSearch;

        private bool _IsCommentSearch;

        private bool _IsTagSearch;

        private bool _IsFuzzy;
        public bool IsTextSearch {
            get => _IsTextSearch;
            set => this.RaiseAndSetIfChanged(ref _IsTextSearch, value);
        }
        public bool IsCommentSearch {
            get => _IsCommentSearch;
            set => this.RaiseAndSetIfChanged(ref _IsCommentSearch, value);
        }
        public bool IsTagSearch {
            get => _IsTagSearch;
            set => this.RaiseAndSetIfChanged(ref _IsTagSearch, value);
        }

        public bool IsFuzzy { get; set; }

        public SqliteDataSource DataSource { get; set; }

        public ICommand StartSearchCommand { get; }
        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create<string>(StartSearch);
            var hints = new List<Hint>
            {
            };
            DataSource = new SqliteDataSource();
            Hints = new ObservableCollection<Hint>(hints);
            
        }

        private void StartSearch(string query)
        {
            var SearchFlags = new List<bool> { _IsTextSearch, _IsCommentSearch, _IsTagSearch }; 
            DataSource.GetHelp(query, true, SearchFlags);

        }
    }
}
