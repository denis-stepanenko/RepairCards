   M S B A M L   `   `   `  ???? H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   RepairCardsUI$   RepairCardsUI.ChooseCardWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation  	 
   Qmahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  	 
   -   (   "MahApps.Metro.Controls.MetroWindow    WindowTransitionsEnabled  .   ? Title$* $Выбор карты ремонта??$	??450??$
??1000?? ? WindowStartupLocation$ CenterScreen=?.??%  Xceed.Wpf.Toolkit.BusyIndicator  -     Name$ busyIndicator??   BusyContent$ Подождите??.???  ? RowDefinitions ??  ? ColumnDefinitions ?? ?? $	??200 ?.+?? $??0??$??Stretch=?$	??Top=?$??30?????#e1e1e1.+??? -    ??Name$ refreshButton??.??Обновить* 
 $Telerik.Windows.Controls.RadGridView  -     Name$ cardsRadGridView??$??0??; 
 5Telerik.Windows.Controls.GridView.GridViewDataControl	   Columns	 1 
 +Telerik.Windows.Controls.GridViewDataColumn  6 
 0Telerik.Windows.Controls.GridViewBoundColumnBase
   Header$
 
Номер??- 
 'Telerik.Windows.Controls.GridViewColumn   Width? 
 9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 150    DataMemberBinding ?? *Number+  $
 Тип ремонта??$	 150  ?? *RepairType.Name+  $
 Цех??$ 80  ?? *
Department+  $#
 Откуда поступил??$	 150  ?? *Source+  $%
 Дата поступления??   DataFormatString$  {0:dd/MM/yyyy}??$	 150  ?? *Date+  $#
 Номер накладной??$	 150  ?? *InvoiceNumber+  $
 
Заказ??$	 150  ?? *Order+  $)
 #Заказ производства??$	 150  ?? *Stage+  $#
 Заводской номер??$	 150  ?? *FactoryNumber+  $5
 /Направление деятельности??$	 150  ?? *	Direction+  $
 Шифр??$	 150  ?? *Cipher+  $
 Заказ клиента??$	 150  ?? *ClientOrder+  $'
 !Децимальный номер??$	 150  ?? *ProductCode+  $
 Наименование??$	 150  ?? *ProductName+  $'
 !Основание ремонта??$	 150  ?? *ReasonForRepair+?? $??1??$??0??.+??? -   $ acceptButton??.??OK