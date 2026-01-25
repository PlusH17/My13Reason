using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.MVVM;

namespace ToDoList.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        // Tento řádek vytváří příkaz pro tlačítko „Přidat“
        // RelayCommand říká: "když někdo klikne, zavolej tuto metodu"
        // execute => AddItem() znamená: ignorujeme parametr (exekuce), jen zavoláme metodu AddItem()
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());

        // Tento řádek vytváří příkaz pro tlačítko „Smazat“
        // Tlačítko se spustí jen tehdy, pokud je vybraná nějaká položka (SelectedItem != null)
        // execute => DeleteItem() říká, co se má stát po kliknutí
        // canExecute => SelectedItem != null říká, zda je tlačítko aktivní
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);

        // '=>' je jen zkrácený zápis pro malou funkci / metodu, kterou předáváme jako parametr.


        public ObservableCollection<Duty> Duties { get; set; }
        public MainViewModel()
        {
            Duties = new ObservableCollection<Duty>();


        }

        private Duty selectedItem;


        public Duty SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private void AddItem()
        {
            Duties.Add(new Duty()
            {
                Name = "ProductX",
                Term = DateTime.Now.AddDays(7),
            });
        }

        private void DeleteItem()
        {
            Duties.Remove(SelectedItem);
        }


    }
}

