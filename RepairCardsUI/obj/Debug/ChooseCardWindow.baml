   M S B A M L   `   `   `  ????H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    RepairCardsUI$   RepairCardsUI.ChooseCardWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation  	 
   5   	   Qmahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  	 
   5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow    WindowTransitionsEnabled  .   ? Title$* $Выбор карты ремонта??5   	   $	??450??6%   $
??1000??62    ? WindowStartupLocation$ CenterScreen=?5   	   .??5      %  Xceed.Wpf.Toolkit.BusyIndicator  -   5          Name$ busyIndicator??5         BusyContent$ Подождите??60   .??5   
   ?  ? RowDefinitions 5      ?? 5       ? ColumnDefinitions 5      ?? 5      ?? 5      $	??200 ?6#   .+?5      ? $??0??6   $??Stretch=?6%   $	??Top=?6C   $??30??6[   ???#e1e1e1.+?5      ?? -   5         ??Name$ refreshButton??5      .??6L   Обновить6V   5      * 
 $Telerik.Windows.Controls.RadGridView  5      -   5          Name$ cardsRadGridView??5   /   $??0??6"   ; 
 5Telerik.Windows.Controls.GridView.GridViewDataControl	   Columns	 5      1 
 +Telerik.Windows.Controls.GridViewDataColumn  5      6 
 0Telerik.Windows.Controls.GridViewBoundColumnBase
   Header$
 
Номер??61   - 
 'Telerik.Windows.Controls.GridViewColumn   Width? 
 9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 150 6e      DataMemberBinding 6@   ?? *Number+6e     5       $
 Тип ремонта??61   $	 150 6t    6F   ?? *RepairType.Name+6t     5!      $
 Цех??61   $ 80 6g    6>   ?? *
Department+6g     5"      $#
 Откуда поступил??61   $	 150 6o    6J   ?? *Source+6o     5#      $%
 Дата поступления??61      DataFormatString$  {0:dd/MM/yyyy}??6n   $	 150 6?    6K   ?? *Date+6?     5$      $#
 Номер накладной??61   $	 150 6v    6J   ?? *InvoiceNumber+6v     5%      $
 
Заказ??61   $	 150 6d    6@   ?? *Order+6d     5&      $)
 #Заказ производства??61   $	 150 6q    6M   ?? *Stage+6q     5'      $#
 Заводской номер??61   $	 150 6v    6J   ?? *FactoryNumber+6v     5(      $5
 /Направление деятельности??61   $	 150 6{    6S   ?? *	Direction+6{     5)      $
 Шифр??61   $	 150 6d    6?   ?? *Cipher+6d     5*      $
 Заказ клиента??61   $	 150 6r    6H   ?? *ClientOrder+6r     5+      $'
 !Децимальный номер??61   $	 150 6v    6L   ?? *ProductCode+6v     5,      $
 Наименование??61   $	 150 6q    6G   ?? *ProductName+6q     5-      $'
 !Основание ремонта??61   $	 150 6z    6L   ?? *ReasonForRepair+6z   5/      ?? 51      $??1??6   $??0??6)   .+?52      ?? -   5        $ acceptButton??52      .??6J   OK6N   53      55      56      58      