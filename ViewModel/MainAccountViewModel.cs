using System;
using Account_Project.Model;
using System.Windows.Input;
using System.Windows;

namespace Account_Project.ViewModel
{
    public class MainAccountViewModel : ViewModelBase
    {
        private User mainUser = null;

        private Window mainWindow = null;

        private bool editAccountVisible = false;

        private bool mainAccountVisible = true;

        public bool EditAccountVisible
        {
            get { return editAccountVisible; }
            set
            {
                if (editAccountVisible != value)
                {
                    editAccountVisible = value;
                    // OnPropertyChanged();
                }
            }
        }

        public bool MainAccountVisible
        {
            get { return mainAccountVisible; }
            set
            {
                if (mainAccountVisible != value)
                {
                    mainAccountVisible = value;
                    // OnPropertyChanged();
                }
            }
        }


        //Поля
        private string name;
        private string description;
        private DateTime birthday;
        public byte[] imageData;

        //Cвойства
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    // OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    // OnPropertyChanged();
                }
            }
        }
        public string Birthday
        {
            get { return birthday.ToString("yyyy-MM-dd"); }
            set
            {
                if (birthday.ToString("yyyy-MM-dd") != value)
                {
                    birthday = DateTime.Parse(value);
                }
            }

        }
        public byte[] ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;
                    
                }
            }
        }

        public MainAccountViewModel(User user, Window mainWindow)
        {
            this.mainUser = user;
            this.mainWindow = mainWindow;

            if (user != null)
            {
                if (user.name != null)
                    Name = user.name;
                if (user.description != null)
                    Description = user.description;
                if (user.birthday != null)
                    Birthday = user.birthday.ToString("yyyy-MM-dd");
                if (user.Image != null)
                    ImageData = user.Image;
            }

            EditAccount = new RelayCommand(EditAccountButton);
        }


        public ICommand EditAccount { get; set; }

        public void EditAccountButton()
        {
            MainAccountVisible = false;
            EditAccountVisible = true;
            mainWindow.DataContext = new RedactionAccountViewModel(mainWindow, mainUser, this);
        }
    }
}
