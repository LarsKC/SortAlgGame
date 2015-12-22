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




namespace SortAlgGame.ViewModel
{
    class GameVM : BaseViewModel
    {

        private ObservableCollection<Statement> _sourceItemsP1;
        private ObservableCollection<Statement> _targetItemsP1;
        private ObservableCollection<Statement> _sourceItemsP2;
        private ObservableCollection<Statement> _targetItemsP2;
        private Game _game;
        private String[] _resultPlayer1;
        private String[] _resultPlayer2;

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
        }

        public GameVM()
        {
            _sourceItemsP1 = new ObservableCollection<Statement>();
            _targetItemsP1 = new ObservableCollection<Statement>();
            _sourceItemsP2 = new ObservableCollection<Statement>();
            _targetItemsP2 = new ObservableCollection<Statement>();
            _game = new Game();
            initSourceItems();
            initTargetItems();
        }

        public void initSourceItems()
        {
            initPlayerWise(_sourceItemsP1, _game.Player1);
            initPlayerWise(_sourceItemsP2, _game.Player2);
        }

        public void initPlayerWise(ObservableCollection<Statement> list, Player player)
        {
            //Player 1
            //Allocations
            list.Add(new AllocALength(player, null));
            list.Add(new AllocIToJ(player, null));
            list.Add(new AllocIToMin(player, null));
            list.Add(new AllocJToMin(player, null));
            list.Add(new AllocLeft(player, null));
            list.Add(new AllocPivot(player, null));
            list.Add(new AllocRight(player, null));
            list.Add(new DecJ(player, null));
            list.Add(new IncI(player, null));
            //Conditions
            list.Add(new IfAiGreaterAiInc(player, null));
            list.Add(new IfAjLessAmin(player, null));
            list.Add(new IfIEqualsJ(player, null));
            list.Add(new IfILessJ(player, null));
            list.Add(new IfILessRight(player, null));
            list.Add(new IfINotEqualsMin(player, null));
            list.Add(new IfLeftLessJ(player, null));
            //Loops
            list.Add(new ForInBubble(player, null));
            list.Add(new ForInSelection(player, null));
            list.Add(new ForInsertion(player, null));
            list.Add(new ForOutBubble(player, null));
            list.Add(new WhileAiLessPivot(player, null));
            list.Add(new WhileAjGreaterPivot(player, null));
            list.Add(new WhileILessEqualsJ(player, null));
            list.Add(new WhileJGreaterNull(player, null));
            //MethodCalls
            list.Add(new CallSortLeft(player, null));
            list.Add(new CallSortRight(player, null));
            list.Add(new SwapAiWithAiInc(player, null));
            list.Add(new SwapAiWithAj(player, null));
            list.Add(new SwapAjWithAjDec(player, null));
            list.Add(new SwapAminWithAi(player, null));
        }

        public void initTargetItems()
        {
            _targetItemsP1.Add(_game.Player1.Stm);
            _targetItemsP1.Add((_game.Player1.Stm as ListStm).StmList.Last.Previous.Value);
            _targetItemsP1.Add((_game.Player1.Stm as ListStm).StmList.Last.Value);
            _targetItemsP2.Add(_game.Player2.Stm);
            _targetItemsP2.Add((_game.Player2.Stm as ListStm).StmList.Last.Previous.Value);
            _targetItemsP2.Add((_game.Player2.Stm as ListStm).StmList.Last.Value);

        }

        public bool canDrop(Object cursorData, Object targetData, String sourceTag, String targetTag)
        {
            //Bedingung für jeden Drop
            if(!dragableObject(cursorData))
            {
                return false;
            }

            //Drop Erlaubnis auf einen AddBrick in der TargetListe die dem Spieler gehört
            if(targetData is AddBrick && (targetData as Statement).Player == (cursorData as Statement).Player && sourceTag != targetTag)
            {
                return true;
            }

            //Drop Erlaubnis innerhalb einer TargetListe zum sortieren der Statements
            if( (sourceTag == "targetListP1" || sourceTag == "targetListP2") && sourceTag == targetTag && targetData is Statement && !(targetData is LoopEnd)  && !(targetData is BaseStatement))
            {
                if ((cursorData is ListStm && (cursorData as ListStm).StmList.Find(targetData as Statement) == null) || !(cursorData is ListStm))
                {
                    return true;
                }
            }

            //Drop Erlaubnis zum Zurücklegen eines Bereits zur TargetList hinzugefügten Blocks
            if(targetData is ObservableCollection<Statement> && (targetData as ObservableCollection<Statement>).First<Statement>().Player == (cursorData as Statement).Player && (targetTag == "sourceListP1" || targetTag == "sourceListP2") && sourceTag != "sourceListP1" && sourceTag != "sourceListP2")
            {
                return true;
            }

            return false;
        }

        public bool dragableObject(Object cursorData)
        {
            if (cursorData is Statement && !(cursorData is AddBrick || cursorData is LoopEnd || cursorData is BaseStatement))
            {
                return true;
            }
            return false;
        }

