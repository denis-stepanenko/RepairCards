﻿#pragma checksum "..\..\CardWindow - Копировать.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A9ABD347638317E63269D904AB3566CFECE0D54FC750D75BDC1BBE5218111E42"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RepairCardsUI;
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
    /// CardWindow
    /// </summary>
    public partial class CardWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\CardWindow - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink cardHyperlink;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\CardWindow - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink detailsHyperlink;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\CardWindow - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink planOperationsHyperlink;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\CardWindow - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Hyperlink factOperationsHyperlink;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\CardWindow - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame mainFrame;
        
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
            System.Uri resourceLocater = new System.Uri("/RepairCardsUI;component/cardwindow%20-%20%d0%9a%d0%be%d0%bf%d0%b8%d1%80%d0%be%d0" +
                    "%b2%d0%b0%d1%82%d1%8c.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CardWindow - Копировать.xaml"
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
            this.cardHyperlink = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 20 "..\..\CardWindow - Копировать.xaml"
            this.cardHyperlink.Click += new System.Windows.RoutedEventHandler(this.cardHyperlink_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.detailsHyperlink = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 24 "..\..\CardWindow - Копировать.xaml"
            this.detailsHyperlink.Click += new System.Windows.RoutedEventHandler(this.DetailsHyperlink_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.planOperationsHyperlink = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 28 "..\..\CardWindow - Копировать.xaml"
            this.planOperationsHyperlink.Click += new System.Windows.RoutedEventHandler(this.planOperationsHyperlink_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.factOperationsHyperlink = ((System.Windows.Documents.Hyperlink)(target));
            
            #line 32 "..\..\CardWindow - Копировать.xaml"
            this.factOperationsHyperlink.Click += new System.Windows.RoutedEventHandler(this.FactOperationsHyperlink_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.mainFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

