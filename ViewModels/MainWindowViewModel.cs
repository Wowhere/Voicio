using ReactiveUI;
using voicio.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Avalonia;
using System;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
//using System.Speech;
using Avalonia.Controls.Selection;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Hint> _hints;
        public ObservableCollection<Hint> Hints { 
            get => _hints; 
            set => this.RaiseAndSetIfChanged(ref _hints, value);
        }

        private FlatTreeDataGridSource<Hint> _source;

        public FlatTreeDataGridSource<Hint> Source { 
            get => _source;
            set => this.RaiseAndSetIfChanged(ref _source, value);
        }

        private string _query;
        public string Query
        {
            get => _query;
            set => this.RaiseAndSetIfChanged(ref _query, value);
        } 

        private bool _IsPinnedWindow = false;
        public bool IsPinnedWindow { 
            get => _IsPinnedWindow; 
            set => this.RaiseAndSetIfChanged(ref _IsPinnedWindow, value); 
        }

        private bool _IsTextSearch = true;

        private bool _IsCommentSearch = true;

        private bool _IsTagSearch = true;

        public bool IsTextSearch
        {
            get => _IsTextSearch;
            set => this.RaiseAndSetIfChanged(ref _IsTextSearch, value);
        }
        public bool IsCommentSearch
        {
            get => _IsCommentSearch;
            set => this.RaiseAndSetIfChanged(ref _IsCommentSearch, value);
        }
        public bool IsTagSearch
        {
            get => _IsTagSearch;
            set => this.RaiseAndSetIfChanged(ref _IsTagSearch, value);
        }

        private bool _IsFuzzy = false;
        public bool IsFuzzy { 
            get => _IsFuzzy; 
            set => this.RaiseAndSetIfChanged(ref _IsFuzzy, value); 
        }

        private bool _IsGridReadOnly = true;
        
        public bool IsGridReadOnly { get; set; }

        public ICommand StartSearchCommand { get; }

        public ICommand SetSearchTypeCommand { get; }

        public void StartSearch()
        {
            using (var DataSource = new HelpContext())
            {
                List<Hint> hints;
                if (IsFuzzy)
                {
                    hints = DataSource.Hints.Where(b => b.HintText.Contains(Query)).ToList();
                } else
                {
                    hints = DataSource.Hints.Where(b => b.HintText == Query).ToList();
                }
                Hints = new ObservableCollection<Hint>(hints);
                Source = new FlatTreeDataGridSource<Hint>(Hints)
                {
                    Columns =
                {
                    new TextColumn<Hint, int>("Id", x => x.Id),
                    new TextColumn<Hint, string>("Text", x => x.HintText, (r, v) => r.HintText = v),
                    new TextColumn<Hint, string>("Comment", x => x.Comment, (r, v) => r.Comment = v)
                },
                };
                Source.Selection = new TreeDataGridCellSelectionModel<Hint>(Source);

            }
            
        }
        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create(StartSearch);
        }
}
}
