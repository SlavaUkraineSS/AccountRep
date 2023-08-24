using System.Windows;

namespace Account_Project
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            LoginGrid.Visibility = Visibility.Visible;
            RegGrid.Visibility = Visibility.Collapsed;

            DataContext = new LoginViewModel(this);

        }

        private void onLogin_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LoginViewModel(this);
            LoginGrid.Visibility = Visibility.Visible;
            RegGrid.Visibility = Visibility.Collapsed;



        }

        private void onCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RegistrationViewModel();
            LoginGrid.Visibility = Visibility.Collapsed;
            RegGrid.Visibility = Visibility.Visible;

        }
    }
}
