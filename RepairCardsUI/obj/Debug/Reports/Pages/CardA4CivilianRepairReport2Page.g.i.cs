#pragma checksum "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D3E9E6EDC444D7082497460936105596A85C728D1ECF9366C677B16FF8E4122F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Reporting.WinForms;
using RepairCardsUI.Infrastructure;
using RepairCardsUI.Reports.Pages;
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


namespace RepairCardsUI.Reports.Pages {
    
    
    /// <summary>
    /// CardA4CivilianRepairReport2Page
    /// </summary>
    public partial class CardA4CivilianRepairReport2Page : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RepairCardsUI.Infrastructure.SelectControl cardSelectControl;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        
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
            System.Uri resourceLocater = new System.Uri("/RepairCardsUI;component/reports/pages/carda4civilianrepairreport2page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.showButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\Reports\Pages\CardA4CivilianRepairReport2Page.xaml"
            this.showButton.Click += new System.Windows.RoutedEventHandler(this.ShowButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cardSelectControl = ((RepairCardsUI.Infrastructure.SelectControl)(target));
            return;
            case 3:
            this.windowsFormsHost = ((System.Windows.Forms.Integration.WindowsFormsHost)(target));
            return;
            case 4:
            this.reportViewer = ((Microsoft.Reporting.WinForms.ReportViewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

