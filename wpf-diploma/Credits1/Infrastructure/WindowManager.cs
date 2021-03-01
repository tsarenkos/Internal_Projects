using Credits1.ViewModels;
using Credits1.Views;

namespace Credits1.Infrastructure
{
    public static class WindowManager
    {
        public static void ProcessMessage(object viewModel)
        {
            if(viewModel is CollateralWindowViewModel)
            {
                CollateralWindow wnd = new CollateralWindow();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if(viewModel is AddFirmDialogViewModel)
            {
                AddFirmDialog wnd = new AddFirmDialog();
                (viewModel as AddFirmDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is AddPersonDialogViewModel)
            {
                AddPersonDialog wnd = new AddPersonDialog();
                (viewModel as AddPersonDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is CalculationInterestDialogViewModel)
            {
                CalculationInterestDialog wnd = new CalculationInterestDialog();
                (viewModel as CalculationInterestDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is UpdateCreditDialogViewModel)
            {
                UpdateCreditDialog wnd = new UpdateCreditDialog();
                (viewModel as UpdateCreditDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is AddCreditDialogViewModel)
            {
                AddCreditDialog wnd = new AddCreditDialog();
                (viewModel as AddCreditDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is CollateralDialogViewModel)
            {
                CollateralDialog wnd = new CollateralDialog();
                (viewModel as CollateralDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }            
            else if (viewModel is MonitoringWindowViewModel)
            {
                MonitoringWindow wnd = new MonitoringWindow();                
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is UpdateMonitoringDialogViewModel)
            {
                UpdateMonitoringDialog wnd = new UpdateMonitoringDialog();
                (viewModel as UpdateMonitoringDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
            else if (viewModel is PasswordWindowViewModel)
            {
                PasswordWindow wnd = new PasswordWindow();
                (viewModel as PasswordWindowViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;                
                wnd.ShowDialog();
            }
            else if (viewModel is MainWindowViewModelcs)
            {
                MainWindow wnd = new MainWindow();                
                wnd.DataContext = viewModel;
                wnd.Closed += (s, e) => App.Current.Shutdown();
                wnd.ShowDialog();
            }
            else if (viewModel is AddPropertyDialogViewModel)
            {
                AddPropertyDialog wnd = new AddPropertyDialog();
                (viewModel as AddPropertyDialogViewModel).CloseWnd += (s, e) => wnd.Close();
                wnd.DataContext = viewModel;
                wnd.ShowDialog();
            }
        }       
    }
}
