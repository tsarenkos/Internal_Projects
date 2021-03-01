using System.Linq;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Credits1.Infrastructure;
using Credits1.Models.UserClasses;

namespace Credits1.ViewModels
{
    public class MainWindowViewModelcs : ViewModelBase
    {
        private ObservableCollection<CreditInfo> credits;
        public ObservableCollection<CreditInfo> Credits
        {
            get { return credits; }
            set
            {
                credits = value;
                OnPropertyChanged("Credits");
            }
        }
        private CreditInfo selectedCredit;
        public CreditInfo SelectedCredit
        {
            get { return selectedCredit; }
            set
            {
                selectedCredit = value;
                OnPropertyChanged("SelectedCredit");
            }
        }
        private string searchCredit;
        public string SearchCredit
        {
            get { return searchCredit; }
            set
            {
                searchCredit = value;
                OnPropertyChanged("SearchCredit");                
            }
        }
        private string searchFirm;
        public string SearchFirm
        {
            get { return searchFirm; }
            set
            {
                searchFirm = value;                
                OnPropertyChanged("SearchFirm");
                SearchCredit = null;
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");                
            }
        }
        public MainWindowViewModelcs(string userName)
        {
            UserName = userName;
            LoadCredits();
        }
        private ICommand loadCreditsBase;
        public ICommand LoadCreditsBase
        {
            get
            {
                if (loadCreditsBase == null)
                    loadCreditsBase = new RelayCommand(ExecuteLoadCredits);
                return loadCreditsBase;
            }
        }
        private void ExecuteLoadCredits(object obj)
        {
            LoadCredits();
            SearchFirm = null;
        }
        public void LoadCredits()
        {
            ObservableCollection<CreditInfo> creditsInfo = new ObservableCollection<CreditInfo>();            

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                var creditsLoad = context.Credits;

                foreach (Credit credit in creditsLoad)
                {
                    CreditInfo creditInfo = new CreditInfo();
                    creditInfo.Credit_agreement = credit.Credit_agreement;
                    creditInfo.Start_date = credit.Start_date;
                    creditInfo.End_date = credit.End_date;
                    creditInfo.Currency = credit.Currencies_directory;
                    creditInfo.Sum = credit.Sum;
                    creditInfo.Id_Firm = credit.Id_Firm;
                    creditInfo.Rate = credit.Interest_rate.OrderByDescending(d => d.Date).FirstOrDefault().Rate;
                    creditInfo.FirmName = credit.Firm.Name;    
                    creditsInfo.Add(creditInfo);
                }
                Credits = creditsInfo;
            }                
        }
        private ICommand addFirm;
        public ICommand AddFirm
        {
            get
            {
                if (addFirm == null)
                    addFirm = new RelayCommand(ExecuteAddFirm);
                return addFirm;
            }
        }
        public void ExecuteAddFirm(object obj)
        {
            AddFirmDialogViewModel viewModel = new AddFirmDialogViewModel();
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand addPerson;
        public ICommand AddPerson
        {
            get
            {
                if (addPerson == null)
                    addPerson = new RelayCommand(ExecuteAddPerson);
                return addPerson;
            }
        }
        public void ExecuteAddPerson(object obj)
        {
            AddPersonDialogViewModel viewModel = new AddPersonDialogViewModel();
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand addCredit;
        public ICommand AddCredit
        {
            get
            {
                if (addCredit == null)
                    addCredit = new RelayCommand(ExecuteAddCredit);
                return addCredit;
            }
        }
        public void ExecuteAddCredit(object obj)
        {
            AddCreditDialogViewModel viewModel = new AddCreditDialogViewModel();
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand collateralWindow;
        public ICommand CollateralWindow
        {
            get
            {
                if (collateralWindow == null)
                    collateralWindow = new RelayCommand(ExecuteCollateralWindow);
                return collateralWindow;
            }
        }
        public void ExecuteCollateralWindow(object obj)
        {
            CollateralWindowViewModel viewModel = new CollateralWindowViewModel(UserName);
            WindowManager.ProcessMessage(viewModel);
        }
        private ICommand monitoringCollateral;
        public ICommand MonitoringCollateral
        {
            get
            {
                if (monitoringCollateral == null)
                    monitoringCollateral = new RelayCommand(ExecuteMonitoringCollateral);
                return monitoringCollateral;
            }
        }
        private void ExecuteMonitoringCollateral(object obj)
        {
            MonitoringWindowViewModel viewModel = new MonitoringWindowViewModel(UserName);
            WindowManager.ProcessMessage(viewModel as object);
        }        
        private ICommand addCollateral;
        public ICommand AddCollateral
        {
            get
            {
                if (addCollateral == null)
                    addCollateral = new RelayCommand(ExecuteAddCollateral, CanExecuteAddCollateral);
                return addCollateral;
            }
        }
        private bool CanExecuteAddCollateral(object obj)
        {
            if (SelectedCredit != null)
                return true;
            else return false;
        }
        public void ExecuteAddCollateral(object obj)
        {
            CollateralDialogViewModel viewModel = new CollateralDialogViewModel(SelectedCredit.Credit_agreement);
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand creditEdit;
        public ICommand CreditEdit
        {
            get
            {
                if (creditEdit == null)
                    creditEdit = new RelayCommand(ExecuteCreditEdit, CanExecuteCreditEdit);
                return creditEdit;
            }
        }
        private bool CanExecuteCreditEdit(object obj)
        {
            if (SelectedCredit != null)
                return true;
            else return false;
        }
        private void ExecuteCreditEdit(object obj)
        {
            UpdateCreditDialogViewModel viewModel = new UpdateCreditDialogViewModel(SelectedCredit.Credit_agreement, SelectedCredit.Start_date,
                SelectedCredit.End_date, SelectedCredit.Currency, SelectedCredit.Sum, SelectedCredit.Id_Firm, SelectedCredit.Rate, UserName);
            WindowManager.ProcessMessage(viewModel as object);            
        }
        private ICommand calcCommand;
        public ICommand CalcCommand
        {
            get
            {
                if (calcCommand == null)
                    calcCommand = new RelayCommand(ExecuteCalcCommand, CanExecuteCalcCommand);
                return calcCommand;
            }
        }
        private bool CanExecuteCalcCommand(object obj)
        {
            if (SelectedCredit != null)
                return true;
            else return false;
        }
        private void ExecuteCalcCommand(object obj)
        {
            CalculationInterestDialogViewModel viewModel = new CalculationInterestDialogViewModel(SelectedCredit.Credit_agreement);
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand searchCreditCommand;
        public ICommand SearchCreditCommand
        {
            get
            {
                if (searchCreditCommand == null)
                    searchCreditCommand = new RelayCommand(ExecuteSearchCredit);
                return searchCreditCommand;
            }
        }
        private void ExecuteSearchCredit(object obj)
        {
            ObservableCollection<CreditInfo> creditsInfo;
            ObservableCollection<Credit> creditsLoad = new ObservableCollection<Credit>();

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                if (!string.IsNullOrWhiteSpace(SearchCredit))
                {
                    var credits = from c in context.Credits
                                  where c.Credit_agreement.StartsWith(SearchCredit)
                                  select c;
                    foreach(Credit credit in credits)
                    {
                        creditsLoad.Add(credit);
                    }                    
                }                
                else if ((!string.IsNullOrWhiteSpace(SearchFirm)))
                {
                    var credits = from c in context.Credits
                                  where c.Firm.Name.Contains(SearchFirm)
                                  select c;
                    foreach (Credit credit in credits)
                    {
                        creditsLoad.Add(credit);
                    }                    
                }                 
                if(creditsLoad!=null)
                {
                    creditsInfo = new ObservableCollection<CreditInfo>();

                    foreach (Credit credit in creditsLoad)
                    {
                        CreditInfo creditInfo = new CreditInfo();
                        creditInfo.Credit_agreement = credit.Credit_agreement;
                        creditInfo.Start_date = credit.Start_date;
                        creditInfo.End_date = credit.End_date;
                        creditInfo.Currency = credit.Currencies_directory;
                        creditInfo.Sum = credit.Sum;
                        creditInfo.Id_Firm = credit.Id_Firm;
                        creditInfo.Rate = credit.Interest_rate.OrderByDescending(d => d.Date).FirstOrDefault().Rate;
                        creditInfo.FirmName = credit.Firm.Name;

                        creditsInfo.Add(creditInfo);
                    }
                    Credits = creditsInfo;
                }                
            }
        }
    }
}
