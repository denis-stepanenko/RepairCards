   M S B A M L   `   `   `  ????H  DRepairCardsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RepairCardsUI.PagesRepairCardsUI.Pages  5      ,    &RepairCardsUI.Pages.CardChildCardsPage   5      ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -local!clr-namespace:RepairCardsUI.Pages  5      n jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
  67     N? Title$  CardChildCardsPage??5
      .??5      ?? .??5   
   ?  ? ColumnDefinitions 5      ?? 5      ?? 5      $
??auto ?6#   ?? 5       ? RowDefinitions 5      ?? 5      $	??300 ?6    ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    ?? 5      $	??300 ?6    ?? 5      $
??auto ?6    .+?5      ? $??3??6    5      ?? 5      $
??auto ?6$   ?? 5      $
??auto ?6$   ?? 5       .+?5#      ?? $??20(?6   Все карты65   (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5%      -   5          Name$ filterTextBox??5%   G      	Watermark$ 
Поиск??6(   $??1??6:   6^   *  $Telerik.Windows.Controls.RadGridView  51      -   5          Name$ cardsDataGrid??52   "   $??2??51   &   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   SearchPanelBackground
 ????    ShowSearchPanel .     Columns 55      1  +Telerik.Windows.Controls.GridViewDataColumn  56      6  0Telerik.Windows.Controls.GridViewBoundColumnBase	   Header$	 
Номер??65   
   DataMemberBinding
 6D   ?? *Number+  57      $5	 /Направление деятельности??65   
 6W   ?? *	Direction+  58      $	 Шифр??65   
 6C   ?? *Cipher+  59      $	 Код??65   
 6B   ?? *ProductCode+  5:      $	 Наименование??65   
 6K   ?? *ProductName+5=      5>      ?? 5@      -   5         ??Name$ addDismantledCardButton??5@   ?   $??1??6   $??Center=?6"   $??>>??6?    ? LayoutTransform 5A      ?? 5B       ?? Angle$ 90Q?6&   5D      ?? 5F      -   5        $ addFromCardsButton??5F   O   $??1??6   $??2??6"   $??Center=?62   $??>>??6?    5G      ?? 5H      $ 90Q?6&   5J      ?? 5L      $??2??6   $??20(?6%   Демонтировано6B   ?? 5M      $??2??6   $??2??6%   $??20(?65   $
??Wrap=?6C   Установлено6d   ? 5O      $??3??6    5P      ?? 5Q      $
??auto ?6$   ?? 5R      .+?5U      ? .+?5V      ?? -   5        $  removeDismantledCardButton??5V      .??6j   Удалить6s   5W        5Y      -   5        $ childCardsDataGrid??5Z   "   $??1??5Y   &   $??Stretch=?5[   "   
 ????  5^        5_      $	 
Номер??65   
 6D   ?? *Number+  5`      $5	 /Направление деятельности??65   
 6W   ?? *	Direction+  5a      $	 Шифр??65   
 6C   ?? *Cipher+  5b      $	 Код??65   
 6B   ?? *ProductCode+  5c      $	 Наименование??65   
 6K   ?? *ProductName+5f      5g      ?? 5i      -   5        $ 	addButton??5i      $??3??6@   $??1??6M   $??Center=?6]   .??6x   >>6|   ? 5k      $??3??6   $??2??6    $??Stretch=?60    5l      ?? 5m      $
??auto ?6$   ?? 5n      .+?5q      ? .+?5r      ?? -   5        $ removeButton??5r      .??6N   Удалить6W   5s        5u      -	   5        $ installedCardsDataGrid??5v   "   $??1??5u   &   
 ???? $??Stretch=?5x   "    5z        5{      $	 
Номер??65   
 6D   ?? *Number+  5|      $5	 /Направление деятельности??65   
 6W   ?? *	Direction+  5}      $	 Шифр??65   
 6C   ?? *Cipher+  5~      $	 Код??65   
 6B   ?? *ProductCode+  5      $	 Наименование??65   
 6K   ?? *ProductName+5?      5?      9? 5?      $??4??6   $??3??6$   $??Дерево??68   .??5?      ? $	??600??6    5?      ?? 5?      $
??auto ?6(   ?? 5?      $
??auto ?6(   ?? 5?       5?      ?? 5?      ?? 5?      .+?5?      ?? -
   5        $ refreshTreesButton??5?   1   $??2??6   .??6n   Обновить6x   ?? 5?      $??1??6    $??20(?6-   IGДерево карт демонтированных продуктов6b   ?? 5?      $??1??6    $??1??6-   $??20(?6=   ECДерево карт установленных продуктов6p   R? 5?      -   5         R?Name$ cardTreeTreeListView??5?   ,   $??2??6   ??5?      ?? 5?       ?? ItemsSource 67   ??  ?? Path$ Children?.R?5?   "   ?? ?? 6-   $
??Bold"?6I   ?61   ?? *Number+6I   , ?? 6_   ?6c   ?? *	Direction+, ?? 6?   ?6?   ?? *Cipher+, ?? 6?   ?6?   ?? *Code+, ?? 6?   ?6?   ?? *Name+6?   5?      5?      R? 5?      -   5        $* $cardTreeWithSubtitutionsTreeListView??5?   <   $??2??6   $??1??6,   ??5?      ?? 5?       67   ?? $ Children?.R?5?   "   ?? ?? 6-   $
??Bold"?6I   ?61   ?? *Number+6I   , ?? 6_   ?6c   ?? *	Direction+, ?? 6?   ?6?   ?? *Cipher+, ?? 6?   ?6?   ?? *Code+, ?? 6?   ?6?   ?? *Name+6?   5?      5?      5?      5?      5?      5?      5?      