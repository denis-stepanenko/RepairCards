   M S B A M L   `   `   `  ????H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    RepairCardsUI#   RepairCardsUI.OperationWindow   5   	   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   Imahapps;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	     ? Title$  Операция??5	   	   $	??650??6   $	??600??6'    ? WindowStartupLocation$ CenterScreen=?5
   	   .??5      ?? .+?5   
   ?? 	Код:6   ]? 5   
   .+?5      ?? -   5         ??Name$ generateCode??5      $??Right=?5      .??5   4   Получить новый6D   ?? 5      -   5         ??Name$ codeTextBox??5       ?? 	MaxLength$ 50??6+   5      ?? 5   
   Наименование:6#   ?? 5   
   -   5        $ nameTextBox??5      $	 500??6'   ?? 5   
   Трудоемкость:6#   *   $MahApps.Metro.Controls.NumericUpDown  5   
   -   5          Name$ laborNumericUpDown??5   I      Minimum$ 0Q?6       Value$ 0X?6,      HasDecimals . 6I   ?? 5   
   	Цена:6     5   
   -   5        $ priceNumericUpDown??5   I   $ 0Q?6    $ 0X?6,    . 6I   ?? 5   
   $"Единица измерения:6(   ?? 5    
   -   5        $ unitNameTextBox??5       ?? 5"   
   Группа:6   ?? 5#   
   -   5        $ groupNameTextBox??5#      $	 500??6,   ?? 5%   
   	Цех:6   ?? 5&   
   -   5        	 ??Name$	 departmentComboBox??5&      .@?5'      ?? .??6   46   ?? 5(      .??6   56   ?? 5)      .??6   66   ?? 5*      .??6   136   ?? 5+      .??6   176   ?? 5,      .??6   806   ?? 5-      .??6   826   5.      ?? 50   
   Описание:6   ?? 51   
   -	   5        $ descriptionTextBox??51      
 ?? TextWrapping$

 Wrap=?52       ?? AcceptsReturn . $	??180??54      " ?? VerticalScrollBarVisibility$
 Auto=?55      $	 400??56      ?? 58   
   -
   5         ??Name$ isInactiveCheckBox??58      .??6/   =;Операция больше не используется6P   ?? 5:   
   -   5        $ acceptButton??5:   E   $??Right=?6   $	??Top=?6-   .??6v   ОК6z   5;      5=      