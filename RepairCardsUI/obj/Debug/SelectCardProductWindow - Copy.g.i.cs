﻿#pragma checksum "..\..\SelectCardProductWindow - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "09B569C7FD287FBB59015AF2A65E87666CD4ED625E3AA7F26EB6591F1CC937CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Behaviors;
using Telerik.Windows.Controls.Data.PropertyGrid;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Controls.MultiColumnComboBox;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeListView;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using Telerik.Windows.Input.Touch;
using Telerik.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace RepairCardsUI {
    
    
    /// <summary>
    /// SelectCardProductWindow
    /// </summary>
    public partial class SelectCardProductWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.BusyIndicator busyIndicator;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refreshButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView productsRadGridView;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadDataPager productsRadDataPager;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.NumericUpDown countNumericUpDown;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\SelectCardProductWindow - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button acceptButton;
        
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
            System.Uri resourceLocater = new System.Uri("/RepairCardsUI;component/selectcardproductwindow%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SelectCardProductWindow - Copy.xaml"
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
            
            #line 11 "..\..\SelectCardProductWindow - Copy.xaml"
            ((RepairCardsUI.SelectCardProductWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MetroWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.busyIndicator = ((Xceed.Wpf.Toolkit.BusyIndicator)(target));
            return;
            case 3:
            this.refreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\SelectCardProductWindow - Copy.xaml"
            this.refreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.productsRadGridView = ((Telerik.Windows.Controls.RadGridView)(target));
            
            #line 33 "..\..\SelectCardProductWindow - Copy.xaml"
            this.productsRadGridView.Copying += new System.EventHandler<Telerik.Windows.Controls.GridViewClipboardEventArgs>(this.ProductsRadGridView_Copying);
            
            #line default
            #line hidden
            return;
            case 5:
            this.productsRadDataPager = ((Telerik.Windows.Controls.RadDataPager)(target));
            return;
            case 6:
            this.countNumericUpDown = ((MahApps.Metro.Controls.NumericUpDown)(target));
            return;
            case 7:
            this.acceptButton = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\SelectCardProductWindow - Copy.xaml"
            this.acceptButton.Click += new System.Windows.RoutedEventHandler(this.AcceptButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

