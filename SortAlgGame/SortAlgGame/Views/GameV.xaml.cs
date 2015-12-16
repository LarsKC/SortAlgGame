using System;
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

        public bool onPlayerList(FrameworkElement source, FrameworkElement target)
        {
            if (source is SurfaceListBox && target is SurfaceListBox)
            {
                if ((source as SurfaceListBox).Name.Equals("sourceListP1") && (target as SurfaceListBox).Name.Equals("targetListP1"))
                {
                    return true;
                }
                if ((source as SurfaceListBox).Name.Equals("sourceListP2") && (target as SurfaceListBox).Name.Equals("targetListP2"))
                {
                    return true;
                }
            }
            return false;
        }

        private void sourceList_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            FrameworkElement source = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);
            //Wurde kein SurfaceListBoxItem gefunden -> return
            if (source == null) return;
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
            FrameworkElement source = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);
            FrameworkElement listSource = getFrameworkElement<SurfaceListBox>(e.OriginalSource as FrameworkElement);
            FrameworkElement listcursor = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource);
            if (!(onPlayerList(listcursor, listSource) && (this.DataContext as GameVM).canDrop(source)))
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
            FrameworkElement target = getFrameworkElement<SurfaceListBoxItem>(e.Cursor.CurrentTarget);
            FrameworkElement listSource = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource);
            FrameworkElement listtarget = getFrameworkElement<SurfaceListBox>(e.Cursor.CurrentTarget);
            e.Cursor.Visual.Tag = ((this.DataContext as GameVM).canDrop(target) && onPlayerList(listSource, listtarget)) ? "CanDrop" : "CannotDrop";
        }

        private void OnDropTargetDrop(object sender, SurfaceDragDropEventArgs e)
        {
            FrameworkElement target = getFrameworkElement<SurfaceListBoxItem>(e.OriginalSource as FrameworkElement);
            FrameworkElement targetList = getFrameworkElement<SurfaceListBox>(e.OriginalSource as FrameworkElement);
            (this.DataContext as GameVM).addToTargetList(e.Cursor, target as SurfaceListBoxItem, targetList as SurfaceListBox);
        }

        private void OnDragCompleted(object sender, SurfaceDragCompletedEventArgs e)
        {
            // If the operation is Move, remove the data from drag source.
            if (e.Cursor.Effects == DragDropEffects.Move)
            {
                SurfaceListBox dragSourceList = getFrameworkElement<SurfaceListBox>(e.Cursor.DragSource) as SurfaceListBox;
                (this.DataContext as GameVM).removeFromSourceList(e.Cursor, dragSourceList);
            }
        }
    }
}
