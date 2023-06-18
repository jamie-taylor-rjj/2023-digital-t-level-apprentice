using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_GenUI.Models.Services
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        
    }
    public class NavigationService : ViewModel, INavigationService
    {
       
        private ViewModel _currentView;
        public ViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService()
        {
            
        }

        

    }
}
