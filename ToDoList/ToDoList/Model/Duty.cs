using System;
using System.ComponentModel;

namespace ToDoList.Model
{
    internal class Duty : INotifyPropertyChanged
    {
        private string name;
        private DateTime? term;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime? Term
        {
            get => term;
            set
            {
                term = value;
                OnPropertyChanged(nameof(Term));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
