   M S B A M L   `   `   `  ???? H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  H DRepairCardsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:RepairCardsUI.InfrastructureRepairCardsUI.Infrastructure 3   -RepairCardsUI.OwnProductChooseOperationWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation  	 
   Qmahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  	 
   Ginfrastructure*clr-namespace:RepairCardsUI.Infrastructure  	 
   (   "MahApps.Metro.Controls.MetroWindow    WindowTransitionsEnabled  .   ? Title$8 2Выбор укрупненной операции??$	??450??$	??800?? ? WindowStartupLocation$ CenterScreen=?.??%  Xceed.Wpf.Toolkit.BusyIndicator  -     Name$ busyIndicator??   BusyContent$ Подождите??.???  ? ColumnDefinitions ?? ?? $	??200 ?.+?? $??Stretch=?$	??Top=?$??30?????#e1e1e1.+??? -    ??Name$ refreshButton??.??Обновить* 
 $Telerik.Windows.Controls.RadGridView  -     Name$ operationsRadGridView??; 
 5Telerik.Windows.Controls.GridView.GridViewDataControl   Columns 1 
 +Telerik.Windows.Controls.GridViewDataColumn  6 
 0Telerik.Windows.Controls.GridViewBoundColumnBase	   Header$	 Код??- 
 'Telerik.Windows.Controls.GridViewColumn
   Width? 
 9Telerik.Windows.Controls.GridView.GridViewLengthConverter$
 80    DataMemberBinding ?? *Code+  $	 Наименование??$	
 200  ?? *Name+  $	 Трудоемкость??$	
 120  ?? *Labor+  $	 Цех??$
 80  ?? *
Department+?? $??1??.+?(	  "System.Windows.Controls.DatePicker	  -    	 Name$ dateDatePicker??0
  *RepairCardsUI.Infrastructure.SelectControl
  -    
 Name$ executorSelectControl??0  *RepairCardsUI.Infrastructure.SelectControl   Click$! ExecutorSelectControl_Click??$??1??$??1???? -   $ 	addButton??.??ОК