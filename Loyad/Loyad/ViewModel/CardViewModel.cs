using MVVMPexeso.Model;
using MVVMProject.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMPexeso.ViewModel
{
    internal class CardViewModel : ViewModelBase
    {
        public Card Model { get; }
        public CardViewModel(Card card)
        {
            Model = card;
        }

        // databindingované vlastnosti:

        private bool _IsCorrect;
        public bool IsCorrect
        {
            get => _IsCorrect;
            set { _IsCorrect = value; OnPropertyChanged(); }
        }

        
        public int Id => Model.Id;


    }
}
