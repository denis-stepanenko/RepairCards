using System;
using System.Timers;
using System.Windows;

namespace RepairCardsUI.Infrastructure
{
    class ServerFilteringHelper
    {
        private Timer _timer = new Timer(1000);

        public ServerFilteringHelper(Action work)
        {
            _timer.Elapsed += (s, e) =>
            {
                _timer.Stop();
                Application.Current.Dispatcher.BeginInvoke(new Action(() => work()));
            };
        }

        public void Run() => _timer.Start();
    }
}
