   M S B A M L   `   `   `  ????   RepairCardsUI,    &RepairCardsUI.Pages.ExportRequestsPage   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     @d2http://schemas.microsoft.com/expression/blend/2008     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     n jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j
 fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .??%  Xceed.Wpf.Toolkit.BusyIndicator  -      Name$  busyIndicator??   BusyContent$ Подождите??.???  ? RowDefinitions ?? $
??auto ??? $
??auto ??? ?? $
??auto ??? $
??auto ??? $
??auto ??? $
??auto ??? .+??? $??0??$??20(?Все карты(  "Xceed.Wpf.Toolkit.WatermarkTextBox  -     Name$ filterTextBox??   	Watermark$ 
Поиск??$??1??&   System.Windows.Controls.DataGrid  -     Name$ cardsDataGrid??$??2??   AutoGenerateColumns .  $??Stretch=?   
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Номер??1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 ?? *Number+  $	 Код??
 ?? *ProductCode+  $	 Наименование??
 ?? *ProductName+?? -    ??Name$ 	addButton??$??3??.??Создать заявку?? $??4??$??20(?Заявки? $??5??.+??? -   $ removeButton??.??Удалить?? -   $  cancelRequestClosingButton??.??#!Отменить закрытие?? -   $ refreshButton??.??Обновить?? -   $" confirmDeficitCreationButton??.??Дефицит создан?? -	   $ closeRequestButton??$??6??.??Закрыть заявку*  $Telerik.Windows.Controls.RadGridView  -
     Name$ itemsRadGridView??$??7??;  5Telerik.Windows.Controls.GridView.GridViewDataControl   SearchPanelBackground
 ????    Columns 1	  +Telerik.Windows.Controls.GridViewDataColumn	  6
  0Telerik.Windows.Controls.GridViewBoundColumnBase 
  Header$ Цех??-  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100  
  DataMemberBinding ?? *
Department+	  $ Дата?? 
  DataFormatString$  {0:dd/MM/yyyy HH:mm}??$	 150  ?? *Date+	  $ Дата закрытия??$  {0:dd/MM/yyyy HH:mm}??$	 200  ?? *	CloseDate+	  $ Номер карты??$	 180  ?? *
CardNumber+	  $ Заказчик??$	 180  ?? *CustomerName+	  $ Исполнитель??$	 180  ?? *ExecutorName+	  $0 *Дата создания дефицита??$  {0:dd/MM/yyyy HH:mm}??$	 200  ?? *DeficitCreationDate+   RowStyle ??  ?? 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow"   ?? Triggers ??  ?? Binding ?? *
IsClosed+ ?? Value True.a???  ?? Property ? ?? ?? Value
 ?????