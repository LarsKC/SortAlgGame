﻿#pragma checksum "..\..\..\Views\GameV.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C4B207A005782AA6EFBAA0C2EFAE99A4"
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
        
        
        #line 65 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox surfaceList1;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox surfaceList2;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox surfaceList3;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceListBox surfaceList4;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton surfaceButton1;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton surfaceButton2;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton surfaceButton3;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\Views\GameV.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton surfaceButton4;
        
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
            this.surfaceList1 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 76 "..\..\..\Views\GameV.xaml"
            this.surfaceList1.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OnTouchDown);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Views\GameV.xaml"
            this.surfaceList1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragEnter));
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\Views\GameV.xaml"
            this.surfaceList1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragLeave));
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\Views\GameV.xaml"
            this.surfaceList1.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDrop));
            
            #line default
            #line hidden
            return;
            case 2:
            this.surfaceList2 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 93 "..\..\..\Views\GameV.xaml"
            this.surfaceList2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragEnter));
            
            #line default
            #line hidden
            
            #line 94 "..\..\..\Views\GameV.xaml"
            this.surfaceList2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragLeave));
            
            #line default
            #line hidden
            
            #line 95 "..\..\..\Views\GameV.xaml"
            this.surfaceList2.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDrop));
            
            #line default
            #line hidden
            
            #line 96 "..\..\..\Views\GameV.xaml"
            this.surfaceList2.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OnTouchDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.surfaceList3 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 114 "..\..\..\Views\GameV.xaml"
            this.surfaceList3.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OnTouchDown);
            
            #line default
            #line hidden
            
            #line 115 "..\..\..\Views\GameV.xaml"
            this.surfaceList3.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragEnter));
            
            #line default
            #line hidden
            
            #line 116 "..\..\..\Views\GameV.xaml"
            this.surfaceList3.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragLeave));
            
            #line default
            #line hidden
            
            #line 117 "..\..\..\Views\GameV.xaml"
            this.surfaceList3.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDrop));
            
            #line default
            #line hidden
            return;
            case 4:
            this.surfaceList4 = ((Microsoft.Surface.Presentation.Controls.SurfaceListBox)(target));
            
            #line 131 "..\..\..\Views\GameV.xaml"
            this.surfaceList4.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OnTouchDown);
            
            #line default
            #line hidden
            
            #line 132 "..\..\..\Views\GameV.xaml"
            this.surfaceList4.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragEnterEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragEnter));
            
            #line default
            #line hidden
            
            #line 133 "..\..\..\Views\GameV.xaml"
            this.surfaceList4.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DragLeaveEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDragLeave));
            
            #line default
            #line hidden
            
            #line 134 "..\..\..\Views\GameV.xaml"
            this.surfaceList4.AddHandler(Microsoft.Surface.Presentation.SurfaceDragDrop.DropEvent, new System.EventHandler<Microsoft.Surface.Presentation.SurfaceDragDropEventArgs>(this.OnDrop));
            
            #line default
            #line hidden
            return;
            case 5:
            this.surfaceButton1 = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            case 6:
            this.surfaceButton2 = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            case 7:
            this.surfaceButton3 = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            case 8:
            this.surfaceButton4 = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

