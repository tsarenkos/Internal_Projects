using System;
using System.Linq;
using Credits1.Models;
using System.Windows;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;

namespace Credits1.ViewModels
{
    public class CalculationInterestDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string creditAgreement;
        private bool valDate;
        private DateTime startDate;
        private DateTime endDate;
        private double calculation;     
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
        public double Calculation
        {
            get { return calculation; }
            set
            {
                calculation = value;
                OnPropertyChanged("Calculation");
            }
        }
        public CalculationInterestDialogViewModel(string creditAgreement)
        {
            this.creditAgreement = creditAgreement;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
        private ICommand calculationCommand;
        public ICommand CalculationCommand
        {
            get
            {
                if (calculationCommand == null)
                    calculationCommand = new RelayCommand(ExecuteCalculationCommand, CanExecuteCalculationCommand);
                return calculationCommand;
            }
        }
        private bool CanExecuteCalculationCommand(object obj)
        {
            if (valDate == true)
                return true;
            else return false;
        }
        public void ExecuteCalculationCommand(object obj)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                double total = 0;
                double balanceDay = 0;
                double balanceDayOver = 0;
                double rateDay = 0;
                double rateDayOver = 0;

                for (DateTime date = startDate;date<=endDate;date=date.AddDays(1))
                {
                    if (date.Day == 31)
                        continue;
                    var balance = context.Principal_balance.Where(p => (p.Credit_agreement == CreditAgreement)&&(p.Date <= date))?.AsEnumerable().LastOrDefault();
                    var balanceOver = context.Overdue_principal_debt.Where(p => (p.Credit_agreement == CreditAgreement)&&(p.Date <= date))?.AsEnumerable().LastOrDefault();
                    if (balance != null)
                    {
                        balanceDay = balance.Balance;
                        rateDay = context.Interest_rate.Where(i => (i.Credit_agreement == CreditAgreement) && (i.Date <= date)).AsEnumerable().LastOrDefault().Rate;
                        if (balanceOver!=null)
                        {
                            balanceDayOver = balanceOver.Sum;
                            rateDayOver = rateDay * 2;
                        }                            
                    }
                    else continue;    
                    
                    double sumDay = ((balanceDay * rateDay / 100)+(balanceDayOver*rateDayOver/100))/360;
                    if((date.Month==2)&&(date.Year%4!=0))
                    {
                        if (date.Day == 28)
                            sumDay = sumDay * 3;
                    }
                    else if(date.Month == 2 && (date.Year % 4 == 0))
                    {
                        if (date.Day == 29)
                            sumDay = sumDay * 2;
                    }
                    total += sumDay;
                }
                Calculation = total;
            }
        }
        private ICommand accrueCommand;
        public ICommand AccrueCommand
        {
            get
            {
                if (accrueCommand == null)
                    accrueCommand = new RelayCommand(ExecuteAccrueCommand, CanExecuteAccrueCommand);
                return accrueCommand;
            }
        }
        private bool CanExecuteAccrueCommand(object obj)
        {
            if (Calculation != 0)
                return true;
            else return false;
        }
        public void ExecuteAccrueCommand(object obj)
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                var item = context.Accrued_interest.FirstOrDefault(i => i.Credit_agreement == CreditAgreement);
                if(item==null)
                {
                    Accrued_interest accrued = new Accrued_interest();
                    accrued.Credit_agreement = CreditAgreement;
                    accrued.Sum = Calculation;
                    accrued.Currency_Id = context.Credits.FirstOrDefault(c => c.Credit_agreement == CreditAgreement).Currency_Id;
                    context.Accrued_interest.Add(accrued);
                }
                else
                {
                    Accrued_interest accrued = (Accrued_interest)item;
                    accrued.Sum = Calculation;
                }                
                context.SaveChanges();
            }
            MessageBox.Show("Операция проведена успешно", "Начисление процентов",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
            CloseWnd(this, new EventArgs());
        }        
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "StartDate":
                        if (StartDate.Date.CompareTo(EndDate.Date) > 0)
                        {
                            error = "Дата окончания должна превышать дату начала";
                            valDate = false;
                        }
                        else valDate = true;
                        break;
                    case "EndDate":
                        if (EndDate.Date.CompareTo(StartDate.Date) < 0)
                        {
                            error = "Дата окончания должна превышать дату начала";
                            valDate = false;
                        }
                        else valDate = true;
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
