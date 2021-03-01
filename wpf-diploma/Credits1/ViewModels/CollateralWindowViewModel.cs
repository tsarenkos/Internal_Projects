using System.Collections.Generic;
using System.Linq;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Credits1.Infrastructure;
using Credits1.Models.UserClasses;
using System.Xml.Linq;

namespace Credits1.ViewModels
{
    public class CollateralWindowViewModel : ViewModelBase
    {
        private ObservableCollection<CollateralInfo> collaterals;
        public ObservableCollection<CollateralInfo> Collaterals
        {
            get { return collaterals; }
            set
            {
                collaterals = value;
                OnPropertyChanged("Collaterals");
            }
        }
        private CollateralInfo selectedCollateral;
        public CollateralInfo SelectedCollateral
        {
            get { return selectedCollateral; }
            set
            {
                selectedCollateral = value;
                OnPropertyChanged("SelectedCollateral");
            }
        }
        private string searchCollateral;
        public string SearchCollateral
        {
            get { return searchCollateral; }
            set
            {
                searchCollateral = value;
                OnPropertyChanged("SearchCollateral");
            }
        }
        private string searchClient;
        public string SearchClient
        {
            get { return searchClient; }
            set
            {
                searchClient = value;
                OnPropertyChanged("SearchClient");
                SearchCollateral = null;
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
        public CollateralWindowViewModel(string userName)
        {
            UserName = userName;
            LoadCollateral();            
        }
        public CollateralWindowViewModel(string creditAgreement, string userName)
        {
            UserName = userName;
            LoadCollateral(creditAgreement);            
        }
        public void LoadCollateral()
        {
            ObservableCollection<CollateralInfo> collateralsInfo = new ObservableCollection<CollateralInfo>();

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                var collateralsLoad = context.Collaterals;

                foreach (Collateral collateral in collateralsLoad)
                {
                    CollateralInfo collateralInfo = new CollateralInfo();
                    collateralInfo.Collateral_agreement = collateral.Collateral_agreement;
                    collateralInfo.Start_date = collateral.Start_date;
                    collateralInfo.End_date = collateral.End_date;
                    collateralInfo.Credit_agreement = collateral.Credit_agreement;
                    collateralInfo.Currency = collateral.Currencies_directory;
                    collateralInfo.Sum = collateral.Sum;
                    collateralInfo.FormId = collateral.FormId;
                    collateralInfo.TypeId = collateral.TypeId;
                    collateralInfo.Description = collateral.Description;

                    if(collateral.Firm!=null)
                    {
                        collateralInfo.ClientId = collateral.Id_Firm;
                        collateralInfo.ClientName = collateral.Firm.Name;
                    }
                    else
                    {
                        collateralInfo.ClientId = collateral.Id_Person;
                        collateralInfo.ClientName = collateral.Individual.Name;
                    }                    
                    collateralsInfo.Add(collateralInfo);
                }                

                Collaterals = collateralsInfo;
            }
        }
        public void LoadCollateral(string creditAgreement)
        {
            ObservableCollection<CollateralInfo> collateralsInfo = new ObservableCollection<CollateralInfo>();

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {

                var collateralsLoad = context.Collaterals.Where(c => c.Credit_agreement == creditAgreement);

                foreach (Collateral collateral in collateralsLoad)
                {
                    CollateralInfo collateralInfo = new CollateralInfo();
                    collateralInfo.Collateral_agreement = collateral.Collateral_agreement;
                    collateralInfo.Start_date = collateral.Start_date;
                    collateralInfo.End_date = collateral.End_date;
                    collateralInfo.Credit_agreement = collateral.Credit_agreement;
                    collateralInfo.Currency = collateral.Currencies_directory;
                    collateralInfo.Sum = collateral.Sum;
                    collateralInfo.FormId = collateral.FormId;
                    collateralInfo.TypeId = collateral.TypeId;
                    collateralInfo.Description = collateral.Description;

                    if (collateral.Firm != null)
                    {
                        collateralInfo.ClientId = collateral.Id_Firm;
                        collateralInfo.ClientName = collateral.Firm.Name;
                    }
                    else
                    {
                        collateralInfo.ClientId = collateral.Id_Person;
                        collateralInfo.ClientName = collateral.Individual.Name;
                    }
                    collateralsInfo.Add(collateralInfo);
                }

                Collaterals = collateralsInfo;
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
        private ICommand loadCollateralCommand;
        public ICommand LoadCollateralCommand
        {
            get
            {
                if (loadCollateralCommand == null)
                    loadCollateralCommand = new RelayCommand(ExecuteLoadCollateralCommand);
                return loadCollateralCommand;
            }
        }
        private void ExecuteLoadCollateralCommand(object obj)
        {
            LoadCollateral();
            SearchClient = null;
        }
        private ICommand addProperty;
        public ICommand AddProperty
        {
            get
            {
                if (addProperty == null)
                    addProperty = new RelayCommand(ExecuteAddProperty, CanExecuteAddProperty);
                return addProperty;
            }
        }
        private bool CanExecuteAddProperty(object obj)
        {
            if (SelectedCollateral != null)
                return true;
            else return false;
        }
        public void ExecuteAddProperty(object obj)
        {
            AddPropertyDialogViewModel viewModel = new AddPropertyDialogViewModel(SelectedCollateral.Collateral_agreement, SelectedCollateral.ClientName);
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand collateralEdit;
        public ICommand CollateralEdit
        {
            get
            {
                if (collateralEdit == null)
                    collateralEdit = new RelayCommand(ExecuteCollateralEdit, CanExecuteCallateralEdit);
                return collateralEdit;
            }
        }
        private bool CanExecuteCallateralEdit(object obj)
        {
            if (SelectedCollateral != null)
                return true;
            else return false;
        }
        private void ExecuteCollateralEdit(object obj)
        {            
            CollateralDialogViewModel viewModel = new CollateralDialogViewModel(SelectedCollateral.Collateral_agreement, 
                SelectedCollateral.Start_date, SelectedCollateral.End_date, SelectedCollateral.Credit_agreement, SelectedCollateral.Currency, 
                SelectedCollateral.TypeId, SelectedCollateral.Sum, SelectedCollateral.Description, SelectedCollateral.FormId);
            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand collateralDelete;
        public ICommand CollateralDelete
        {
            get
            {
                if (collateralDelete == null)
                    collateralDelete = new RelayCommand(ExecuteCollateralDelete, CanExecuteCollateralDelete);
                return collateralDelete;
            }
        }
        private bool CanExecuteCollateralDelete(object obj)
        {
            if (SelectedCollateral != null)
                return true;
            else return false;
        }
        private void ExecuteCollateralDelete(object obj)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Collateral collateralDel = context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == SelectedCollateral.Collateral_agreement);
                if(collateralDel!=null)
                {
                    context.Collaterals.Remove(collateralDel);
                    context.SaveChanges();
                }

            }
        }
        private ICommand searchCollateralCommand;
        public ICommand SearchCollateralCommand
        {
            get
            {
                if (searchCollateralCommand == null)
                    searchCollateralCommand = new RelayCommand(ExecuteSearchCollateral);
                return searchCollateralCommand;
            }
        }
        private void ExecuteSearchCollateral(object obj)
        {
            ObservableCollection<CollateralInfo> collateralsInfo = new ObservableCollection<CollateralInfo>();
            ObservableCollection<Collateral> collateralsLoad = new ObservableCollection<Collateral>();

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                if (!string.IsNullOrWhiteSpace(SearchCollateral))
                {
                    var collaterals = from c in context.Collaterals
                                  where c.Collateral_agreement.StartsWith(SearchCollateral)
                                  select c;
                    foreach (Collateral collateral in collaterals)
                    {
                        collateralsLoad.Add(collateral);
                    }
                }

                else if (!string.IsNullOrWhiteSpace(SearchClient))
                {
                    var collaterals = from c in context.Collaterals
                                  where c.Firm.Name.Contains(SearchClient)||c.Individual.Name.Contains(SearchClient)
                                  select c;
                    foreach (Collateral collateral in collaterals)
                    {
                        collateralsLoad.Add(collateral);
                    }
                }                

                if (collateralsLoad != null)
                {
                    foreach (Collateral collateral in collateralsLoad)
                    {
                        CollateralInfo collateralInfo = new CollateralInfo();
                        collateralInfo.Collateral_agreement = collateral.Collateral_agreement;
                        collateralInfo.Start_date = collateral.Start_date;
                        collateralInfo.End_date = collateral.End_date;
                        collateralInfo.Credit_agreement = collateral.Credit_agreement;
                        collateralInfo.Currency = collateral.Currencies_directory;
                        collateralInfo.Sum = collateral.Sum;
                        collateralInfo.FormId = collateral.FormId;
                        collateralInfo.TypeId = collateral.TypeId;
                        collateralInfo.Description = collateral.Description;

                        if (collateral.Firm != null)
                        {
                            collateralInfo.ClientId = collateral.Id_Firm;
                            collateralInfo.ClientName = collateral.Firm.Name;
                        }
                        else
                        {
                            collateralInfo.ClientId = collateral.Id_Person;
                            collateralInfo.ClientName = collateral.Individual.Name;
                        }                        
                        collateralsInfo.Add(collateralInfo);
                    }
                    Collaterals = collateralsInfo;
                }
            }
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
        private ICommand collateralRegisterCommand;
        public ICommand CollateralRegisterCommand
        {
            get
            {
                if (collateralRegisterCommand == null)
                    collateralRegisterCommand = new RelayCommand(ExecuteCollateralRegister, CanExecuteCollateralRegister);
                return collateralRegisterCommand;
            }
        }
        private bool CanExecuteCollateralRegister(object obj)
        {
            if (SelectedCollateral != null)
                return true;
            else return false;
        }
        private void ExecuteCollateralRegister(object obj)
        {
            List<CollateralRegister> colRegList = new List<CollateralRegister>();
            if (SelectedCollateral.TypeId == 99553)
            {
                CollateralRegister colReg = new CollateralRegister();

                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    colReg.Subject = context.TypeCollaterals.FirstOrDefault(c => c.TypeId == SelectedCollateral.TypeId).Type;
                }
                colReg.Collateral_agreement = SelectedCollateral.Collateral_agreement;
                colReg.Start_date = SelectedCollateral.Start_date;
                colReg.End_date = SelectedCollateral.End_date;
                colReg.ClientName = SelectedCollateral.ClientName;
                colReg.ClientId = SelectedCollateral.ClientId;
                colReg.Currency_Id = SelectedCollateral.Currency.Currency_Id;
                colReg.Description = SelectedCollateral.Description;
                colReg.Price = SelectedCollateral.Sum;
                colRegList.Add(colReg);
            }
            else if (SelectedCollateral.TypeId == 99551)
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {                    
                    var items = context.Properties.Where(p => p.Collateral_agreement == SelectedCollateral.Collateral_agreement);
                    foreach(Property item in items)
                    {
                        CollateralRegister colReg = new CollateralRegister();
                        colReg.Subject = item.Name;
                        colReg.Id = item.Id;
                        colReg.Description = item.Description;
                        colReg.Currency_Id = item.Currency_Id;
                        colReg.Price = item.Price;
                        colReg.Collateral_agreement = SelectedCollateral.Collateral_agreement;
                        colReg.Start_date = SelectedCollateral.Start_date;
                        colReg.End_date = SelectedCollateral.End_date;
                        colReg.ClientName = SelectedCollateral.ClientName;
                        colReg.ClientId = SelectedCollateral.ClientId;
                        colRegList.Add(colReg);
                    }
                }
            }

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            var result = sfd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                XDocument doc = new XDocument();
                XElement root = new XElement("CollateralX");

                foreach (var collateral in colRegList)
                {
                    XElement collateralElement = new XElement("CollateralX",
                        new XAttribute("ClientId", collateral.ClientId),
                        new XElement("ClientName", collateral.ClientName),
                        new XElement("Collateral_agreement", collateral.Collateral_agreement),
                        new XElement("Start_date", collateral.Start_date),
                        new XElement("End_date", collateral.End_date),
                        new XElement("Subject", collateral.Subject),
                        new XElement("Id", collateral.Id),
                        new XElement("Description", collateral.Description),
                        new XElement("CurrencyId",collateral.Currency_Id),
                        new XElement("Price",collateral.Price));
                    root.Add(collateralElement);
                }
                doc.Add(root);
                doc.Save(sfd.FileName);
            }
        }
    }
}
