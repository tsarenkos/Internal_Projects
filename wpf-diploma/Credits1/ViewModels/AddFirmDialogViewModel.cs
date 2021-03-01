using System;
using System.Linq;
using Credits1.Models;
using System.Windows;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;

namespace Credits1.ViewModels
{    
    public class AddFirmDialogViewModel: ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string firmName;
        private string firmId;
        private DateTime dateRegistration;
        private string phone;
        private string legalAddress;
        private string validateFirm;
        private string validateDate;
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
        public DateTime DateRegistration
        {
            get { return dateRegistration; }
            set
            {
                dateRegistration = (DateTime)value;
                OnPropertyChanged("DateRegistration");
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
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
        private bool FirmInfo()
        {            
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Firm firmInfo = (Firm)context.Firms.FirstOrDefault(f => f.Id_Firm == FirmId);
                if (firmInfo != null)
                    return true;              
                else return false;

            }            
        }
        public AddFirmDialogViewModel()
        {
            DateRegistration = DateTime.Now;
        }
        public ICommand SaveFirm
        {
            get
            {                 
                return new RelayCommand(ExecuteSaveFirm);
            }
        }        
        public ICommand ResultCancel
        {
            get 
            {                
                return new RelayCommand(ExecuteResultCancel);
            }
        }        
        public void ExecuteSaveFirm(object obj)
        {
            if(string.IsNullOrWhiteSpace(validateFirm)&& string.IsNullOrWhiteSpace(validateDate)&&LegalAddress!=null
                &&FirmName!=null&&FirmId!=null&&DateRegistration!=null)
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Firm firmAdd = new Firm();
                    firmAdd.Name = FirmName;
                    firmAdd.Id_Firm = FirmId;
                    firmAdd.Registration_date = DateRegistration;
                    firmAdd.Phone = Phone;
                    firmAdd.Legal_address = LegalAddress;
                    context.Firms.Add(firmAdd);
                    context.SaveChanges();
                    CloseWnd(this, new EventArgs());
                }
                MessageBox.Show("Операция проведена успешно", "Регистрация клиента",
               MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("Не все поля заполены корректно", "Ошибка при регистрации клиента",
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
                    case "FirmId":
                        if (FirmId != null)
                        {
                            if (FirmInfo())
                            {
                                MessageBox.Show("Клиент с таким УНП уже существует", "Ошибка при регистрации клиента",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                error = "Клиент с таким УНП уже существует!";
                                validateFirm = error;
                                break;
                            }
                            else validateFirm = null;
                            if (!(FirmId.Length == 9))
                            {
                                error = "УНП должен содержать 9 цифр";
                                validateFirm = error;
                                break;
                            }
                            for (int i = 0; i < 9; i++)
                            {
                                if (!char.IsDigit(FirmId[i]))
                                {
                                    error = "УНП должен содержать только цифры";
                                    validateFirm = error;
                                    break;
                                }
                            }
                        }
                        else validateFirm = null;
                        break;                    
                    case "DateRegistration":
                        if((DateRegistration.Date>DateTime.Now))
                        {
                            error = "Некорректная дата регистрации фирмы";
                            validateDate = error;
                        }
                        else validateDate = null;
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
