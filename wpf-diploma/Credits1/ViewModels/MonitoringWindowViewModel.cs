using System;
using System.Collections.Generic;
using System.Linq;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Credits1.Infrastructure;
using Credits1.Models.UserClasses;

namespace Credits1.ViewModels
{
    class MonitoringWindowViewModel : ViewModelBase
    {
        private ObservableCollection<MonitoringInfo> monitoringCollection;
        public ObservableCollection<MonitoringInfo> MonitoringCollection
        {
            get { return monitoringCollection; }
            set
            {
                monitoringCollection = value;
                OnPropertyChanged("MonitoringCollection");
            }
        }
        bool filtrEmp;
        bool filtrTime;
        private MonitoringInfo selectedMonitoring;
        public MonitoringInfo SelectedMonitoring
        {
            get { return selectedMonitoring; }
            set
            {
                selectedMonitoring = value;
                OnPropertyChanged("SelectedMonitoring");
            }
        }
        private List<string> monitoringEmployee;
        public List<string> MonitoringEmployee
        {
            get
            {
                using(CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var empRequest = context.Employees.Where(e=>e.Department_Id==104);
                    List<string> emp = new List<string>();

                    foreach (var item in empRequest)
                    {
                        emp.Add(item.Name);
                    }
                    emp.Add("Все");
                    return emp;
                }
            }
            set
            {
                monitoringEmployee = value;
                OnPropertyChanged("MonitoringEmployee");
            }

        }
        private string filtrEmployee;
        public string FiltrEmployee
        {
            get { return filtrEmployee; }
            set
            {
                filtrEmployee = value;
                OnPropertyChanged("FiltrEmployee");
            }
        }
        private List<string> monitoringPeriod;
        public List<string> MonitoringPeriod
        {
            get
            {
                return new List<string> { "Текущие", "Просроченные", "Все" };

            }
            set
            {
                monitoringPeriod = value;
                OnPropertyChanged("MonitoringPeriod");
            }

        }
        private string filtrPeriod;
        public string FiltrPeriod
        {
            get { return filtrPeriod; }
            set
            {
                filtrPeriod = value;
                OnPropertyChanged("FiltrPeriod");
            }
        }
        private string searchMonitoring;
        public string SearchMonitoring
        {
            get { return searchMonitoring; }
            set
            {
                searchMonitoring = value;
                OnPropertyChanged("SearchMonitoring");
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
                SearchMonitoring = null;
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
        public MonitoringWindowViewModel(string userName)
        {
            UserName = userName;
            filtrEmp = false;
            filtrTime = false;
            FiltrEmployee = "Все";
            FiltrPeriod = "Все";
            LoadCollateral();
        }        
        public void LoadCollateral()
        {
            ObservableCollection<MonitoringInfo> monitoringCollectionInfo = new ObservableCollection<MonitoringInfo>();

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                var collateralsLoad = context.Collaterals;

                foreach (Collateral collateral in collateralsLoad)
                {                    
                    if(collateral.TypeId!=99559)
                    {
                        MonitoringInfo monitoringInfo = new MonitoringInfo();
                        monitoringInfo.Collateral_agreement = collateral.Collateral_agreement;                        
                        monitoringInfo.TypeId = collateral.TypeId;
                        monitoringInfo.Description = collateral.Description;
                        monitoringInfo.Previous_date = collateral.Monitoring_collateral.Previous_date;
                        monitoringInfo.Planned_date = collateral.Monitoring_collateral.Planned_date;
                        monitoringInfo.Note = collateral.Monitoring_collateral.Note;
                        monitoringInfo.EmployeeSupport = collateral.Monitoring_collateral.Employees.FirstOrDefault(e => e.Department_Id == 103)?.Name;
                        monitoringInfo.EmployeeMonitoring = collateral.Monitoring_collateral.Employees.FirstOrDefault(e => e.Department_Id == 104)?.Name;

                        if (collateral.Firm != null)
                        {
                            monitoringInfo.ClientName = collateral.Firm.Name;
                        }
                        else
                        {
                            monitoringInfo.ClientName = collateral.Individual.Name;
                        }

                        monitoringCollectionInfo.Add(monitoringInfo);
                    }
                    
                }

                MonitoringCollection = monitoringCollectionInfo;
            }

        }
        private ICommand loadMonitoringCommand;
        public ICommand LoadMonitoringCommand
        {
            get
            {
                if (loadMonitoringCommand == null)
                    loadMonitoringCommand = new RelayCommand(ExecuteLoadMonitoringCommand);
                return loadMonitoringCommand;
            }
        }
        private void ExecuteLoadMonitoringCommand(object obj)
        {
            LoadCollateral();
            SearchClient = null;
            FiltrEmployee = "Все";
            FiltrPeriod = "Все";
        }
        private ICommand monitoringEdit;
        public ICommand MonitoringEdit
        {
            get
            {
                if (monitoringEdit == null)
                    monitoringEdit = new RelayCommand(ExecuteMonitoringEdit, CanExecuteMonitoringEdit);
                return monitoringEdit;
            }
        }
        private bool CanExecuteMonitoringEdit(object obj)
        {
            if (SelectedMonitoring != null)
                return true;
            else return false;
        }
        private void ExecuteMonitoringEdit(object obj)
        {
            Employee empSupport;
            Employee empMonitoring;

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                empSupport = context.Employees.FirstOrDefault(e => e.Name == SelectedMonitoring.EmployeeSupport);
                empMonitoring = context.Employees.FirstOrDefault(e => e.Name == SelectedMonitoring.EmployeeMonitoring);
            }
                UpdateMonitoringDialogViewModel viewModel = new UpdateMonitoringDialogViewModel(SelectedMonitoring.Collateral_agreement,
                    SelectedMonitoring.ClientName, SelectedMonitoring.Description, SelectedMonitoring.Previous_date, SelectedMonitoring.Planned_date,
                    SelectedMonitoring.Note, empSupport, empMonitoring);

            WindowManager.ProcessMessage(viewModel as object);
        }
        private ICommand searchMonitoringCommand;
        public ICommand SearchMonitoringCommand
        {
            get
            {
                if (searchMonitoringCommand == null)
                    searchMonitoringCommand = new RelayCommand(ExecuteSearchMonitoring);
                return searchMonitoringCommand;
            }
        }
        private void ExecuteSearchMonitoring(object obj)
        {
            ObservableCollection<MonitoringInfo> monitoringCollectionInfo = new ObservableCollection<MonitoringInfo>();
            ObservableCollection<Collateral> collateralsLoad = new ObservableCollection<Collateral>();            

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                if (!string.IsNullOrWhiteSpace(SearchMonitoring))
                {                   
                    var collaterals = from c in context.Collaterals
                                      where c.Collateral_agreement.StartsWith(SearchMonitoring)
                                      select c;
                    foreach (Collateral collateral in collaterals)
                    {
                        collateralsLoad.Add(collateral);
                    }
                }

                else if ((!string.IsNullOrWhiteSpace(SearchClient)))
                {
                    var collaterals = from c in context.Collaterals
                                      where c.Firm.Name.Contains(SearchClient) || c.Individual.Name.Contains(SearchClient)
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
                        if (collateral.TypeId != 99559)
                        {
                            MonitoringInfo monitoringInfo = new MonitoringInfo();
                            monitoringInfo.Collateral_agreement = collateral.Collateral_agreement;
                            monitoringInfo.TypeId = collateral.TypeId;
                            monitoringInfo.Description = collateral.Description;
                            monitoringInfo.Previous_date = collateral.Monitoring_collateral.Previous_date;
                            monitoringInfo.Planned_date = collateral.Monitoring_collateral.Planned_date;
                            monitoringInfo.Note = collateral.Monitoring_collateral.Note;
                            monitoringInfo.EmployeeSupport = collateral.Monitoring_collateral.Employees.FirstOrDefault(e => e.Department_Id == 103)?.Name;
                            monitoringInfo.EmployeeMonitoring = collateral.Monitoring_collateral.Employees.FirstOrDefault(e => e.Department_Id == 104)?.Name;

                            if (collateral.Firm != null)
                            {
                                monitoringInfo.ClientName = collateral.Firm.Name;
                            }
                            else
                            {
                                monitoringInfo.ClientName = collateral.Individual.Name;
                            }

                            monitoringCollectionInfo.Add(monitoringInfo);
                        }                        
                    }
                    MonitoringCollection = monitoringCollectionInfo;
                }
            }
        }
        private ICommand filtrMonitoringEmployee;
        public ICommand FiltrMonitoringEmployee
        {
            get
            {
                if (filtrMonitoringEmployee == null)
                    filtrMonitoringEmployee = new RelayCommand(ExecuteFiltrEmployee);
                return filtrMonitoringEmployee;
            }
        }
        private void ExecuteFiltrEmployee(object obj)
        {
            ObservableCollection<MonitoringInfo> filtrMonitoring = new ObservableCollection<MonitoringInfo>();            
            if(FiltrEmployee=="Все")
            {
                filtrEmp = false;
                LoadCollateral();
                if(filtrTime==true)
                {
                    foreach (MonitoringInfo item in MonitoringCollection)
                    {
                        if (FiltrPeriod == "Просроченные")
                        {
                            if (item.Planned_date < DateTime.Now)
                                filtrMonitoring.Add(item);
                        }
                        else if (FiltrPeriod == "Текущие")
                        {
                            if (item.Planned_date >= DateTime.Now)
                                filtrMonitoring.Add(item);
                        }
                        MonitoringCollection = filtrMonitoring;
                    }
                }
            }
            else
            {
                if (filtrEmp == true)
                    LoadCollateral();
                else
                {
                    LoadCollateral();
                    filtrEmp = true;
                }
                if (filtrTime == true)
                {
                    if (FiltrPeriod == "Просроченные")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if ((item.EmployeeMonitoring == FiltrEmployee) && (item.Planned_date < DateTime.Now))
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                    else if (FiltrPeriod == "Текущие")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if ((item.EmployeeMonitoring == FiltrEmployee) && (item.Planned_date >= DateTime.Now))
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                }
                else if (filtrTime == false)
                {
                    foreach (MonitoringInfo item in MonitoringCollection)
                    {
                        if (item.EmployeeMonitoring == FiltrEmployee)
                            filtrMonitoring.Add(item);
                        MonitoringCollection = filtrMonitoring;
                    }
                }
            }           
        }
        private ICommand filtrMonitoringPeriod;
        public ICommand FiltrMonitoringPeriod
        {
            get
            {
                if (filtrMonitoringPeriod == null)
                    filtrMonitoringPeriod = new RelayCommand(ExecuteFiltrPeriod);
                return filtrMonitoringPeriod;
            }
        }
        private void ExecuteFiltrPeriod(object obj)
        {            
            ObservableCollection<MonitoringInfo> filtrMonitoring = new ObservableCollection<MonitoringInfo>();
            if (FiltrPeriod == "Все")
            {
                filtrTime = false;
                LoadCollateral();
                if (filtrEmp == true)
                {
                    foreach (MonitoringInfo item in MonitoringCollection)
                    {
                        if (item.EmployeeMonitoring == FiltrEmployee)
                            filtrMonitoring.Add(item);
                        MonitoringCollection = filtrMonitoring;
                    }
                }
            }
            else
            {
                if (filtrTime == true)
                    LoadCollateral();
                else
                {
                    LoadCollateral();
                    filtrTime = true;
                }
                if (filtrEmp == true)
                {
                    if (FiltrPeriod == "Просроченные")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if ((item.EmployeeMonitoring == FiltrEmployee) && (item.Planned_date < DateTime.Now))
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                    else if (FiltrPeriod == "Текущие")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if ((item.EmployeeMonitoring == FiltrEmployee) && (item.Planned_date >= DateTime.Now))
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                }
                else if (filtrEmp == false)
                {
                    if (FiltrPeriod == "Просроченные")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if (item.Planned_date < DateTime.Now)
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                    else if (FiltrPeriod == "Текущие")
                    {
                        foreach (MonitoringInfo item in MonitoringCollection)
                        {
                            if (item.Planned_date >= DateTime.Now)
                                filtrMonitoring.Add(item);
                            MonitoringCollection = filtrMonitoring;
                        }
                    }
                }
            }
        }                   
    }
}
