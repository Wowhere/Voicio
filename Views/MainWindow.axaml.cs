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
        public async void CopyHintToClipboard(object sender, TappedEventArgs e)
        {
            if (e.Source.GetType() == typeof(Avalonia.Controls.TextBlock))
            {
                TreeDataGrid c = (TreeDataGrid)sender;
                var clipboard = TopLevel.GetTopLevel(c)?.Clipboard;
                var dataObject = new DataObject();

                dataObject.Set(DataFormats.Text, ((Avalonia.Controls.TextBlock)e.Source).Text);
                await clipboard.SetDataObjectAsync(dataObject);
            }
            
        }
        //public void MakeSaveButtonVisible(object sender, PropertyChangedEventArgs e)
        //{
        //    //sender.DataContext.HintsRows
        //   Console.WriteLine(sender);
        //}
    }
}