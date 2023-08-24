using System;
using System.Windows.Input;
using System.Windows;
using Account_Project.Model;
using Microsoft.Win32;
using System.IO;

namespace Account_Project.ViewModel
{
    public class RedactionAccountViewModel : ViewModelBase
    {
        private User user = null;

        private Window mainWindow = null;
        private MainAccountViewModel main = null;


        private string errorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {

                birthday = value;
                //OnPropertyChanged();


            }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    //OnPropertyChanged();
                }
            }
        }


        private string discription;
        public string Description
        {
            get { return discription; }
            set
            {
                if (discription != value)
                {
                    discription = value;
                    //OnPropertyChanged();
                }
            }
        }

        public byte[] imageData;
        public byte[] ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;


                    OnPropertyChanged();

                }
            }
        }

        public byte[] ImageDataFirst
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


        public ICommand saveCommand { get; set; }

        public ICommand discardCommand { get; set; }

        public ICommand newPhoto { get; set; }

        public RedactionAccountViewModel(Window mainWindow, User user, MainAccountViewModel main)
        {
            this.mainWindow = mainWindow;
            this.user = user;
            this.main = main;

            if (user.name != null)
                Name = user.name;
            if (user.description != null)
                Description = user.description;
            if (user.birthday != null)
                Birthday = user.birthday;
            if (user.Image != null)
                ImageDataFirst = user.Image;


            saveCommand = new RelayCommand(SaveChangeButton);
            discardCommand = new RelayCommand(DiscardButton);
            newPhoto = new RelayCommand(NewPhotoButton);
        }


        public void SaveChangeButton()
        {
            bool nameTaken = MsSqlDataProvider.NameIsTaken(Name);


            if (nameTaken == true && user.name != Name)
            {
                ErrorMessage = "Имя занято!";
            }
            else if (MessageBoxResult.Yes == MessageBox.Show("Сохранить?", "", MessageBoxButton.YesNo))
            {
                if (ImageData != null)
                {
                    if (ImageData != user.Image)
                    {
                        user.Image = ImageData;
                        MsSqlDataProvider.AddImage(ImageData, user);
                    }
                }

                if (!string.IsNullOrWhiteSpace(Description))
                {
                    user.description = Description;
                    MsSqlDataProvider.AddDescription(Description, user);
                }

                if (!string.IsNullOrWhiteSpace(Name))
                {

                    if (user.name != Name)
                    {
                        user.name = Name;
                        MsSqlDataProvider.AddName(Name, user);
                    }

                }


                user.birthday = Birthday;
                MsSqlDataProvider.AddDate(Birthday, user);



                main.EditAccountVisible = false;
                main.MainAccountVisible = true;
                mainWindow.DataContext = new MainAccountViewModel(user, mainWindow);

            }

        }

        public void DiscardButton()
        {
            MessageBoxResult res = MessageBox.Show("Отменить?", null, MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                main.EditAccountVisible = false;
                main.MainAccountVisible = true;
                mainWindow.DataContext = main;
            }

        }

        private void NewPhotoButton()
        {
            OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "Фото(*.jpg; *.jpeg; *.png)| *.jpg; *.jpeg; *.png" };

            if (fileDialog.ShowDialog() == true)
            {
                // BitmapImage newBitmapPhoto = new BitmapImage(new Uri(fileDialog.FileName));

                ImageData = File.ReadAllBytes(fileDialog.FileName);
            }

        }


    }
}
