using System.Windows;
using ToDoList.ViewModel;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Přidáme ViewModel jako DataContext
            this.DataContext = new MainViewModel();
        }
    }
}
