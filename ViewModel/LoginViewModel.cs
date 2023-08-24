using Account_Project.Model;
using System.Windows.Input;
using System.Windows;

namespace Account_Project
{
    public class LoginViewModel : ViewModelBase
    {

        private Window thisWindow = null;

        private string username;
        private string passwordBox;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PasswordBox
        {
            get { return passwordBox; }
            set
            {
                if (passwordBox != value)
                {
                    passwordBox = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(Window thisWindow)
        {
            this.thisWindow = thisWindow;
            LoginCommand = new RelayCommand(LoginButton);

        }

        public void LoginButton()
        {

            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(PasswordBox))
            {
                if (MsSqlDataProvider.IsValidLogin(Username, PasswordBox))
                {

                    new MainWindow(MsSqlDataProvider.GetUser(Username, PasswordBox)).Show(); //можно сделть запуск в новом потоке, через application...
                    thisWindow.Close();


                }


            }


        }
    }
}
