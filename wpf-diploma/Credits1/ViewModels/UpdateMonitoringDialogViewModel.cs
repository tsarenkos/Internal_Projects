using System;
using System.Linq;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;
using Credits1.Models;
using System.Collections.ObjectModel;


namespace Credits1.ViewModels
{
    class UpdateMonitoringDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string collateralAgreement;
        private string clientName;        
        private string description;
        public DateTime previous_date;
        public DateTime planned_date;
        public string note;
        public Employee employeeSupport;
        public Employee employeeMonitoring;
        public ObservableCollection<Employee> employeesSupport;
        public ObservableCollection<Employee> employeesMonitoring;

        public UpdateMonitoringDialogViewModel(string collateral_agreement, string clientName, string description,
            DateTime previous_date, DateTime planned_date, string note, Employee employeeSupport, Employee employeeMonitoring)
        {
            this.collateralAgreement = collateral_agreement;
            this.clientName = clientName;
            this.description = description;
            this.previous_date = previous_date;
            this.planned_date = planned_date;
            this.note = note;
            this.employeeSupport = employeeSupport;
            this.employeeMonitoring = employeeMonitoring;           
        }
        public string CollateralAgreement
        {
            get { return collateralAgreement; }
            set
            {
                collateralAgreement = value;
                OnPropertyChanged("CollateralAgreement");
            }
        }
        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = value;
                OnPropertyChanged("ClientName");
            }
        }
        public DateTime Previous_date
        {
            get { return previous_date; }
            set
            {
                previous_date = (DateTime)value;
                OnPropertyChanged("Previous_date");
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Collateral collateral = context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == CollateralAgreement);
                    if (collateral.TypeCollateral.TypeId == 99551 || collateral.TypeCollateral.TypeId == 99552)
                        Planned_date = Previous_date.AddYears(1);
                    else if (collateral.TypeCollateral.TypeId == 99553)
                        Planned_date = Previous_date.AddMonths(3);
                }                   
            }
        }
        public DateTime Planned_date
        {
            get { return planned_date; }
            set
            {
                planned_date = (DateTime)value;
                OnPropertyChanged("Planned_date");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }
        public Employee EmployeeSupport
        {
            get { return employeeSupport; }
            set
            {
                employeeSupport = value;
                OnPropertyChanged("EmployeeSupport");
            }
        }
        public Employee EmployeeMonitoring
        {
            get { return employeeMonitoring; }
            set
            {
                employeeMonitoring = value;
                OnPropertyChanged("EmployeeMonitoring");
            }
        }
        public ObservableCollection<Employee> EmployeesSupport
        {
            get
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var employees = context.Employees.Where(e=>e.Department_Id==103);
                    ObservableCollection<Employee> emp = new ObservableCollection<Employee>();

                    foreach (var item in employees)
                    {
                        emp.Add(item);
                    }
                    return emp;
                }
            }
            
            set
            {
                employeesSupport = value;
                OnPropertyChanged("EmployeesSupport");
            }
        }
        public ObservableCollection<Employee> EmployeesMonitoring
        {
            get
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var employees = context.Employees.Where(e => e.Department_Id == 104);
                    ObservableCollection<Employee> emp = new ObservableCollection<Employee>();

                    foreach (var item in employees)
                    {
                        emp.Add(item);
                    }
                    return emp;
                }
            }
            set
            {
                employeesMonitoring = value;
                OnPropertyChanged("EmployeesMonitoring");
            }
        }
        public ICommand SaveMonitoring
        {
            get
            {
                return new RelayCommand(ExecuteSaveMonitoring);
            }
        }
        public ICommand ResultCancel
        {
            get
            {
                return new RelayCommand(ExecuteResultCancel);
            }
        }
        public void ExecuteSaveMonitoring(object obj)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Collateral collateral = context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == CollateralAgreement);
                Monitoring_collateral monitoringUpdate = context.Monitoring_collateral.FirstOrDefault(m => m.Collateral_agreement == CollateralAgreement);
                monitoringUpdate.Collateral_agreement = CollateralAgreement;
                monitoringUpdate.Note = Note;
                monitoringUpdate.Previous_date = Previous_date;
                monitoringUpdate.Planned_date = Planned_date;                                           

                monitoringUpdate.Employees.Clear();

                if(EmployeeMonitoring!=null)
                {                    
                    Employee emp1 = context.Employees.First(e => e.Employee_Id == EmployeeMonitoring.Employee_Id);
                    emp1.Monitoring_collateral.Add(monitoringUpdate);
                }
                if (EmployeeSupport != null)
                {
                    Employee emp = context.Employees.First(e => e.Employee_Id == EmployeeSupport.Employee_Id);
                    emp.Monitoring_collateral.Add(monitoringUpdate);                    
                }
                context.SaveChanges();
                CloseWnd(this, new EventArgs());
            }
        }
        public void ExecuteResultCancel(object obj)
        {
            CloseWnd(this, new EventArgs());
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "CollateralAgreement":
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
