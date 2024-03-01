using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Remote.Protocol.Input;
using Avalonia.VisualTree;
using System;
using System.ComponentModel;
using System.Linq;

namespace voicio.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void CopyHintToClipboard(object sender, TappedEventArgs e)
        {
            TreeDataGridRow c = (TreeDataGridRow)sender;
            var clipboard = TopLevel.GetTopLevel((TreeDataGridCell)sender)?.Clipboard;
            var dataObject = new DataObject();
            dataObject.Set(DataFormats.Text, c.DataContext.ToString);
        }
        //public void MakeSaveButtonVisible(object sender, PropertyChangedEventArgs e)
        //{
        //    //sender.DataContext.HintsRows
        //   Console.WriteLine(sender);
        //}
    }
}