using ReactiveUI;
using voicio.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
//using System.Speech;
using Avalonia.Controls.Selection;
using Avalonia.Controls.Templates;
using DynamicData;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.IO;
using System;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Hint>? _HintsRows;
        public ObservableCollection<Hint>? HintsRows
        {
            get => _HintsRows;
            set => this.RaiseAndSetIfChanged(ref _HintsRows, value);
        }
        private FlatTreeDataGridSource<Hint>? _HintsGridData;
        public FlatTreeDataGridSource<Hint>? HintsGridData
        {
            get => _HintsGridData;
            set => this.RaiseAndSetIfChanged(ref _HintsGridData, value);
        }
        private string _query;
        public string Query
        {
            get => _query;
            set => this.RaiseAndSetIfChanged(ref _query, value);
        }
        private string? _StatusText;
        public string? StatusText
        {
            get => _StatusText;
            set => this.RaiseAndSetIfChanged(ref _StatusText, value);
        }
        private bool _IsPinnedWindow = false;
        public bool IsPinnedWindow
        {
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
        public bool IsFuzzy
        {
            get => _IsFuzzy;
            set => this.RaiseAndSetIfChanged(ref _IsFuzzy, value);
        }
        public bool IsAddButtonVisible
        {
            get => _IsAddButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _IsAddButtonVisible, value);
        }
        public bool IsGridEditable
        {
            get => _IsGridEditable;
            set
            {
                this.RaiseAndSetIfChanged(ref _IsGridEditable, value);
                TreeDataGridInit();
            }
        }
        public ICommand StartSearchCommand { get; }
        public ICommand StartVoiceSearchCommand { get; }
        private void RemoveHint(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Hint SavedHint = (Hint)b.DataContext;
            HintsRows.Remove(SavedHint);
            SavedHint.Remove();
        }
        private void SaveHint(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Hint SavedHint = (Hint)b.DataContext;
            SavedHint.Update();
        }
        public void AddHint()
        {
            Hint NewHint = new Hint();
            HintsRows.Add(NewHint);
            NewHint.Add();
        }
        private Button SaveButtonInit()
        {
            var b = new Button();
            b.Background = new SolidColorBrush() { Color = new Color(255, 34, 139, 34) };
            b.Content = "Save";
            b.Click += SaveHint;
            return b;
        }
        private Button RemoveButtonInit()
        {
            var b = new Button();
            b.Background = new SolidColorBrush() { Color = new Color(255, 80, 00, 20) };
            b.Content = "Remove";
            b.Click += RemoveHint;
            return b;
        }
        private DockPanel ButtonsPanelInit()
        {
            var panel = new DockPanel();
            panel.Children.Add(SaveButtonInit());
            panel.Children.Add(RemoveButtonInit());
            panel.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            return panel;
        }

        public void TreeDataGridInit()
        {
            var TextColumnLength = new GridLength(1, GridUnitType.Star);
            var TemplateColumnLength = new GridLength(0.4, GridUnitType.Star);

            if (IsGridEditable)
            {
                var EditOptions = new TextColumnOptions<Hint>
                {
                    BeginEditGestures = BeginEditGestures.Tap,
                    IsTextSearchEnabled = true,
                    
                };
                TextColumn<Hint, string> HintTextColumn = new TextColumn<Hint, string>("Text", x => x.HintText, (r, v) => r.HintText = v, options: EditOptions, width: TextColumnLength);
                TextColumn<Hint, string> HintCommentColumn = new TextColumn<Hint, string>("Comment", x => x.Comment, (r, v) => r.Comment = v, options: EditOptions, width: TextColumnLength);
                HintsGridData = new FlatTreeDataGridSource<Hint>(HintsRows)
                {
                    Columns =
                    {
                        HintTextColumn,
                        HintCommentColumn,
                        new TemplateColumn<Hint>("", new FuncDataTemplate<Hint>((a, e) => ButtonsPanelInit(), supportsRecycling: true), width: TemplateColumnLength),
                        //new TemplateColumn<Hint>("", new FuncDataTemplate<Hint>((a, e) => SaveButtonInit(), supportsRecycling: true), width: TemplateColumnLength),
                        //new TemplateColumn<Hint>("", new FuncDataTemplate<Hint>((a, e) => RemoveButtonInit(), supportsRecycling: true), width: TemplateColumnLength)
                    },
                };
                IsAddButtonVisible = true;
            }
            else
            {
                HintsGridData = new FlatTreeDataGridSource<Hint>(HintsRows)
                {
                    Columns =
                    {
                        new TextColumn<Hint, string>("Text", x => x.HintText, width: TextColumnLength),
                        new TextColumn<Hint, string>("Comment", x => x.Comment, width: TextColumnLength)
                    },
                };
                IsAddButtonVisible = false;
                
            }
            HintsGridData.Selection = new TreeDataGridCellSelectionModel<Hint>(HintsGridData);
        }
        public void StartVoiceSearch()
        {
            var recorder = new NAudioRecorder();
            var recognition = new SpeechRecognition(".\\voice_model");
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
                }
                else
                {
                    if (IsTextSearch) hints.Add(DataSource.Hints.Where(b => b.HintText == Query).ToList());
                    if (IsCommentSearch) hints.Add(DataSource.Hints.Where(b => b.Comment == Query).ToList());
                }
                HintsRows = new ObservableCollection<Hint>(hints.Distinct());
            }
            TreeDataGridInit();
        }

        public MainWindowViewModel()
        {
            StartSearchCommand = ReactiveCommand.Create(StartSearch);
            StartVoiceSearchCommand = ReactiveCommand.Create(StartVoiceSearch);
            HintsRows = new ObservableCollection<Hint>();
            TreeDataGridInit();
        }
    }
}