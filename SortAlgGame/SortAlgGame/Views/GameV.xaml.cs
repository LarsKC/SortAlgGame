﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using SortAlgGame.ViewModel;

namespace SortAlgGame.Views
{
    /// <summary>
    /// Interaktionslogik für Game.xaml
    /// </summary>
    public partial class GameV : UserControl
    {
        public GameV()
        {
            InitializeComponent();
        }

        public FrameworkElement getFrameworkElement<T>(FrameworkElement source)
        {
            FrameworkElement frame = source;
            while (frame != null && !(frame is T))
            {
                frame = VisualTreeHelper.GetParent(frame) as FrameworkElement;
            }
            return frame;
        }

        private void sourceList_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            FrameworkElement source = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);
            //Wurde kein Ziehbares Obeject gefunden -> return
            if (source == null || !(this.DataContext as GameVM).dragableObject(source.DataContext)) return;
            //Cursor Darstellung
            ContentControl cursor = new ContentControl()
            {
                Content = source.DataContext,
                Style = FindResource("CursorStyle") as Style
            };
            //EventHandler um die Darstellung zu ändern
            SurfaceDragDrop.AddTargetChangedHandler(cursor, OnTargetChanged);
            //Erstellt eine Liste von alle registrierten Touch Punkten, die sich im ausgewaehlten Element befinden
            List<InputDevice> devices = new List<InputDevice>();
            devices.Add(e.TouchDevice);
            foreach (TouchDevice touch in source.TouchesCapturedWithin)
            {
                if (touch != e.TouchDevice)
                {
                    devices.Add(touch);
                }
            }
            //Quelle des zu bewegenen Element bestimmen
            ItemsControl dragSource = ItemsControl.ItemsControlFromItemContainer(source);
            //Beginn der DragDrop Operation
            SurfaceDragCursor startDragOkay = SurfaceDragDrop.BeginDragDrop(
                dragSource,
                source,
                cursor,
                source.DataContext,
                devices,
                DragDropEffects.Move);
            //Wurde mit dem Drag Ablauf erfolgreich begonnen, ist dieses Event erfolgreich beendet. Dadurch wird das feuern der Eltern Events verhindert.
            if (startDragOkay != null)
            {
                startDragOkay.CanRotate = false;
                e.Handled = (startDragOkay != null);
            }
        }

        //Wird immer dann ausgelöst, wenn das Drag Object über ein Element gezogen wird, welches ein DragEnterEvent auslösen kann.
        private void OnDropTargetDragEnter(object sender, SurfaceDragDropEventArgs e)
        {
            FrameworkElement target = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);
            FrameworkElement targetList = getFrameworkElement<SurfaceListBox>(e.OriginalSource as FrameworkElement);
            FrameworkElement sourceList = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource);

            if (targetList != null && sourceList != null && targetList.Tag != null && sourceList.Tag != null)
            {
                if (target == null && !(this.DataContext as GameVM).canDrop(e.Cursor.Data, (targetList as SurfaceListBox).ItemsSource, sourceList.Tag.ToString(), targetList.Tag.ToString()))
                {
                    e.Effects = DragDropEffects.None;
                }
                else if (target != null && !(this.DataContext as GameVM).canDrop(e.Cursor.Data, (target as SurfaceListBoxItem).DataContext, sourceList.Tag.ToString(), targetList.Tag.ToString()))
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }


        private void OnDropTargetDragLeave(object sender, SurfaceDragDropEventArgs e)
        {
            e.Effects = e.Cursor.AllowedEffects;
        }

        private void OnTargetChanged(object sender, TargetChangedEventArgs e)
        {
            FrameworkElement sourceList = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource);
            FrameworkElement targetList = getFrameworkElement<SurfaceListBox>(e.Cursor.CurrentTarget);
            FrameworkElement targetItem = getFrameworkElement<SurfaceListBoxItem>(e.Cursor.CurrentTarget);


            if (sourceList == null || sourceList.Tag == null)
            {
                e.Cursor.Visual.Tag = "CannotDrop";
                return;
            }

            if (targetList != null && targetList.Tag != null)
            {
                if (targetList.Tag.ToString() == "sourceListP1" || targetList.Tag.ToString() == "sourceListP2")
                {
                    e.Cursor.Visual.Tag = (this.DataContext as GameVM).canDrop(e.Cursor.Data, (targetList as SurfaceListBox).ItemsSource, sourceList.Tag.ToString(), targetList.Tag.ToString()) ? "CanDrop" : "CannotDrop";
                }
                else if (targetItem != null)
                {
                    e.Cursor.Visual.Tag = (this.DataContext as GameVM).canDrop(e.Cursor.Data, targetItem.DataContext, sourceList.Tag.ToString(), targetList.Tag.ToString()) ? "CanDrop" : "CannotDrop";
                }
            }
            else
            {
                e.Cursor.Visual.Tag = "CannotDrop";
            }
        }

        private void OnDropTargetDrop(object sender, SurfaceDragDropEventArgs e)
        {
            if (sender is SurfaceListBox && (sender as SurfaceListBox).Tag != null)
            {
                FrameworkElement targetItem = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);

                switch ((sender as SurfaceListBox).Tag.ToString())
                {
                    case "sourceListP1":
                    case "sourceListP2":
                        (this.DataContext as GameVM).addToSourceList(e.Cursor.Data, (sender as SurfaceListBox).ItemsSource);
                        break;
                    case "targetListP1":
                    case "targetListP2":
                        FrameworkElement sourceList = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource);
                        if(sourceList != null && sourceList.Tag != null)
                        {
                            switch (sourceList.Tag.ToString())
                            {
                                case "sourceListP1":
                                case "sourceListP2":
                                    (this.DataContext as GameVM).addToTargetList(e.Cursor.Data, (targetItem as SurfaceListBoxItem).DataContext, (sourceList as SurfaceListBox).ItemsSource);
                                    break;
                                case "targetListP1":
                                case "targetListP2":
                                    (this.DataContext as GameVM).sortStm(e.Cursor.Data, targetItem.DataContext);
                                    break;
                                default:
                                    //Nothing
                                    break;
                            }
                        }
                        break;
                    default:
                        //Nothing
                        break;
                }
            }
        } 
    }
}
