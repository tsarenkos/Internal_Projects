using System.Windows;
using Credits1.Infrastructure;
using Credits1.ViewModels;

namespace Credits1
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {          
            base.OnStartup(e);
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            PasswordWindowViewModel viewModel = new PasswordWindowViewModel();
            WindowManager.ProcessMessage(viewModel as object);           
        }
    }
}
