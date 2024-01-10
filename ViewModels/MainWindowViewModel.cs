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
using Avalonia.Controls.Templates;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Hint>? _hints;
        public ObservableCollection<Hint>? Hints {
            get => _hints;
            set => this.RaiseAndSetIfChanged(ref _hints, value);
        }

        private FlatTreeDataGridSource<Hint>? _source;

        public FlatTreeDataGridSource<Hint>? Source {
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

        private bool _IsGridEditable = false;

        public bool IsGridEditable { 
            get => _IsGridEditable;
            set {
                this.RaiseAndSetIfChanged(ref _IsGridEditable, value);
                TreeDataGridInit();
            } 
        }
        public ICommand StartSearchCommand { get; }
        public ICommand DeleteHintCommand { get; }

        public void AddHint(Hint h) {
            using (var DataSource = new HelpContext())
            {
                DataSource.Hints.Add(h);
            }    
            Hints.Add(h);
        }
        public void DeleteHint(Hint h)
        {
            using (var DataSource = new HelpContext())
            {
                DataSource.Hints.Remove(h);
            }
            Hints.Remove(h);
        }
        public Button DeleteButtonInit()
        {
            Button b = new Button();
            b.Content = "Delete";
            b.Command = DeleteHintCommand;
            return b;
        }
        public void TreeDataGridInit()
        {
            if (IsGridEditable)
            {
                Source = new FlatTreeDataGridSource<Hint>(Hints)
                {
                    Columns =
                    {
                        new TextColumn<Hint, int>("Id", x => x.Id),
                        new TextColumn<Hint, string>("Text", x => x.HintText, (r, v) => r.HintText = v),
                        new TextColumn<Hint, string>("Comment", x => x.Comment, (r, v) => r.Comment = v),
                        new TemplateColumn<Hint>("", new FuncDataTemplate<Hint>((a, e) => DeleteButtonInit()))
                    },
                };
            } else
            {
                Source = new FlatTreeDataGridSource<Hint>(Hints)
                {
                    Columns =
                    {
                        new TextColumn<Hint, int>("Id", x => x.Id),
                        new TextColumn<Hint, string>("Text", x => x.HintText),
                        new TextColumn<Hint, string>("Comment", x => x.Comment)
                    },
                };
            }
            Source.Selection = new TreeDataGridCellSelectionModel<Hint>(Source);
        }
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
                TreeDataGridInit();
            } 
        }
        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create(StartSearch);
            DeleteHintCommand = ReactiveCommand.Create<Hint>(DeleteHint);
            TreeDataGridInit();
        }
}
}
