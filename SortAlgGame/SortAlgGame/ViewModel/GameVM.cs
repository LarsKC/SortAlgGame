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
using System.Windows.Threading;




namespace SortAlgGame.ViewModel
{
    /// <summary>
    /// Stellt die Daten fuer die Gui des Spiels zur Verfuegung. Sie Erbt von der Klasse NotifyChangeBase, 
    /// um der GUI Aenderungen mitteilen zu koennen.
    /// </summary>
    class GameVM : NotifyChangeBase
    {
        #region Member
        /// <summary>
        /// Auflistung der Daten fuer die Bausteinliste des Spielers 1.
        /// </summary>
        private ObservableCollection<Statement> _sourceItemsP1;
        /// <summary>
        /// Auflistung der Daten fuer die Algorithmus-Liste des Spielers 1.
        /// </summary>
        private ObservableCollection<Statement> _targetItemsP1;
        /// <summary>
        /// Auflistung der Daten fuer die Bausteinliste des Spielers 2.
        /// </summary>
        private ObservableCollection<Statement> _sourceItemsP2;
        /// <summary>
        /// Auflistung der Daten fuer die Algorithmus-Liste des Spielers 2.
        /// </summary>
        private ObservableCollection<Statement> _targetItemsP2;
        /// <summary>
        /// Referenz auf ein Objekt der Model Klasse Game.
        /// </summary>
        private Game _game;
        /// <summary>
        /// Referenz auf die MainVM.
        /// </summary>
        private MainVM _mainVM;
        /// <summary>
        /// Timer fuer die bereits verstrichene Spielzeit.
        /// </summary>
        private DispatcherTimer _gameTimer;
        /// <summary>
        /// Timer fuer den Ladebildschirm.
        /// </summary>
        private DispatcherTimer _loadingTimer;
        /// <summary>
        /// Signalisiert, ob der Spieler 1 fertig ist.
        /// </summary>
        private bool _p1Fin;
        /// <summary>
        /// Signalisiert, ob der Spieler 2 fertig ist.
        /// </summary>
        private bool _p2Fin;
        /// <summary>
        /// Zeit des Spieler 1.
        /// </summary>
        private string _p1Time;
        /// <summary>
        /// Zeit des Spieler 2.
        /// </summary>
        private string _p2Time;
        /// <summary>
        /// Text des Wartebildschirms.
        /// </summary>
        private string _waiting;
        /// <summary>
        /// Sichtbarkeit des Spieler 1 Overlay.
        /// </summary>
        private string _p1LoadingVisible;
        /// <summary>
        /// Sichtbarkeit des Spieler 2 Overlay.
        /// </summary>
        private string _p2LoadingVisible;
        #endregion

        #region Accessoren & Commands
        /// <summary>
        /// Befehl, der den Spieler 1 als fertig markiert.
        /// </summary>
        public ICommand gameFin1
        {
            get
            {
                return new Command(Action => gameFin(_game.P1));
            }
        }
        /// <summary>
        /// Befehl, der den Spieler 2 als fertig markiert.
        /// </summary>
        public ICommand gameFin2
        {
            get
            {
                return new Command(Action => gameFin(_game.P2));
            }
        }
        /// <summary>
        /// _mainVM Accessor
        /// </summary>
        public MainVM MainVM
        {
            get { return _mainVM; }
        }
        /// <summary>
        /// _sourceItemsP1 Accessor
        /// </summary>
        public ObservableCollection<Statement> SourceItemsP1
        {
            get
            {
                return _sourceItemsP1;

            }
        }
        /// <summary>
        /// _targetItemsP1 Accessor
        /// </summary>
        public ObservableCollection<Statement> TargetItemsP1
        {
            get
            {
                return _targetItemsP1;
            }
            set
            {
                _targetItemsP1 = value;
                NotifyPropertyChanged("TargetItemsP1");
            }
        }
        /// <summary>
        /// _sourceItemsP2 Accessor
        /// </summary>
        public ObservableCollection<Statement> SourceItemsP2
        {
            get
            {
                return _sourceItemsP2;

            }
        }
        /// <summary>
        /// _targetItemsP2 Accessor
        /// </summary>
        public ObservableCollection<Statement> TargetItemsP2
        {
            get
            {
                return _targetItemsP2;
            }
            set
            {
                _targetItemsP2 = value;
                NotifyPropertyChanged("TargetItemsP2");
            }
        }
        /// <summary>
        /// _p1Time Accessor
        /// </summary>
        public string P1Time
        {
            get { return _p1Time; }
            set
            {
                _p1Time = value;
                NotifyPropertyChanged("P1Time");
            }
        }
        /// <summary>
        /// _p2Time Accessor
        /// </summary>
        public string P2Time
        {
            get { return _p2Time; }
            set
            {
                _p2Time = value;
                NotifyPropertyChanged("P2Time");
            }
        }
        /// <summary>
        /// _waiting Accessor
        /// </summary>
        public string Waiting
        {
            get { return _waiting; }
            set
            {
                _waiting = value;
                NotifyPropertyChanged("Waiting");
            }
        }
        /// <summary>
        /// _p1LoadingVisible Accessor
        /// </summary>
        public string P1LoadingVisible
        {
            get { return _p1LoadingVisible; }
            set
            {
                _p1LoadingVisible = value;
                NotifyPropertyChanged("P1LoadingVisible");
            }
        }
        /// <summary>
        /// _p2LoadingVisible Accessor
        /// </summary>
        public string P2LoadingVisible
        {
            get { return _p2LoadingVisible; }
            set
            {
                _p2LoadingVisible = value;
                NotifyPropertyChanged("P2LoadingVisible");
            }
        }
        /// <summary>
        /// _game Accessor
        /// </summary>
        public Game Game
        {
            get { return _game; }
        }
        #endregion

