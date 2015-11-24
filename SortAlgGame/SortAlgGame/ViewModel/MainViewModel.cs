using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentView;

        public MainViewModel()
        {
            currentView = new HauptmenueViewModel();
        }

        public BaseViewModel CurrentView
        {
            get { return currentView; }
            set
            {
                if (currentView != value)
                {
                    currentView = value;
                }
            }
        }

        //TODO: Methoden zum switchen der View
        public void changeToErklaerung()
        {
            currentView = new MenueErklaerungViewModel();
        }

        public void changeToHauptmenue()
        {
            currentView = new HauptmenueViewModel();
        }


    }
}
