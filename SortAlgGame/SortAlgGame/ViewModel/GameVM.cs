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
    class GameVM : NotifyChangeBase
    {

        private ObservableCollection<Statement> _sourceItemsP1;
        private ObservableCollection<Statement> _targetItemsP1;
        private ObservableCollection<Statement> _sourceItemsP2;
        private ObservableCollection<Statement> _targetItemsP2;
        private Game _game;
        private MainVM _mainVM;
        private DispatcherTimer _gameTimer;
        private DispatcherTimer _loadingTimer;
        private bool _p1Fin;
        private bool _p2Fin;
        private string _p1Time;
        private string _p2Time;
        private string _waiting;
        private string _p1LoadingVisible;
        private string _p2LoadingVisible;


        public ICommand gameFin1
        {
            get
            {
                return new Command(Action => gameFin(_game.P1));
            }
        }

        public ICommand gameFin2
        {
            get
            {
                return new Command(Action => gameFin(_game.P2));
            }
        }

        public MainVM MainVM
        {
            get { return _mainVM; }
        }

        public ObservableCollection<Statement> SourceItemsP1
        {
            get
            {
                return _sourceItemsP1;

            }
        }

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

        public ObservableCollection<Statement> SourceItemsP2
        {
            get
            {
                return _sourceItemsP2;

            }
        }

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

        public string P1Time
        {
            get { return _p1Time; }
            set
            {
                _p1Time = value;
                NotifyPropertyChanged("P1Time");
            }
        }

        public string P2Time
        {
            get { return _p2Time; }
            set
            {
                _p2Time = value;
                NotifyPropertyChanged("P2Time");
            }
        }

        public string Waiting
        {
            get { return _waiting; }
            set
            {
                _waiting = value;
                NotifyPropertyChanged("Waiting");
            }
        }

        public string P1LoadingVisible
        {
            get { return _p1LoadingVisible; }
            set
            {
                _p1LoadingVisible = value;
                NotifyPropertyChanged("P1LoadingVisible");
            }
        }

        public string P2LoadingVisible
        {
            get { return _p2LoadingVisible; }
            set
            {
                _p2LoadingVisible = value;
                NotifyPropertyChanged("P2LoadingVisible");
            }
        }

        public Game Game
        {
            get { return _game; }
        }

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

        public void stopTimer()
        {
            _gameTimer.Stop();
        }

        public void gameTimeEvent(object sender, EventArgs e)
        {
            if (!_p1Fin)
            {
                _game.P1.Time++;
                P1Time = string.Format("{0:00}:{1:00}",_game.P1.Time/60, _game.P1.Time%60);
            }
            if (!_p2Fin)
            {
                _game.P2.Time++;
                P2Time = string.Format("{0:00}:{1:00}", _game.P2.Time / 60, _game.P2.Time % 60);
            }
        }

        public void loadingTimeEvent(object sender, EventArgs e)
        {
            ResultVM resultVM = new ResultVM(this);
            _mainVM.CurrentView = resultVM;
            _loadingTimer.Stop();
        }

        public void gameFin(Player player)
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

        public void initSourceItems()
        {
            initPlayerWise(_sourceItemsP1, _game.P1);
            initPlayerWise(_sourceItemsP2, _game.P2);
        }

        public void initPlayerWise(ObservableCollection<Statement> list, Player player)
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
            list.Add(new Statement(player.Programm, null, Config.EXECUTE.INC_I));
            //Conditions
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_AI_GREATER_AI_INC));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_AJ_LESS_AMIN));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_LESS_EQUALS_J));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_LESS_RIGHT));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_I_NOTEQUALS_MIN));
            list.Add(new ListStm(player.Programm, null, Config.EXECUTE.IF_LEFT_LESS_J));
            //Loops
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

        public void initTargetItems()
        {
            _targetItemsP1.Add(_game.P1.Programm.Stm);
            _targetItemsP1.Add((_game.P1.Programm.Stm as ListStm).StmList.Last.Value);
            _targetItemsP2.Add(_game.P2.Programm.Stm);
            _targetItemsP2.Add((_game.P2.Programm.Stm as ListStm).StmList.Last.Value);
        }

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
                if ((cursorData as Statement).Programm != (targetData as ObservableCollection<Statement>).First<Statement>().Programm)
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
                            //Zurücklegen eines Code Blocks
                            return true;
                        default:
                            //Nothing
                            break;
                    }
                    break;
                case "sourceListP1":
                case "sourceListP2":
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

        public bool dragableObject(Object cursorData)
        {
            if (cursorData is Statement && !( (cursorData as Statement).Brick == Config.EXECUTE.LOOP_END || (cursorData as Statement).Brick == Config.EXECUTE.BASE_STM))
            {
                return true;
            }
            return false;
        }

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

        

        public void addToTargetList(Object cursorData, Object targetStm, System.Collections.IEnumerable sourceList)
        {
            if (cursorData is Statement && targetStm is Statement && sourceList is ObservableCollection<Statement>)
            {
                (targetStm as Statement).Parent.addBeforeStm(targetStm as Statement, cursorData as Statement);
                (sourceList as ObservableCollection<Statement>).Remove(cursorData as Statement);
                updateTargetList((cursorData as Statement).Programm);
            }
        }

        public void addToSourceList(object cursorData, object target)
        {
            if (cursorData is Statement && target is ObservableCollection<Statement>)
            {
                //CursorDaten zur Zielliste Hinzufügen
                (target as ObservableCollection<Statement>).Insert(0, cursorData as Statement);
                //Model Statement Schachtelung aktualisieren
                (cursorData as Statement).Parent.removeStm(cursorData as Statement);
                //TargetList aktualisieren
                updateTargetList((cursorData as Statement).Programm);
            }
        }

        public void sortStm(object cursorData, object targetData)
        {
            if (cursorData is Statement && targetData is Statement &&cursorData != targetData)
            {
                (cursorData as Statement).Parent.StmList.Remove(cursorData as Statement);
                (targetData as Statement).Parent.addBeforeStm(targetData as Statement, cursorData as Statement);
                updateTargetList((cursorData as Statement).Programm);
            }
        }
    }
}
