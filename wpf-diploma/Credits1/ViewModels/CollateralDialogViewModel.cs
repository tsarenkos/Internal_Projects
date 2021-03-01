using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Credits1.ViewModels
{
    class CollateralDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;        
        private string collateralAgreement;
        private DateTime startDate;
        private DateTime endDate;
        private ObservableCollection<Currencies_directory> currencies;
        private Currencies_directory currency;
        private string sumText;
        private string firmName;
        private string firmId;
        private string personName;
        private string personId;
        private ObservableCollection<TypeCollateral> typesCollateral;
        private TypeCollateral typeCollateral;
        private ObservableCollection<FormCollateral> formsCollateral;
        private FormCollateral formCollateral;
        private string description;
        private string creditAgreement;
        private bool valClient;        
        private bool valDate;
        private bool valSum;
        private bool valAgreement;
        private bool flag;

        public CollateralDialogViewModel(string creditAgreement)
        {
            CreditAgreement = creditAgreement;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            Flag = false;
        }
        public CollateralDialogViewModel(string collateral_agreement, DateTime start_date, DateTime end_date,
                string credit_agreement, Currencies_directory currency, int typeId, double sum, string description, int? formId)
        {
            CollateralAgreement = collateral_agreement;
            StartDate = start_date;
            EndDate = end_date;
            CreditAgreement = credit_agreement;
            Currency = currency;
            SumText = sum.ToString();
            this.description = description;
            Flag = true;

            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                typeCollateral = (TypeCollateral)context.TypeCollaterals.FirstOrDefault(t => t.TypeId == typeId);
                formCollateral = (FormCollateral)context.FormCollaterals.FirstOrDefault(f => f.FormId == formId);
                Collateral collateral = (Collateral)context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == collateral_agreement);

                if (collateral.Firm != null)
                {
                    firmId = collateral.Id_Firm;                    
                }
                else
                {
                    personId = collateral.Id_Person;                    
                }
            }          
        }
        public bool Flag
        {
            get { return flag; }
            set
            {
                flag = value;
                OnPropertyChanged("Flag");
            }
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
        public string CreditAgreement
        {
            get { return creditAgreement; }
            set
            {
                creditAgreement = value;
                OnPropertyChanged("CreditAgreement");
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = (DateTime)value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = (DateTime)value;
                OnPropertyChanged("EndDate");
            }
        }
        public ObservableCollection<Currencies_directory> Currencies
        {
            get
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var currency = context.Currencies_directory;
                    ObservableCollection<Currencies_directory> cur = new ObservableCollection<Currencies_directory>();

                    foreach (var item in currency)
                    {
                        cur.Add(item);
                    }
                    return cur;
                }
            }
            set
            {
                currencies = value;
                OnPropertyChanged("Currencies");
            }
        }
        public Currencies_directory Currency
        {
            get { return currency; }
            set
            {
                currency = value;
                OnPropertyChanged("Currency");
            }
        }
        public string SumText
        {
            get { return sumText; }
            set
            {
                sumText = value;
                OnPropertyChanged("SumText");
            }
        }
        public string FirmName
        {
            get { return firmName; }
            set
            {
                firmName = value;
                OnPropertyChanged("FirmName");
            }
        }
        public string FirmId
        {
            get { return firmId; }
            set
            {
                if (PersonId != null)
                {
                    PersonId = null;
                    PersonName = null;
                }
                    
                firmId = value;
                OnPropertyChanged("FirmId");
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
        private bool FirmValidation(string value)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Firm firmInfo = (Firm)context.Firms.FirstOrDefault(f => f.Id_Firm == value);
                if (firmInfo != null)
                {
                    FirmName = firmInfo.Name;                    
                    return false;
                }
                else
                    return true;
            }
        }
        public string PersonId
        {
            get { return personId; }
            set
            {
                if (FirmId!=null)
                {
                    FirmId = null;
                    FirmName = null;
                }                                    
                personId = value;
                OnPropertyChanged("PersonId");
            }
        }
        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                OnPropertyChanged("PersonName");
            }
        }
        private bool PersonValidation(string value)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Individual personInfo = (Individual)context.Individuals.FirstOrDefault(p => p.Id_Person == value);
                if (personInfo != null)
                {
                    PersonName = personInfo.Name;
                    return false;
                }
                else
                    return true;
            }
        }
        public ObservableCollection<TypeCollateral> TypesCollateral
        {
            get
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var typeCollateral = context.TypeCollaterals;
                    ObservableCollection<TypeCollateral> type = new ObservableCollection<TypeCollateral>();

                    foreach (var item in typeCollateral)
                    {
                        type.Add(item);
                    }
                    return type;
                }
            }
            set
            {
                typesCollateral = value;
                OnPropertyChanged("TypesCollateral");
            }
        }
        public TypeCollateral TypeCollateral
        {
            get { return typeCollateral; }
            set
            {
                typeCollateral = value;
                OnPropertyChanged("TypeCollateral");
            }
        }
        public ObservableCollection<FormCollateral> FormsCollateral
        {
            get
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    var formCollateral = context.FormCollaterals;
                    ObservableCollection<FormCollateral> form = new ObservableCollection<FormCollateral>();

                    foreach (var item in formCollateral)
                    {
                        form.Add(item);
                    }
                    return form;
                }
            }
            set
            {
                formsCollateral = value;
                OnPropertyChanged("FormsCollateral");
            }
        }
        public FormCollateral FormCollateral
        {
            get { return formCollateral; }
            set
            {
                formCollateral = value;
                OnPropertyChanged("FormCollateral");
            }
        }
        private bool CollateralInfo()
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Collateral collateralInfo = (Collateral)context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == CollateralAgreement);
                if (collateralInfo != null)
                    return true;
                else return false;

            }
        }
        public ICommand SaveCollateral
        {
            get
            {
                return new RelayCommand(ExecuteSaveCollateral);
            }
        }       
        public void ExecuteSaveCollateral(object obj)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                if (valAgreement && valClient && valDate && valSum && Currency != null && TypeCollateral!=null &&
                    !string.IsNullOrWhiteSpace(CreditAgreement) && FormCollateral!=null && (!string.IsNullOrWhiteSpace(FirmId)|| !string.IsNullOrWhiteSpace(PersonId)))
                {
                    Collateral colRequest;
                    if (Flag==true)
                    {
                        colRequest = (Collateral)context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == CollateralAgreement);
                    }                    
                    else
                    {
                        colRequest = new Collateral();
                    }
                    colRequest.Collateral_agreement = CollateralAgreement;
                    colRequest.Start_date = StartDate;
                    colRequest.End_date = EndDate;
                    colRequest.Currency_Id = Currency.Currency_Id;
                    colRequest.Sum = double.Parse(SumText);
                    colRequest.Description = Description;
                    colRequest.TypeId = TypeCollateral.TypeId;
                    colRequest.FormId = FormCollateral.FormId;
                    colRequest.Credit_agreement = CreditAgreement;
                    if (FirmId != null)
                        colRequest.Id_Firm = FirmId;
                    else if (PersonId != null)
                        colRequest.Id_Person = PersonId;
                    if (Flag==false)
                    {
                        Monitoring_collateral monitoringAdd = new Monitoring_collateral();
                        monitoringAdd.Collateral_agreement = CollateralAgreement;
                        monitoringAdd.Previous_date = StartDate;
                        if (TypeCollateral.TypeId == 99551 || TypeCollateral.TypeId == 99552)
                            monitoringAdd.Planned_date = StartDate.AddYears(1);
                        if (TypeCollateral.TypeId == 99553)
                            monitoringAdd.Planned_date = StartDate.AddMonths(3);

                        context.Monitoring_collateral.Add(monitoringAdd);
                        context.Collaterals.Add(colRequest);
                    }
                    context.SaveChanges();
                    CloseWnd(this, new EventArgs());
                    MessageBox.Show("Операция проведена успешно", "Договор обеспечения",
               MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                    MessageBox.Show("Не все поля заполены корректно", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Error);
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
        }        
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "CollateralAgreement":
                        if (Flag == false)
                        {
                            if (CollateralInfo())
                            {
                                MessageBox.Show("Такой договор уже существует", "Ошибка при регистрации договора",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                error = "Такой договор уже существует ";
                                valAgreement = false;
                            }
                            else valAgreement = true;
                        }
                        else valAgreement = true;
                        break;
                    case "FirmId":
                        if (!string.IsNullOrWhiteSpace(FirmId))
                        {
                            if (FirmValidation(FirmId))
                            {
                                MessageBox.Show("Фирма в базе не найдена", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Error);
                                error = "Фирма в базе не найдена ";
                                valClient = false;
                            }
                            else valClient = true;
                        }                        
                        break;
                    case "PersonId":
                        if (!string.IsNullOrWhiteSpace(PersonId))
                        {
                            if (PersonValidation(PersonId))
                            {
                                MessageBox.Show("Клиент в базе не найден", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Error);
                                error = "Клиент в базе не найден";
                                valClient = false;
                            }
                            else valClient = true;
                        }                        
                        break;
                    case "StartDate":
                        if (StartDate.Date.CompareTo(EndDate.Date) >= 0)
                        {
                            error = "Дата окончания договора должна превышать дату начала";
                            valDate = false;
                        }
                        else valDate = true;
                        break;
                    case "EndDate":
                        if (EndDate.Date.CompareTo(StartDate.Date) <= 0)
                        {
                            error = "Дата окончания договора должна превышать дату начала";
                            valDate = false;
                        }
                        else valDate = true;
                        break;
                    case "SumText":
                        if (SumText != null)
                        {
                            double s;
                            if (!double.TryParse(SumText, out s))
                            {
                                error = "Сумма введена некорректно";
                                valSum = false;
                            }
                            else
                            {
                                if (s <= 0)
                                {
                                    error = "Сумма должна быть больше 0";
                                    valSum = false;
                                }
                                else if (s > 0)
                                {
                                    valSum = true;
                                }
                            }
                        }
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
