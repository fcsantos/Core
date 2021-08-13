using System.Collections.ObjectModel;

namespace ProjetoMRP.Paciente.ViewModel
{
    public class HomeViewModel
    {

        public ObservableCollection<Menu> MenuItems { get; set; }

        public HomeViewModel()
        {
            MenuItems = new ObservableCollection<Menu>();
        }

        public class Menu
        {
            public string Text { get; set; }
            public string NumberOf { get; set; }
            public string Image { get; set; }
            public string Page { get; set; }
        }

    }

}
