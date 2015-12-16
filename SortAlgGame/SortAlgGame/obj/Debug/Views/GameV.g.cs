﻿#pragma checksum "..\..\..\Views\GameV.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F589935D82F3CE3BBDC8435499762063"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34209
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Controls.Primitives;
using Microsoft.Surface.Presentation.Controls.TouchVisualizations;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Palettes;
using SortAlgGame;
using SortAlgGame.ViewModel;
using SortAlgGame.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SortAlgGame.Views {
    
    
    /// <summary>
    /// GameV
    /// </summary>
    public partial class GameV : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox sourceListP1;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox targetListP1;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox sourceListP2;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox targetListP2;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton surfaceButton1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SortAlgGame;component/views/gamev.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\GameV.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.sourceListP1 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 75 "..\..\..\Views\GameV.xaml"
            this.sourceListP1.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.sourceList_PreviewTouchDown);
            
            #line default
            #line hidden
            
            #line 76 "..\..\..\Views\GameV.xaml"
            this.sourceListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragEnter));
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Views\GameV.xaml"
            this.sourceListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragLeave));
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\Views\GameV.xaml"
            this.sourceListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragCompletedEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragCompletedEventArgs>(this.OnDragCompleted));
            
            #line default
            #line hidden
            return;
            case 2:
            this.targetListP1 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 91 "..\..\..\Views\GameV.xaml"
            this.targetListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragEnter));
            
            #line default
            #line hidden
            
            #line 92 "..\..\..\Views\GameV.xaml"
            this.targetListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragLeave));
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\Views\GameV.xaml"
            this.targetListP1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDrop));
            
            #line default
            #line hidden
            return;
            case 3:
            this.sourceListP2 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 109 "..\..\..\Views\GameV.xaml"
            this.sourceListP2.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.sourceList_PreviewTouchDown);
            
            #line default
            #line hidden
            
            #line 110 "..\..\..\Views\GameV.xaml"
            this.sourceListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragEnter));
            
            #line default
            #line hidden
            
            #line 111 "..\..\..\Views\GameV.xaml"
            this.sourceListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragLeave));
            
            #line default
            #line hidden
            
            #line 112 "..\..\..\Views\GameV.xaml"
            this.sourceListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragCompletedEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragCompletedEventArgs>(this.OnDragCompleted));
            
            #line default
            #line hidden
            return;
            case 4:
            this.targetListP2 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 125 "..\..\..\Views\GameV.xaml"
            this.targetListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragEnter));
            
            #line default
            #line hidden
            
            #line 126 "..\..\..\Views\GameV.xaml"
            this.targetListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDragLeave));
            
            #line default
            #line hidden
            
            #line 127 "..\..\..\Views\GameV.xaml"
            this.targetListP2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDropTargetDrop));
            
            #line default
            #line hidden
            return;
            case 5:
            this.surfaceButton1 = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
