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
using SortAlgGame.Model.Statements.Loops;
using SortAlgGame.Model.Statements.Allocations;
using SortAlgGame.Model.Statements.Conditions;
using SortAlgGame.Model.Statements.MethodCalls;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Threading;




namespace SortAlgGame.ViewModel
{
    class GameVM : BaseViewModel
    {

        private ObservableCollection<Statement> _sourceItemsP1;
        private ObservableCollection<Statement> _targetItemsP1;
        private ObservableCollection<Statement> _sourceItemsP2;
        private ObservableCollection<Statement> _targetItemsP2;
        private Game _game;
        private MainViewModel _mainVM;
        private ResultVM _resultVM;
        private DispatcherTimer _timer;
        private int _loadingTimer;
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
                return new RelayCommand(Action => gameFin(_game.P1), Predicate => !_p1Fin);
            }
        }

        public ICommand gameFin2
        {
            get
            {
                return new RelayCommand(Action => gameFin(_game.P2), Predicate => !_p2Fin);
            }
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

        public GameVM( MainViewModel mainVM)
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
            _loadingTimer = 0;
            _game = new Game();
            _mainVM = mainVM;
            initSourceItems();
            initTargetItems();
            _p1Time = "00:00";
            _p2Time = "00:00";
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += timeEvent;
            _timer.Start();
        }

        public void stopTimer()
        {
            _timer.Stop();
        }

        public bool playerFin(string sourceTag)
        {
            if ((sourceTag == "targetListP1" || sourceTag == "sourceListP1") && _p1Fin)
            {
                return true;
            }
            if ((sourceTag == "targetListP2" || sourceTag == "sourceListP2") && _p2Fin)
            {
                return true;
            }
            return false;
        }

        public void timeEvent(object sender, EventArgs e)
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
            if (_p1Fin && _p2Fin)
            {
                _loadingTimer++;
            }
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
                App.Current.Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
                _timer.Stop();
                _resultVM = new ResultVM(this); 
                _mainVM.CurrentView = _resultVM;
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
            list.Add(new AllocALength(player.Programm, null));
            list.Add(new AllocIToJ(player.Programm, null));
            list.Add(new AllocIToMin(player.Programm, null));
            list.Add(new AllocJToMin(player.Programm, null));
            list.Add(new AllocLeft(player.Programm, null));
            list.Add(new AllocPivot(player.Programm, null));
            list.Add(new AllocRight(player.Programm, null));
            list.Add(new DecJ(player.Programm, null));
            list.Add(new IncI(player.Programm, null));
            //Conditions
            list.Add(new IfAiGreaterAiInc(player.Programm, null));
            list.Add(new IfAjLessAmin(player.Programm, null));
            list.Add(new IfILessEqualsJ(player.Programm, null));
            list.Add(new IfILessRight(player.Programm, null));
            list.Add(new IfINotEqualsMin(player.Programm, null));
            list.Add(new IfLeftLessJ(player.Programm, null));
            //Loops
            list.Add(new ForInBubble(player.Programm, null));
            list.Add(new ForInSelection(player.Programm, null));
            list.Add(new ForInsertion(player.Programm, null));
            list.Add(new ForOutBubble(player.Programm, null));
            list.Add(new WhileAiLessPivot(player.Programm, null));
            list.Add(new WhileAjGreaterPivot(player.Programm, null));
            list.Add(new WhileILessEqualsJ(player.Programm, null));
            list.Add(new WhileJGreaterNull(player.Programm, null));
            //MethodCalls
            list.Add(new CallSortLeft(player.Programm, null));
            list.Add(new CallSortRight(player.Programm, null));
            list.Add(new SwapAiWithAiInc(player.Programm, null));
            list.Add(new SwapAiWithAj(player.Programm, null));
            list.Add(new SwapAjWithAjDec(player.Programm, null));
            list.Add(new SwapAminWithAi(player.Programm, null));
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
                            if(!(targetData is BaseStatement) && targetData is Statement)
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
                    if((targetTag == "targetListP1" || targetTag == "targetListP2") && !(targetData is BaseStatement))
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
            if (cursorData is Statement && !( cursorData is LoopEnd || cursorData is BaseStatement))
            {
                return true;
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
