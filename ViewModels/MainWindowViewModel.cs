using ReactiveUI;
using voicio.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Avalonia;
using System;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Hint> _hints;
        public ObservableCollection<Hint> Hints { 
            get => _hints; 
            set => this.RaiseAndSetIfChanged(ref _hints, value);
        }
        private string _query;
        public string Query
        {
            get => _query;
            set => this.RaiseAndSetIfChanged(ref _query, value);

        }

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
        //public SqliteDataSource DataSource { get; set; }

        public ICommand StartSearchCommand { get; }

        public ICommand SetSearchTypeCommand { get; }

        private void SetSearchType()
        {

        }
        public void StartSearch()
        {
            using (var DataSource = new HelpContext())
            {
                var hints = DataSource.Hints.Where(b => b.HintText.Contains(Query)).ToList();
                Hints = new ObservableCollection<Hint>(hints);
            }
            
        }
        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create(StartSearch);
            SetSearchTypeCommand = ReactiveCommand.Create(SetSearchType);
            //var h = new List<Hint> { new Hint(1, "test", "comment") };
            //Hints = new ObservableCollection<Hint>(h);
        }
}
}
