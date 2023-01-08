using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace RepairCardsUI
{
    public partial class App : Application
    {
        public App()
        {
            LocalizationManager.Manager = new CustomLocalizationManager();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
$@"Ой, что-то пошло не так.. Покажите текст ошибки разработчикам:
{e.Exception.Message}",
"Необработанное исключение");
        }
    }
}
