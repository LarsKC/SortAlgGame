﻿using System;
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
            if (!(cursorData is Statement && (targetData is Statement || targetData is ObservableCollection<Statement>)))
            {
                return false;
            }

            if (targetData is Statement)
            {
                if ((cursorData as Statement).Player != (targetData as Statement).Player)
                {
                    return false;
                }
            }
            else
            {
                if ((cursorData as Statement).Player != (targetData as ObservableCollection<Statement>).First<Statement>().Player)
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
                            if(!(targetData is LoopEnd) && !(targetData is BaseStatement) && targetData is Statement)
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
                    if((targetTag == "targetListP1" || targetTag == "targetListP2") && targetData is AddBrick)
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
            if (cursorData is Statement && !(cursorData is AddBrick || cursorData is LoopEnd || cursorData is BaseStatement))
            {
                return true;
            }
            return false;
        }

        public void updateTargetList(Statement stm)
        {
            if (stm.Player == _game.Player1)
            {
                TargetItemsP1 = getActualStmNesting(_game.Player1.Stm);
            }
            else if (stm.Player == _game.Player2)
            {
                TargetItemsP2 = getActualStmNesting(_game.Player2.Stm);
            }
        }

        public ObservableCollection<Statement> getActualStmNesting(Statement baseStm)
        {
            ObservableCollection<Statement> itemList = new ObservableCollection<Statement>();
            itemList.Add(baseStm);
            if (baseStm is ListStm)
            {
                foreach (Statement x in (baseStm as ListStm).StmList)
                {
                    if (x is ListStm)
                    {
                        concatCollection(itemList, getActualStmNesting(x));
                    }
                    else
                    {
                        itemList.Add(x);
                    }
                }
            }
            return itemList;
        }

        public void concatCollection(ObservableCollection<Statement> first, ObservableCollection<Statement> second)
        {
            foreach (Statement x in second)
            {
                first.Add(x);
            }
        }

        public void addToTargetList(Object cursorData, Object targetStm, System.Collections.IEnumerable sourceList)
        {
            if (cursorData is Statement && targetStm is Statement && sourceList is ObservableCollection<Statement>)
            {
                (targetStm as Statement).Parent.addBeforeStm(null, cursorData as Statement);
                (sourceList as ObservableCollection<Statement>).Remove(cursorData as Statement);
                updateTargetList(cursorData as Statement);
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
                updateTargetList(cursorData as Statement);
            }
        }

        public void sortStm(object cursorData, object targetData)
        {
            if (cursorData is Statement && targetData is Statement &&cursorData != targetData)
            {
                (cursorData as Statement).Parent.StmList.Remove(cursorData as Statement);
                (targetData as Statement).Parent.addBeforeStm(targetData as Statement, cursorData as Statement);
                updateTargetList(cursorData as Statement);
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
