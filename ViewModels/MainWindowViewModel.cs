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
using Avalonia.Markup.Xaml;
using voicio.Views;
using DynamicData;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Controls.Primitives;
using System.Data.Entity.Infrastructure;

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
        private bool _IsFuzzy = false;
        private bool _IsGridEditable = false;
        private bool _IsAddButtonVisible = false;
        private bool _IsHighlighting = false;
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
        
        public bool IsHighlighting
        {
            get => _IsHighlighting;
            set => this.RaiseAndSetIfChanged(ref _IsHighlighting, value);
        }
        public bool IsFuzzy {
            get => _IsFuzzy;
            set => this.RaiseAndSetIfChanged(ref _IsFuzzy, value);
        }
        
        public bool IsAddButtonVisible
        {
            get => _IsAddButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _IsAddButtonVisible, value);
        }
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
            var invisible = new GridLength(0);
            if (IsGridEditable)
            {
                var EditOptions= new TextColumnOptions<Hint>
                {
                    BeginEditGestures = BeginEditGestures.Tap,
                    IsTextSearchEnabled = true
                };
                
                Source = new FlatTreeDataGridSource<Hint>(Hints)
                {
                    Columns =
                    {
                        new TextColumn<Hint, int>("Id", x => x.Id, options: new TextColumnOptions<Hint>{MaxWidth = invisible}),
                        new TextColumn<Hint, string>("Text", x => x.HintText, (r, v) => r.HintText = v, options: EditOptions),
                        new TextColumn<Hint, string>("Comment", x => x.Comment, (r, v) => r.Comment = v, options: EditOptions),
                        new TemplateColumn<Hint>("", new FuncDataTemplate<Hint>((a, e) => DeleteButtonInit()))
                    },
                };
                IsAddButtonVisible = true;
            }
            else
            {
                Source = new FlatTreeDataGridSource<Hint>(Hints)
                {
                    Columns =
                    {
                        new TextColumn<Hint, int>("Id", x => x.Id, options: new TextColumnOptions<Hint>{MaxWidth = invisible}),
                        new TextColumn<Hint, string>("Text", x => x.HintText),
                        new TextColumn<Hint, string>("Comment", x => x.Comment)
                    },
                };
                IsAddButtonVisible = false;
            }
            Source.Selection = new TreeDataGridCellSelectionModel<Hint>(Source);
        }
        public void StartSearch()
        {
            using (var DataSource = new HelpContext())
            {
                List<Hint> hints = new List<Hint>();
                if (IsFuzzy)
                {
                    if (IsTextSearch) hints.Add(DataSource.Hints.Where(b => b.HintText.Contains(Query)).ToList());
                    if (IsCommentSearch) hints.Add(DataSource.Hints.Where(b => b.Comment.Contains(Query)).ToList());
                } else
                {
                    if (IsTextSearch) hints.Add(DataSource.Hints.Where(b => b.HintText == Query).ToList());
                    if (IsCommentSearch) hints.Add(DataSource.Hints.Where(b => b.Comment == Query).ToList());
                }
                Hints = new ObservableCollection<Hint>(hints.Distinct());
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
