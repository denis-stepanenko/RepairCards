   M S B A M L   `   `   `  ????H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   H DRepairCardsUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:RepairCardsUIRepairCardsUI 5   	   0   *RepairCardsUI.SelectProductMaterialsWindow   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Omahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -localclr-namespace:RepairCardsUI     5   	     ? Title$&   Добавление по ДСЕ??5	   	   $	??610??6#   $
??1000??60    ? WindowStartupLocation$ CenterScreen=?5
   	   .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      ?? 5      ?? 5      $
??auto ?6   .+?5   
   ?? -   5         ??Name$ filterTextBox??5      6)   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ productsDataGrid??5       $??1??6      
IsReadOnly .    AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Код??6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 200 6K   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 62   ?? *Code+6K     5      $ Наименование??6%   $		 400 6T   
 6;   ?? *Name+6T   5        5   
   -   5        $ materialsDataGrid??5       $??2??6    .  .   5        5      $ Код??6%   $		 200 6K   
 62   ?? *Code+6K     5      $ Наименование??6%   $		 400 6T   
 6;   ?? *Name+6T     5       $ Количество??6%   
 69   ?? *Count+  5!      $' !Единица измерения??6%   
 6@   ?? *	Unit.Name+  5"      $ Размер??6%   
 65   ?? *Size+  5#      $ Тип??6%   
 62   ?? *Type+5%      ?? 5'   
   -   5         ??Name$ okButton??5'      $??3??6   .??6G   Добавить6Q   5(      5)      