        public void addToTargetList(Object cursorData, Object targetStm, System.Collections.IEnumerable targetList)
        {
            if (cursorData is Statement && targetStm is Statement && targetList is ObservableCollection<Statement>)
            {
                //Liste für die View aktualisieren
                int index = (targetList as ObservableCollection<Statement>).IndexOf(targetStm as Statement);
                (targetList as ObservableCollection<Statement>).Insert(index, cursorData as Statement);
                //Model Datenschachtelung aktualisieren
                (targetStm as Statement).Parent.addStm(cursorData as Statement);
                if (cursorData is ListStm)
                {
                    (targetList as ObservableCollection<Statement>).Insert(index + 1, (cursorData as ListStm).StmList.Last.Previous.Value);
                    (targetList as ObservableCollection<Statement>).Insert(index + 2, (cursorData as ListStm).StmList.Last.Value);
                }


            }
        }

        public void addToSourceList(object cursorData, object target)
        {
            if (cursorData is Statement && target is ObservableCollection<Statement>)
            {
                //CursorDaten zur Zielliste Hinzufügen
                (target as ObservableCollection<Statement>).Add(cursorData as Statement);
            }
        }

        public void removeFromSourceList(object cursorData, System.Collections.IEnumerable dragSourceList)
        {
            if (cursorData is Statement && dragSourceList is ObservableCollection<Statement>)
            {
                (dragSourceList as ObservableCollection<Statement>).Remove(cursorData as Statement);
            }
        }

        public void removeFromTargetList(object cursorData, object source)
        {
            if (cursorData is Statement && source is ObservableCollection<Statement>)
            {
                //Von View entfernen
                (source as ObservableCollection<Statement>).Remove(cursorData as Statement);
                if (cursorData is ListStm)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        (source as ObservableCollection<Statement>).Remove((cursorData as ListStm).StmList.Last<Statement>());
                    }

                }
                //Statements aus der Schachtelung entfernen
                (cursorData as Statement).Parent.removeStm(cursorData as Statement);
            }
        }

        public void sortStm(object cursorData, object targetData, System.Collections.IEnumerable targetList)
        {
            if (cursorData is Statement && targetData is Statement && targetList is ObservableCollection<Statement> && cursorData != targetData)
            {
                //ViewSortieren
                if (cursorData is ListStm)
                {
                    removeItemsFromViewList(targetList as ObservableCollection<Statement>, (cursorData as ListStm).StmList);
                }
                (targetList as ObservableCollection<Statement>).Remove(cursorData as Statement);
                int index = (targetList as ObservableCollection<Statement>).IndexOf(targetData as Statement);
                (targetList as ObservableCollection<Statement>).Insert(index, cursorData as Statement);
                //Model Sortieren TODO: Indent berechnen!!!!
                (targetData as Statement).Parent.StmList.AddBefore((targetData as Statement).Parent.StmList.Find(targetData as Statement), cursorData as Statement);
                (cursorData as Statement).Parent.StmList.Remove(cursorData as Statement);
                (cursorData as Statement).Parent = (targetData as Statement).Parent;
                (cursorData as Statement).Indent = (cursorData as Statement).Parent.Indent + 1;
                if (cursorData is ListStm)
                {
                    addItemsToViewList(targetList as ObservableCollection<Statement>, index, (cursorData as ListStm).StmList);
                }
            }
        }

        public void removeItemsFromViewList(ObservableCollection<Statement> targetList, LinkedList<Statement> stmList)
        {
            foreach (Statement x in stmList)
            {
                if (x is ListStm)
                {
                    targetList.Remove(x);
                    removeItemsFromViewList(targetList, (x as ListStm).StmList);
                }
                else
                {
                    targetList.Remove(x);
                }
            }
        }

        public void addItemsToViewList(ObservableCollection<Statement> targetList, int index , LinkedList<Statement> stmList)
        {
            foreach (Statement x in stmList)
            {
                if (x is ListStm)
                {
                    targetList.Insert(++index, x);
                    addItemsToViewList(targetList, index,  (x as ListStm).StmList);
                }
                else
                {
                    targetList.Insert(++index, x);
                }
            }

        }

        public void stopPlayer1()
        {
            _game.Player1.EndTime = DateTime.Now;
            if (_game.PlayerRdy)
            {
                gameFin();
            }
            else
            {
                _game.PlayerRdy = true;
            }
        }

        public void stopPlayer2()
        {
            _game.Player2.EndTime = DateTime.Now;
            if (_game.PlayerRdy)
            {
                gameFin();
            }
            else
            {
                _game.PlayerRdy = true;
            }
        }

        private void gameFin()
        {
            _game.run();
        }

        public void printExecute()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Lars\Desktop\executePrint.txt");
            file.WriteLine(_game.Player1.Stm.Content);
            rekursivePrint((_game.Player1.Stm as ListStm).StmList, file);
            file.WriteLine("---------------------------------------------------------------------------------");
            file.Close();
        }

        public void rekursivePrint(LinkedList<Statement> stmList, System.IO.StreamWriter file)
        {
            foreach (Statement x in stmList)
            {
                file.WriteLine(x.Content);
                if (x is ListStm)
                {
                    rekursivePrint((x as ListStm).StmList, file);
                }
            }
        }
    }
}
