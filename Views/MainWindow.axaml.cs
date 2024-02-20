using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Linq;

namespace voicio.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MakeSaveButtonVisible(object sender, TextInputEventArgs e)
        {
            //sender.DataContext.HintsRows
            Console.WriteLine(sender);
        }
    }
}