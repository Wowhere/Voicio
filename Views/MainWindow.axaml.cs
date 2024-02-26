using Avalonia;
using Avalonia.Controls;
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
        //public void MakeSaveButtonVisible(object sender, PointerPressedEventArgs e)
        //{
        //    Control grid = (Control)sender;
        //    Console.WriteLine(sender);
        //}
        //public void MakeSaveButtonVisible(object sender, PropertyChangedEventArgs e)
        //{
        //    //sender.DataContext.HintsRows
        //   Console.WriteLine(sender);
        //}
    }
}