﻿#pragma checksum "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E545A34AFA0AA2C252F788BAF313E0EBE370ADBBF7627632F25C223C16FAD411"
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
    /// ProductsMaterialsAndOperationsReportPage
    /// </summary>
    public partial class ProductsMaterialsAndOperationsReportPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filterTextBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid cardsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid selectedCardsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Forms.Integration.WindowsFormsHost windowsFormsHost;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/RepairCardsUI;component/reports/pages/productsmaterialsandoperationsreportpage.x" +
                    "aml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
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
            this.filterTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
            this.filterTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.filterTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cardsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 33 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
            this.cardsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.cardsDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.showButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
            this.showButton.Click += new System.Windows.RoutedEventHandler(this.ShowButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.selectedCardsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 44 "..\..\..\..\Reports\Pages\ProductsMaterialsAndOperationsReportPage.xaml"
            this.selectedCardsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.selectedCardsDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.windowsFormsHost = ((System.Windows.Forms.Integration.WindowsFormsHost)(target));
            return;
            case 6:
            this.reportViewer = ((Microsoft.Reporting.WinForms.ReportViewer)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
