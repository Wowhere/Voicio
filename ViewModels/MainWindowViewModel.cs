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

        private bool _IsTextSearch = true;

        private bool _IsCommentSearch = true;

        private bool _IsTagSearch;

        private bool _IsFuzzy = true;

        private bool _IsGridReadOnly = true;
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

        public bool FuzzyButtonChecked { get; set; }
        public bool IsFuzzy { get; set; }

        public bool IsGridReadOnly { get; set; }
        public bool IsReadOnly { get; set; }
        public SqliteDataSource DataSource { get; set; }

        public ICommand StartSearchCommand { get; }

        public ICommand SetSearchTypeCommand { get; }

        private void SetSearchType()
        {

        }
        private void StartSearch(string query)
        {
            
        }
        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create<string>(StartSearch);
            SetSearchTypeCommand = ReactiveCommand.Create(SetSearchType);
            var hints = new List<Hint>
            {
            };
            DataSource = new SqliteDataSource();
            Hints = new ObservableCollection<Hint>(hints);
            
        }
}
}
