   M S B A M L   `   `   `  ????   RepairCardsUI)    #RepairCardsUI.Pages.CardDetailsPage   ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     @d2http://schemas.microsoft.com/expression/blend/2008     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .??%  Xceed.Wpf.Toolkit.BusyIndicator  -      Name$  busyIndicator??   BusyContent$ Подождите??.???? .??? $	??Top=? ? ColumnDefinitions ?? $	??200 ???  ? RowDefinitions ?? ?? ?? ?? .+??? $??0??&$Внешняя дефектация:?? -    ??Name$ externalDefectsTextBox??$??0??$??1?? ?? TextWrapping$
 Wrap=? ?? AcceptsReturn . $	??180?? ?? 	MaxLength$
 8000??" ?? VerticalScrollBarVisibility$
 Auto=??? $??1??$
??Wrap=???Проверка эл. параметров в объеме ПСИ, описание обнаруженных неисправностей:?? -   $ malfunctionsTextBox??$??1??$??1??$
 Wrap=? . $	??180??$
 8000??$
 Auto=??? $??2??,*Внутренняя дефектация:?? -   $ internalDefectsTextBox??$??2??$??1??$
 Wrap=? . $	??180??$
 8000??$
 Auto=??? -   	 ??Name$	 
saveButton??$??3??$??2??$??Right=?.??Сохранить