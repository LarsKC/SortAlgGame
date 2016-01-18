using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgGame.ViewModel
{
    class LoadingScreenVM : BaseViewModel
    {
        private GameVM _gameVM;

        public LoadingScreenVM(GameVM gameVM)
        {
            _gameVM = gameVM;
            _gameVM.MainVM.CurrentView = new ResultVM(_gameVM);
        }
    }
}