        #region Konstruktoren
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="mainVM">Referenz auf das MainVM Objekt, dass mit der SurfaceWindow1 View verknuepft ist.</param>
        public GameVM( MainVM mainVM)
        {
            _sourceItemsP1 = new ObservableCollection<Statement>();
            _targetItemsP1 = new ObservableCollection<Statement>();
            _sourceItemsP2 = new ObservableCollection<Statement>();
            _targetItemsP2 = new ObservableCollection<Statement>();
            _p1Fin = false;
            _p2Fin = false;
            _p1LoadingVisible = "Hidden";
            _p2LoadingVisible = "Hidden";
            _waiting = Config.WAITING_Player;
            _game = new Game();
            _mainVM = mainVM;
            initSourceItems();
            initTargetItems();
            _p1Time = "00:00";
            _p2Time = "00:00";
            _gameTimer = new DispatcherTimer();
            _gameTimer.Interval = new TimeSpan(0, 0, 1);
            _gameTimer.Tick += gameTimeEvent;
            _gameTimer.Start();
            _loadingTimer = new DispatcherTimer();
            _loadingTimer.Interval = new TimeSpan(0, 0, 1);
            _loadingTimer.Tick += loadingTimeEvent;
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Stoppt den _gameTimer
        /// </summary>
        public void stopTimer()
        {
            _gameTimer.Stop();
        }
        /// <summary>
        /// Wird im festgelegten Interval des _gameTimer Objekts aufgerufen. Aktualisiert die Zeiten der Spieler.
        /// </summary>
        /// <param name="sender">Sender des Events</param>
        /// <param name="e">Event</param>
        private void gameTimeEvent(object sender, EventArgs e)
        {
            if (_game.P1.Time <= Config.MAX_GAME_TIME || _game.P2.Time <= Config.MAX_GAME_TIME)
            {
                if (!_p1Fin)
                {
                    _game.P1.Time++;
                    P1Time = string.Format("{0:00}:{1:00}", _game.P1.Time / 60, _game.P1.Time % 60);
                }
                if (!_p2Fin)
                {
                    _game.P2.Time++;
                    P2Time = string.Format("{0:00}:{1:00}", _game.P2.Time / 60, _game.P2.Time % 60);
                }
            }
            else
            {
                _gameTimer.Stop();
            }
        }
        /// <summary>
        /// Wird im Interval des _loadingTimer Objekts aufgerufen. Sorgt fuer eine geringe Verzoegerung der 
        /// Auswertung, damit der Ladebildschirm auch bei einer sehr schnellen Auswertung der Algorithmen kurz 
        /// zu sehen ist und nicht nur kurz aufblitzt.
        /// </summary>
        /// <param name="sender">Sender des Events</param>
        /// <param name="e">Event</param>
        private void loadingTimeEvent(object sender, EventArgs e)
        {
            ResultVM resultVM = new ResultVM(this);
            _mainVM.CurrentView = resultVM;
            _loadingTimer.Stop();
        }
        /// <summary>
        /// Schaltet das Spieler Overlay sichtbar und startet, wenn beide Spieler fertig sind den Timer _loadingTimer 
        /// zur Darstellung des Ladebildschirms.
        /// </summary>
        /// <param name="player">Spieler der fertig ist</param>
        private void gameFin(Player player)
        {
            if (player == _game.P1)
            {
                _p1Fin = true;
                P1LoadingVisible = "Visible";
            }
            else if (player == _game.P2)
            {
                _p2Fin = true;
                P2LoadingVisible = "Visible";
            }
            if (_p1Fin && _p2Fin)
            {
                Waiting = Config.WAITING_RESULT;
                _gameTimer.Stop();
                _loadingTimer.Start();
            }

        }
        /// <summary>
        /// Sorgt fuer das Initialisieren der Bausteinlisten.
        /// </summary>
        private void initSourceItems()
        {
            initPlayerWise(_sourceItemsP1, _game.P1);
            initPlayerWise(_sourceItemsP2, _game.P2);
        }
        /// <summary>
        /// Initialisiert die Bausteinliste des uebergebenen Spielers.
        /// </summary>
        /// <param name="list">Bausteinliste</param>
        /// <param name="player">Besitzer der Bausteinliste</param>
        private void initPlayerWise(ObservableCollection<Statement> list, Player player)
        {
            //Allocations
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_ALENGHT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_I_TO_J));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_I_TO_MIN));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_J_TO_MIN));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_LEFT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_PIVOT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.ALLOC_RIGHT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.DEC_J));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.DEC_J));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.INC_I));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.INC_I));
            //Conditions
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_AI_GREATER_AI_INC));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_AJ_LESS_AMIN));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_LESS_EQUALS_J));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_LESS_RIGHT));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_NOTEQUALS_MIN));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_LEFT_LESS_J));
            //Loops & If's
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.FOR_IN_BUBBLE));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.FOR_IN_SELECTION));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.FOR_INSERTION));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.FOR_OUT_BUBBLE));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.WHILE_AI_LESS_PIVOT));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.WHILE_AJ_GREATER_PIVOT));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.WHILE_I_LESS_EQUALS_J));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.WHILE_J_GREATER_NULL));
            //MethodCalls
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.CALL_SORT_LEFT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.CALL_SORT_RIGHT));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.SWAP_AI_WITH_AI_INC));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.SWAP_AI_WITH_AJ));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.SWAP_AJ_WITH_AJ_DEC));
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.SWAP_AMIN_WITH_AI));
        }
        /// <summary>
        /// Initialisiert die Algorithmus-Listen der Spieler
        /// </summary>
        private void initTargetItems()
        {
            _targetItemsP1.Add(_game.P1.Programm.Stm);
            _targetItemsP1.Add((_game.P1.Programm.Stm as ListStm).StmList.Last.Value);
            _targetItemsP2.Add(_game.P2.Programm.Stm);
            _targetItemsP2.Add((_game.P2.Programm.Stm as ListStm).StmList.Last.Value);
        }
        /// <summary>
        /// Ueberprueft ob ein Drop erlaubt ist.
        /// </summary>
        /// <param name="cursorData">Die Daten des Cursors</param>
        /// <param name="targetData">Daten des Ziels</param>
        /// <param name="sourceTag">Ursprungsort des Cursors</param>
        /// <param name="targetTag">Bezeichnung des Ziels</param>
        /// <returns>True, wenn ein Drop erlaubt ist. False, wenn nicht.</returns>
        public bool canDrop(Object cursorData, Object targetData, String sourceTag, String targetTag)
        {
            if (!(cursorData is Statement && (targetData is Statement || targetData is ObservableCollection<Statement>)))
            {
                return false;
            }

            if (targetData is Statement)
            {
                if ((cursorData as Statement).Programm != (targetData as Statement).Programm)
                {
                    return false;
                }
            }
            else
            {
                if ((sourceTag.Contains("P1") && targetTag.Contains("P2")) || (sourceTag.Contains("P2") && targetTag.Contains("P1")))
                {
                    return false;
                }
            }

            switch (sourceTag)
            {
                case "targetListP1":
                case "targetListP2":
                    switch (targetTag)
                    {
                        case "targetListP1":
                        case "targetListP2":
                            //Sortieren der TargetListe
                            if(!((targetData as Statement).Brick == Config.EXECUTE.BASE_STM) && targetData is Statement)
                            {
                                if((cursorData is ListStm && !(cursorData as ListStm).stmListContains(targetData as Statement, cursorData as ListStm)))
                                {
                                    return true;
                                }
                                else if(!(cursorData is ListStm))
                                {
                                    return true;
                                }
                            }
                            break;
                        case "sourceListP1":
                        case "sourceListP2":
                            //Zuruecklegen eines Code Blocks in die Bausteinliste
                            return true;
                        default:
                            //Nothing
                            break;
                    }
                    break;
                case "sourceListP1":
                case "sourceListP2":
                    //Hinzufuegen eines Code Blocks zur Algorithmus-Liste
                    if((targetTag == "targetListP1" || targetTag == "targetListP2") && !((targetData as Statement).Brick == Config.EXECUTE.BASE_STM))
                    {
                        return true;
                    }
                    break;
                default:
                    //Nothing
                    break;
            }
            return false;
        }
        /// <summary>
        /// Prueft ob die im Cursor enthaltenen Daten bewegt werden duerfen.
        /// </summary>
        /// <param name="cursorData">Daten des Cursors</param>
        /// <returns>True, wenn Drag erlaubt. False, wenn nicht.</returns>
        public bool dragableObject(Object cursorData)
        {
            if (cursorData is Statement && !( (cursorData as Statement).Brick == Config.EXECUTE.LOOP_END || (cursorData as Statement).Brick == Config.EXECUTE.BASE_STM))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ueberprueft ob sich das gesuchte Statement in der Liste befindet.
        /// </summary>
        /// <param name="sourceList">Zu durchsuchende Liste</param>
        /// <param name="cursorData">Gesuchtes Statement</param>
        /// <returns></returns>
        public bool inSourceList(Object sourceList, Object cursorData)
        {
            if(sourceList is ObservableCollection<Statement> && cursorData is Statement)
            {
                if((sourceList as ObservableCollection<Statement>).Contains(cursorData as Statement))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Aktualisiert die ObservableCollections _targetItemsP1 und _targetItemsP2 
        /// </summary>
        /// <param name="p">Das Programm, das aktualisiert wurde</param>
        public void updateTargetList(Programm p)
        {
            if (p == _game.P1.Programm)
            {
                TargetItemsP1 = new ObservableCollection<Statement>(_game.P1.Programm.getActualStmNesting());
            }
            else if (p == _game.P2.Programm)
            {
                TargetItemsP2 = new ObservableCollection<Statement>(_game.P2.Programm.getActualStmNesting());
            }
        }
        /// <summary>
        /// Fuegt an gewuenschter Position des Algorithmus den in CursorData enthaltenen Baustein hinzu, aktualisiert dann 
        /// die Algorithmus-Liste und entfernt den Baustein aus der Bausteinliste.
        /// </summary>
        /// <param name="cursorData">Cursor Daten in Form eines Statement</param>
        /// <param name="targetStm">Ziel des Drops</param>
        /// <param name="sourceList">Ursprung der Cursor Daten</param>
        public void addToTargetList(Object cursorData, Object targetStm, System.Collections.IEnumerable sourceList)
        {
            if (cursorData is Statement && targetStm is Statement && sourceList is ObservableCollection<Statement>)
            {
                //CursorDaten zum Algorithmu hinzufuegen
                (targetStm as Statement).Parent.addBeforeStm(targetStm as Statement, cursorData as Statement);
                //CursorDaten aus Ursprungsliste entfernen
                (sourceList as ObservableCollection<Statement>).Remove(cursorData as Statement);
                //Algorithmus-Liste aktualisieren
                updateTargetList((cursorData as Statement).Programm);
            }
        }
        /// <summary>
        /// Fuegt der Bausteinliste einen neuen Baustein hinzu.
        /// </summary>
        /// <param name="cursorData">Cursor Daten in form eines Statements</param>
        /// <param name="target">Ziel Objekt</param>
        public void addToSourceList(object cursorData, object target)
        {
            if (cursorData is Statement && target is ObservableCollection<Statement>)
            {
                //CursorDaten zur Zielliste Hinzufuegen
                (target as ObservableCollection<Statement>).Insert(0, cursorData as Statement);
                //Model Statement Schachtelung aktualisieren
                (cursorData as Statement).Parent.removeStm(cursorData as Statement);
                //TargetList aktualisieren
                updateTargetList((cursorData as Statement).Programm);
            }
        }
        /// <summary>
        /// Verschiebt das Statement des Cursors an die gewuenschtne Position im Algorithmus.
        /// </summary>
        /// <param name="cursorData">Cursor Daten in Form eines Statements</param>
        /// <param name="targetData">Neue Position des Statements</param>
        public void sortStm(object cursorData, object targetData)
        {
            if (cursorData is Statement && targetData is Statement &&cursorData != targetData)
            {
                (cursorData as Statement).Parent.StmList.Remove(cursorData as Statement);
                (targetData as Statement).Parent.addBeforeStm(targetData as Statement, cursorData as Statement);
                updateTargetList((cursorData as Statement).Programm);
            }
        }
        #endregion
    }
}
