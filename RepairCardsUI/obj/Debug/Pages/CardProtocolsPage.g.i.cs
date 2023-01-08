#pragma checksum "..\..\..\Pages\CardProtocolsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "12449E98216FB0703C84862377335C6018373035FF30EA90C073178A1FE6289E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using RepairCardsUI.Infrastructure;
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


namespace RepairCardsUI.Pages
{


    /// <summary>
    /// TemplatesPage
    /// </summary>
    public partial class CardProtocolsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {


#line 16 "..\..\..\Pages\CardProtocolsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.BusyIndicator busyIndicator;

#line default
#line hidden


#line 20 "..\..\..\Pages\CardProtocolsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;

#line default
#line hidden


#line 21 "..\..\..\Pages\CardProtocolsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openButton;

#line default
#line hidden


#line 22 "..\..\..\Pages\CardProtocolsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteButton;

#line default
#line hidden


#line 23 "..\..\..\Pages\CardProtocolsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refreshButton;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RepairCardsUI;component/pages/cardprotocolspage.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Pages\CardProtocolsPage.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 11 "..\..\..\Pages\CardProtocolsPage.xaml"
                    ((RepairCardsUI.Pages.TemplatesPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);

#line default
#line hidden
                    return;
                case 2:
                    this.busyIndicator = ((Xceed.Wpf.Toolkit.BusyIndicator)(target));
                    return;
                case 3:
                    this.addButton = ((System.Windows.Controls.Button)(target));

#line 20 "..\..\..\Pages\CardProtocolsPage.xaml"
                    this.addButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.openButton = ((System.Windows.Controls.Button)(target));

#line 21 "..\..\..\Pages\CardProtocolsPage.xaml"
                    this.openButton.Click += new System.Windows.RoutedEventHandler(this.OpenButton_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.deleteButton = ((System.Windows.Controls.Button)(target));

#line 22 "..\..\..\Pages\CardProtocolsPage.xaml"
                    this.deleteButton.Click += new System.Windows.RoutedEventHandler(this.DeleteButton_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.refreshButton = ((System.Windows.Controls.Button)(target));

#line 23 "..\..\..\Pages\CardProtocolsPage.xaml"
                    this.refreshButton.Click += new System.Windows.RoutedEventHandler(this.refreshButton_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.templatesRadGridView = ((Telerik.Windows.Controls.RadGridView)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal Telerik.Windows.Controls.RadGridView protocolsRadGridView;
    }
}

