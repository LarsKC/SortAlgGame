using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SortAlgGame.Model;
using SortAlgGame.Model.Statements;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;

namespace SortAlgGame.ViewModel
{
    class ResultVM : NotifyChangeBase
    {
        private Game _game;
        private ObservableCollection<Tuple<int, string, string, string, int>> _p1StatCol;
        private ObservableCollection<Tuple<int, string, string, string, int>> _p2StatCol;
        private ObservableCollection<Statement> _p1TargetItems;
        private ObservableCollection<Statement> _p2TargetItems;
        private string _p1Status;
        private string _p2Status;
        private int _p1Points;
        private int _p2Points;
        private string _p1Time;
        private string _p2Time;
        private AnimationVM _p1Animation;
        private AnimationVM _p2Animation;

        #region Accessor
        public ObservableCollection<Tuple<int, string, string, string, int>> P1StatCol
        {
            get { return _p1StatCol; }
        }

        public ObservableCollection<Tuple<int, string, string, string, int>> P2StatCol
        {
            get { return _p2StatCol; }
        }

        public ObservableCollection<Statement> P1TargetItems
        {
            get { return _p1TargetItems; }
        }

        public ObservableCollection<Statement> P2TargetItems
        {
            get { return _p2TargetItems; }
        }

        public string P1Status
        {
            get { return _p1Status; }
        }

        public string P2Status
        {
            get { return _p2Status; }
        }

        public int P1Points
        {
            get { return _p1Points; }
        }

        public int P2Points
        {
            get { return _p2Points; }
        }

        public AnimationVM P1Animation
        {
            get { return _p1Animation; }
        }

        public AnimationVM P2Animation
        {
            get { return _p2Animation; }
        }

        public string P1Time
        {
            get { return _p1Time; }
        }

        public string P2Time
        {
            get { return _p2Time; }
        }

        #endregion

        public ResultVM(GameVM gameVM)
        {
            _p1TargetItems = gameVM.TargetItemsP1;
            _p2TargetItems = gameVM.TargetItemsP2;
            _game = gameVM.Game;
            _game.evaluate();
            _p1StatCol = new ObservableCollection<Tuple<int, string, string, string, int>>(_game.P1.PointList);
            _p2StatCol = new ObservableCollection<Tuple<int, string, string, string, int>>(_game.P2.PointList);
            _p1Points = _game.P1.Points;
            _p2Points = _game.P2.Points;
            _p1Time = "Benötigte Zeit: " + string.Format("{0:00}:{1:00}", _game.P1.Time / 60, _game.P1.Time % 60);
            _p2Time = "Benötigte Zeit: " + string.Format("{0:00}:{1:00}", _game.P2.Time / 60, _game.P2.Time % 60);
            if (_game.FastestPlayer == _game.P1)
            {
                _p1Time += " schnellster Spieler +1 Bonuspunkt!";
            }
            else if (_game.FastestPlayer == _game.P2)
            {
                _p2Time += " schnellster Spieler +1 Bonuspunkt!";
            }
            _p1Animation = new AnimationVM(_game.P1.Programm);
            _p2Animation = new AnimationVM(_game.P2.Programm);
            if (_game.Winner == _game.P1)
            {
                _p1Status = "Gewonnen";
                _p2Status = "Verloren";
            }
            else if (_game.Winner == _game.P2)
            {
                _p2Status = "Gewonnen";
                _p1Status = "Verloren";
            }
            else
            {
                _p1Status = "Unentschieden";
                _p2Status = "Unentschieden";
            }
        }
    }
}
