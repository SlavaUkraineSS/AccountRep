using Account_Project.Model;
using System;
using System.Windows.Input;

namespace Account_Project
{
    public class RegistrationViewModel : ViewModelBase
    {

        private string name, errorMessageName;

        private string password, errorMessagePassword;



        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }

            }
        }

        public string ErrorMessageName
        {
            get { return errorMessageName; }
            set { errorMessageName = value; OnPropertyChanged(); }
        }

        public string ErrorMessagePassword
        {
            get { return errorMessagePassword; }
            set { errorMessagePassword = value; OnPropertyChanged(); }
        }


        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel()
        {
            RegisterCommand = new RelayCommand(RegistrationNewUser);
        }

        private void RegistrationNewUser()
        {

            if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Name))
            {
                if (MsSqlDataProvider.NameIsTaken(Name))
                {
                    ErrorMessageName = "Данное имя занято";
                }
                else
                {
                    MsSqlDataProvider.AddNewUser(new User() { id = Guid.NewGuid().ToString(), name = Name, password = Password});
                    Name = null;
                    Password = null;
                }

            }

        }
    }
}
