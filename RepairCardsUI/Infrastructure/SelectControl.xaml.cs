using System.Windows;
using System.Windows.Controls;

namespace RepairCardsUI.Infrastructure
{
    public partial class SelectControl : UserControl
    {
        public delegate void ClickHandler();
        public event ClickHandler Click;

        public delegate void ClearHandler();
        public event ClearHandler Clear;

        public SelectControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public bool ShowClearButton
        {
            get { return (bool)GetValue(ShowClearButtonProperty); }
            set { SetValue(ShowClearButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowClearButtonProperty =
            DependencyProperty.Register("ShowClearButton", typeof(bool), typeof(SelectControl), new PropertyMetadata(false));



        public bool ShowChooseButton
        {
            get { return (bool)GetValue(ShowChooseButtonProperty); }
            set { SetValue(ShowChooseButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowChooseButtonProperty =
            DependencyProperty.Register("ShowChooseButton", typeof(bool), typeof(SelectControl), new PropertyMetadata(true));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SelectControl), new PropertyMetadata(""));


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear?.Invoke();
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke();
        }

    }
}
