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


        public bool canDrop(FrameworkElement source)
        {
            if (source as SurfaceListBoxItem != null && (source as SurfaceListBoxItem).DataContext is AddBrick)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addToTargetList(SurfaceDragCursor cursor, SurfaceListBoxItem target, SurfaceListBox targetList )
        {
            if (cursor.Data is Statement && target != null && targetList != null)
            {
                //Liste der View bearbeiten
                int index = (targetList.ItemsSource as ObservableCollection<Statement>).IndexOf(target.DataContext as Statement);
                (targetList.ItemsSource as ObservableCollection<Statement>).Insert(index, cursor.Data as Statement);
                //ModelDaten bearbeiten
                ListStm parentList = ((target.DataContext as Statement).Parent as ListStm);
                parentList.addStm(cursor.Data as Statement);
                if (cursor.Data is ListStm)
                {
                    (targetList.ItemsSource as ObservableCollection<Statement>).Insert(index + 1, (cursor.Data as ListStm).StmList.Last.Previous.Value);
                    (targetList.ItemsSource as ObservableCollection<Statement>).Insert(index + 2, (cursor.Data as ListStm).StmList.Last.Value);
                }
            }
        }

        public void removeFromSourceList(SurfaceDragCursor cursor, SurfaceListBox dragSourceList)
        {
            if (cursor.Data is Statement && dragSourceList != null)
            {
                (dragSourceList.ItemsSource as ObservableCollection<Statement>).Remove(cursor.Data as Statement);
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

    }
}
