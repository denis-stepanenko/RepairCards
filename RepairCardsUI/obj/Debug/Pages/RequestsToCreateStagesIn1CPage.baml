   M S B A M L   `   `   `  ????  RepairCardsUI8    2RepairCardsUI.Pages.RequestsToCreateStagesIn1CPage   5      ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      n jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 5      X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .??5   
   ?   ? RowDefinitions  5      ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    ?? 5      .+?5      9? $??Все карты??6    9? 
IsExpanded . .??5      ?? .+?5      ?? $??20(?6    Все карты69   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5      -   5          Name$ filterTextBox??5   >      	Watermark$ 
Поиск??6,   6U   &   System.Windows.Controls.DataGrid  5      -   5          Name$ cardsDataGrid??5      $	??150??66      AutoGenerateColumns .  $??Stretch=?6_      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер??61   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6@   ?? *Number+  5       $ Код??61   	 6>   ?? *ProductCode+  5!      $ Наименование??61   	 6G   ?? *ProductName+5#      ?? 5%      -   5        
 ??Name$
 	addButton??5%      .??6H   Создать заявку6X   5&      5'      ?? 5*      $??1??6   $??20(?6%   Заявки6;   ? 5,      $??2??6   .+?5-      ?? -   5        $
 removeButton??5-      .??6J   Удалить6S   ?? 5.      -   5        $ 
 cancelRequestClosingButton??5.      .??6f   #!Отменить закрытие6y   ?? 5/      -   5        $
 refreshButton??5/      .??6L   Обновить6V   50      ?? 52      -   5        $
 closeRequestButton??52   "   $??3??6   .??6_   Закрыть заявку6o   *  $Telerik.Windows.Controls.RadGridView  54      -   5          Name$ itemsRadGridView??55   "   $??4??54   "   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   SearchPanelBackground
 ????    Columns 58      1  +Telerik.Windows.Controls.GridViewDataColumn  59      6	  0Telerik.Windows.Controls.GridViewBoundColumnBase 	  Header$ Цех??61   -
  'Telerik.Windows.Controls.GridViewColumn 
  Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$	 100 6g    	  DataMemberBinding 6>   ?? *
Department+6g     5:      $ Дата??61    	  DataFormatString$  {0:dd/MM/yyyy HH:mm}??6b   $	 150 6?    6?   ?? *Date+6?     5;      $ Дата закрытия??61   $  {0:dd/MM/yyyy HH:mm}??6p   $	 200 6?    6H   ?? *	CloseDate+6?     5<      $ Номер карты??61   $	 180 6o    6F   ?? *
CardNumber+6o     5=      $ Заказчик??61   $	 180 6n    6C   ?? *CustomerName+6n     5>      $ Исполнитель??61   $	 180 6q    6F   ?? *ExecutorName+6q      RowStyle 5A      ?? 5B       ?? 
TargetType3  -Telerik.Windows.Controls.GridView.GridViewRow"  6    ?? Triggers 5C      ?? 5D       ?? Binding 6*   ?? *
IsClosed+ ?? Value True6G   .a?5E   "   ??  ?? Property ? ?? ?? Value
 ?????6?   5F      5H      5K      5M      5O      