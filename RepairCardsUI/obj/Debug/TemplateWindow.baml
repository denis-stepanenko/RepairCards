   M S B A M L   `   `   `  ????H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    RepairCardsUI"   RepairCardsUI.TemplateWindow   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Omahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   n jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
  5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     ? Title$  Шаблон??5
   	   $	??750??6   $
??1000??6%    ? WindowStartupLocation$ CenterScreen=?5   	   .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??200 ?6   ?? 5       ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $
??auto ?6   ?? 5      $??* ?6   .+?5   
   ?? $??0??6   	Цех:6'   ?? 5   
   -   5         ??Name$ departmentTextBlock??5   1   $??0??6   $??1??6!   61   ?? 5   
   $??1??6   	Код:6'   ?? 5   
   -   5        $ templateIdTextBlock??5   1   $??1??6   $??1??6!   61   ?? 5    
   $??2??6   Наименование:60   ?? 5!   
   -   5         ??Name$ nameTextBox??5!   /   $??2??6   $??1??6    ?? 	MaxLength$	 200??6D   ?? 5"   
   $??3??6   Описание:6,   ?? 5#   
   -   5        $ descriptionTextBox??5$      $??3??5#      $??1??6    ?? TextWrapping$
 Wrap=?5%       ?? AcceptsReturn . $	??180??5'      "	 ?? VerticalScrollBarVisibility$
	 Auto=?5(      $	 300??5)      ?? 5,   
   -   5        
 ??Name$
 acceptButton??5,   f   $??4??6   $??2??6   $??Right=?62   $	??Top=?6N   .??6?   Сохранить6?   ? 5.   
   $??5??6   $??2??6   .+?5/      ? $??Stretch=?6   $	??Top=?66   $??35??6N   ???#e1e1e1.+?50      ?? -   5        $
 	addButton??50      .??6D   Добавить6N   ?? 51      -   5        $
 addFromCardButton??51      .??6T   " Добавить из карты6g   ?? 52      -   5        $
 
editButton??52      .??6F   Изменить6P   ?? 53      -	   5        $
 deleteButton??53      .??6J   Удалить6S   ?? 54      -
   5        $
 refreshButton??54      .??6L   Обновить6V   ?? 55      -   5        $
 moveUpButton??55      .??6J   
Вверх6Q   ?? 56      -   5        $
 moveDownButton??56      .??6N   
Вниз6T   57      *  $Telerik.Windows.Controls.RadGridView  59      -   5          Name$ operationsRadGridView??59   "   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   Columns 5;      1  +Telerik.Windows.Controls.GridViewDataColumn  5<      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Код??61   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 80 6k      DataMemberBinding 6>   ?? *Operation.Code+6k     5=      $ Наименование??61   $	 300 6t    6G   ?? *Operation.Name+6t     5>      $ Количество??61   $	 110 6i    6E   ?? *Count+6i     5?      $' !Единица измерения??61   $	 160 6}    6L   ?? *Operation.UnitName+6}     5@      $ Н/Ч на единицу??61   $	 130 6w    6I   ?? *Operation.Labor+6w     5A      $ Группа??61   $	 200 6s    6A   ?? *Operation.GroupName+6s     5B      $ Цех??61   $ 80 6q    6>   ?? *Operation.Department+6q   5E      5F      5H      5J      