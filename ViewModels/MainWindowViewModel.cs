using voicio.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace voicio.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Hint> Hints { get; }

        public MainWindowViewModel()
        {
            var hints = new List<Hint>
            {
                new Hint("ipconfig", "Armstrong"),
                new Hint("Buzz", "Lightyear"),
                new Hint("James", "Kirk")
            };
            Hints = new ObservableCollection<Hint>(hints);
        }
    }
}
