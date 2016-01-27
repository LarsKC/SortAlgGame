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
    /// <summary>
    /// Die Klasse ResultVM stellt die Daten fuer die Auswertung des Spiels zur Verfuegung. Sie erbt von der Klasse 
    /// NotifyChangeBase, um der GUI Aenderungen mitteilen zu koennen.
    /// </summary>
    class ResultVM : NotifyChangeBase
    {
        #region Member
        /// <summary>
        /// Referenz auf das Spiel, das ausgewertet werden soll.
        /// </summary>
        private Game _game;
        /// <summary>
        /// Daten der Punktetabelle des Spieler 1: Laenge der angewandten Zahlenfolge, Fehlerbericht, Laufzeit, Sortierung, Punkte.
        /// </summary>
        private ObservableCollection<Tuple<int, string, string, string, int>> _p1StatCol;
        /// <summary>
        /// Daten der Punktetabelle des Spieler 2: Laenge der angewandten Zahlenfolge, Fehlerbericht, Laufzeit, Sortierung, Punkte.
        /// </summary>
        private ObservableCollection<Tuple<int, string, string, string, int>> _p2StatCol;
        /// <summary>
        /// Der vom Spieler 1 zusammengebaute Algorithmus in Form einer Auflistung.
        /// </summary>
        private ObservableCollection<Statement> _p1TargetItems;
        /// <summary>
        /// Der vom Spieler 1 zusammengebaute Algorithmus in Form einer Auflistung.
        /// </summary>
        private ObservableCollection<Statement> _p2TargetItems;
        /// <summary>
        /// Text, ob der Spieler 1 Gewonnen hat.
        /// </summary>
        private string _p1Status;
        /// <summary>
        /// Text, ob der Spieler 2 Gewonnen hat.
        /// </summary>
        private string _p2Status;
        /// <summary>
        /// Gesamtpunktzahl des Spieler 1.
        /// </summary>
        private int _p1Points;
        /// <summary>
        /// Gesamtpunktzahl des Spieler 1.
        /// </summary>
        private int _p2Points;
        /// <summary>
        /// Die vom Spieler 1 benoetigte Zeit.
        /// </summary>
        private string _p1Time;
        /// <summary>
        /// Die vom Spieler 1 benoetigte Zeit.
        /// </summary>
        private string _p2Time;
        /// <summary>
        /// Referenz auf das AnimationVM Objekt, was die Daten fuer die Animation des Spieler 1 beinhaltet.
        /// </summary>
        private AnimationVM _p1Animation;
        /// <summary>
        /// Referenz auf das AnimationVM Objekt, was die Daten fuer die Animation des Spieler 1 beinhaltet.
        /// </summary>
        private AnimationVM _p2Animation;
        #endregion

        #region Accessor
        /// <summary>
        /// _p1StatCol Accessor
        /// </summary>
        public ObservableCollection<Tuple<int, string, string, string, int>> P1StatCol
        {
            get { return _p1StatCol; }
        }
        /// <summary>
        /// _p2StatCol Accessor
        /// </summary>
        public ObservableCollection<Tuple<int, string, string, string, int>> P2StatCol
        {
            get { return _p2StatCol; }
        }
        /// <summary>
        /// _p1TargetItems Accessor
        /// </summary>
        public ObservableCollection<Statement> P1TargetItems
        {
            get { return _p1TargetItems; }
        }
        /// <summary>
        /// _p2TargetItems Accessor
        /// </summary>
        public ObservableCollection<Statement> P2TargetItems
        {
            get { return _p2TargetItems; }
        }
        /// <summary>
        /// _p1Status Accessor
        /// </summary>
        public string P1Status
        {
            get { return _p1Status; }
        }
        /// <summary>
        /// _p2Status Accessor
        /// </summary>
        public string P2Status
        {
            get { return _p2Status; }
        }
        /// <summary>
        /// _p1Points Accessor
        /// </summary>
        public int P1Points
        {
            get { return _p1Points; }
        }
        /// <summary>
        /// _p2Points Accessor
        /// </summary>
        public int P2Points
        {
            get { return _p2Points; }
        }
        /// <summary>
        /// _p1Animation Accessor
        /// </summary>
        public AnimationVM P1Animation
        {
            get { return _p1Animation; }
        }
        /// <summary>
        /// _p2Animation Accessor
        /// </summary>
        public AnimationVM P2Animation
        {
            get { return _p2Animation; }
        }
        /// <summary>
        /// _p1Time Accessor
        /// </summary>
        public string P1Time
        {
            get { return _p1Time; }
        }
        /// <summary>
        /// _p2Time Accessor
        /// </summary>
        public string P2Time
        {
            get { return _p2Time; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="gameVM">Referenz auf das auszuwertende Spiel</param>
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
        #endregion
    }
}
