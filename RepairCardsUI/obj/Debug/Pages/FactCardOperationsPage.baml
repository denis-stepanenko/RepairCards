   M S B A M L   `   `   `  ????  RepairCardsUI0    *RepairCardsUI.Pages.FactCardOperationsPage   5      ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      n jTelerik.Windows.Controls.GridView, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2019.3.917.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Gtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 5      X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .??5   
   ?   ? RowDefinitions  5      ?? 5      $
??auto ?6    ?? 5      $
??auto ?6    ?? 5      .+?5      ? .+?5      ?? -   5         ??Name$ 	addButton??5      .??6D   Добавить6N   ?? 5      -   5        $ addFromPlanButton??5      .??6T   .,Добавить из планируемых6m   ?? 5      -   5        $ addFromTemplateButton??5      .??6\   &$Добавить из шаблона6q   ?? 5      -   5        $ addFromAnotherCardButton??5      .??6b   /-Добавить из другой карты6|   ?? 5      -   5        $ 
editButton??5      .??6F   Изменить6P   ?? 5      -   5        $ deleteButton??5      .??6J   Удалить6S   ?? 5      -   5        $ refreshButton??5      .??6L   Обновить6V   ?? 5      -   5        $ confirmButton??5      .??6L   Утвердить6W   ?? 5      -	   5        $ unconfirmButton??5      .??6P   #!Снять утверждение6c   5      9? 5      $??1??6   $+??%Групповое изменение??6$   .??5      ? .+?5       ? .+?5!      ?? $??Center=?6    Исполнитель6H   ?? 5"      -
   5         ??Name$ executorsComboBox??5"      $	??200??6:    ?? DisplayMemberPath$
 Name??6F    ?? 
IsEditable .  ?? IsTextSearchEnabled . 6q   ?? 5#      -   5        $ changeExecutorButton??5#      .??6^   Изменить6h   5$      ? 5&      .+?5'      ?? $??Center=?6    
Дата6A   (  "System.Windows.Controls.DatePicker  5(      -   5          Name$ dateDateTimePicker??5(   !   $	??200??6=   ?? 5)      -   5        $ changeDateButton??5)      .??6V   Изменить6`   5*      5+      5,      *  $Telerik.Windows.Controls.RadGridView  5.      -   5          Name$ operationsRadGridView??5.   "   $??2??5/   "   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   SearchPanelBackground
 ???? 	   Columns	 52      1  +Telerik.Windows.Controls.GridViewDataColumn  53      6  0Telerik.Windows.Controls.GridViewBoundColumnBase
   Header$
 Цех??61   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 80 6g      DataMemberBinding 6>   ?? *
Department+6g     54      $
 Утвердил??61   $ 70 6q    6C   ?? *ConfirmUserName+6q     55      $
 Код??61   $ 80 6a    6>   ?? *Code+6a     56      $
 Наименование??61   $	 300 6j    6G   ?? *Name+6j     57      $
 Количество??61   $	 110 6i    6E   ?? *Count+6i     58      $'
 !Единица измерения??61   $	 160 6s    6L   ?? *
UnitName+6s     59      $
 Н/Ч на единицу??61   $	 130 6m    6I   ?? *Labor+6m     5:      $
 Н/Ч всего??61   $	 120 6k    6D   ?? *
LaborAll+6k     5;      $
 Дата??61      DataFormatString$  {0:dd/MM/yyyy}??6b   $	 150 6?    6?   ?? *Date+6?     5<      $
 Исполнитель??61   $	 150 6r    6F   ?? *Executor.Name+6r     5=      $
 Группа??61   $	 200 6i    6A   ?? *	GroupName+6i     5>      $
 Примечание??61   $ 80 6k    6E   ?? *	Comment+6k     5?      $-
 'Наименование шаблона??61   $	 180 6z    6O   ?? *TemplateName+6z   5A      5C      5F      