using System;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.Windows.Controls;
using Credits1.Models.UserClasses;
using System.Windows;

namespace Credits1.ViewModels
{
    public class PasswordWindowViewModel : ViewModelBase
    {
        public event EventHandler CloseWnd;

        string userName;
        string password;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private ICommand sign;
        public ICommand Sign
        {
            get
            {
                if (sign == null)
                    sign = new RelayCommand(ExecuteSign);
                return sign;
            }
        }
        private void ExecuteSign(object obj)
        {
            var passwordBox = obj as PasswordBox;
            if ((passwordBox != null) && (UserName != null))
            {
                Password = passwordBox.Password;
                Authorization user = new Authorization(UserName, Password);
                if (user.Authorize())
                {
                    CloseWnd(this, new EventArgs());
                    MainWindowViewModelcs viewModel = new MainWindowViewModelcs(UserName);
                    WindowManager.ProcessMessage(viewModel as object);
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
            }
        }
        public ICommand ResultCancel
        {
            get
            {
                return new RelayCommand(ExecuteResultCancel);
            }
        }
        public void ExecuteResultCancel(object obj)
        {
            CloseWnd(this, new EventArgs());
            App.Current.Shutdown();            
        }
    }
}
