using System.Windows;
using Account_Project.Model;
using Account_Project.ViewModel;

namespace Account_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
              
        public MainWindow(User user)
        {
            InitializeComponent();
            Closing += (sender, e) => Application.Current.Shutdown();
            DataContext = new MainAccountViewModel(user, this);
            
        }
    }
}
