using System;
using System.Linq;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Credits1.ViewModels
{
   public class AddCreditDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string credit_agreement;
        private DateTime startDate;
        private DateTime endDate;        
        private ObservableCollection<Currencies_directory> currencies;
        private Currencies_directory currency;
        private string sumText;
        private string rateText;
        private string firmName;
        private string firmId;        
        private string legalAddress;        

        private bool valFirm;        
        private bool valDate;
        private bool valSum;
        private bool valAgreement;
        private bool valRate;

        public string Credit_agreement
        {
            get { return credit_agreement; }
            set
            {
                credit_agreement = value;
                OnPropertyChanged("Credit_agreement");
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
                firmId = value;
                OnPropertyChanged("FirmId");                
            }
        }
        public AddCreditDialogViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);                      
        }
        private bool FirmValidation()
        {
            if (FirmId != null)
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Firm firmInfo = (Firm)context.Firms.FirstOrDefault(f => f.Id_Firm == FirmId);
                    if (firmInfo != null)
                    {
                        FirmName = firmInfo.Name;
                        LegalAddress = firmInfo.Legal_address;
                        return false;
                    }
                    else
                    {
                        FirmName = "";
                        LegalAddress = "";
                        return true;
                    }
                }
            }
            else return false;                 
        }
        private bool CreditInfo()
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Credit creditInfo = (Credit)context.Credits.FirstOrDefault(c => c.Credit_agreement == Credit_agreement);
                if (creditInfo != null)
                    return true;
                else return false;
            }
        }
        public string LegalAddress
        {
            get { return legalAddress; }
            set
            {
                legalAddress = value;
                OnPropertyChanged("LegalAddress");
            }
        }
        public string RateText
        {
            get { return rateText; }
            set
            {
                rateText = value;
                OnPropertyChanged("RateText");
            }
        }
        public ICommand SaveCredit
        {
            get
            {
                return new RelayCommand(ExecuteSaveCredit);
            }
        }
        public ICommand ResultCancel
        {
            get
            {
                return new RelayCommand(ExecuteResultCancel);
            }
        }
        public void ExecuteSaveCredit(object obj)
        {
            if(valAgreement && valDate && valFirm && valSum && valRate && Currency!=null && !string.IsNullOrWhiteSpace(Credit_agreement)
                && !string.IsNullOrWhiteSpace(FirmId) && !string.IsNullOrWhiteSpace(SumText) && !string.IsNullOrWhiteSpace(RateText))
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Credit creditAdd = new Credit();
                    creditAdd.Credit_agreement = Credit_agreement;
                    creditAdd.Start_date = StartDate;
                    creditAdd.End_date = EndDate;
                    creditAdd.Currency_Id = Currency.Currency_Id;
                    creditAdd.Sum = int.Parse(SumText);
                    creditAdd.Id_Firm = FirmId;
                    context.Credits.Add(creditAdd);

                    Principal_balance balanceAdd = new Principal_balance();
                    balanceAdd.Credit_agreement = Credit_agreement;
                    balanceAdd.Date = StartDate;
                    balanceAdd.Currency_Id = Currency.Currency_Id;
                    balanceAdd.Balance = int.Parse(SumText);
                    context.Principal_balance.Add(balanceAdd);

                    Interest_rate rateAdd = new Interest_rate();
                    rateAdd.Credit_agreement = Credit_agreement;
                    rateAdd.Date = StartDate;
                    rateAdd.Rate = double.Parse(RateText);
                    context.Interest_rate.Add(rateAdd);

                    context.SaveChanges();
                    CloseWnd(this, new EventArgs());
                }
                MessageBox.Show("Операция проведена успешно", "Регистрация кредитного договора",
               MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }    
            else
                MessageBox.Show("Не все поля заполены корректно", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Error);
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
                    case "Credit_agreement":
                        if (CreditInfo())
                        {
                            MessageBox.Show("Такой договор уже существует", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            error = "Такой договор уже существует ";
                            valAgreement = false;
                        }
                        else valAgreement = true;
                        break;
                    case "FirmId":
                        if (FirmValidation())
                        {
                            MessageBox.Show("Фирма в базе не найдена", "Ошибка при регистрации договора",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            error = "Фирма в базе не найдена ";
                            valFirm = false;
                        }
                        else valFirm = true;
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
                        if(SumText!=null)
                        {
                            int s;
                            if (!int.TryParse(SumText, out s))
                            {
                                error = "Сумма введена некорректно";
                                valSum = false;
                            }
                            else
                            {
                                if (s <= 0)
                                {
                                    error = "Сумма по кредиту должна быть больше 0";
                                    valSum = false;
                                }
                                else if (s > 0)
                                {
                                    valSum = true;
                                }
                            }
                        }                        
                        break;
                    case "RateText":
                        if(RateText!=null)
                        {
                            double r;
                            if (!double.TryParse(RateText, out r))
                            {
                                error = "Ставка введена некорректно";
                                valRate = false;
                            }
                            else
                            {
                                if (r <= 0)
                                {
                                    error = "Ставка по кредиту должна быть больше 0";
                                    valRate = false;
                                }
                                else if (r > 0)
                                {
                                    valRate = true;
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
