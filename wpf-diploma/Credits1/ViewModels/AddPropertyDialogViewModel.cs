using System;
using System.Collections.Generic;
using System.Linq;
using Credits1.Infrastructure;
using System.Windows.Input;
using System.ComponentModel;
using Credits1.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Credits1.ViewModels
{
    class AddPropertyDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string collateralAgreement;        
        private ObservableCollection<Currencies_directory> currencies;
        private Currencies_directory currency;
        private string priceText;
        private string clientName;        
        private string name;
        private string description;
        private string propertyId;
        private bool valPrice;
        private bool valProperty;
        public AddPropertyDialogViewModel(string collateralAgreement, string clientName)
        {
            ClientName = clientName;
            CollateralAgreement = collateralAgreement;
            Names = new List<string> { "Автотранспорт", "Недвижимость" };
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
        public string PriceText
        {
            get { return priceText; }
            set
            {
                priceText = value;
                OnPropertyChanged("PriceText");
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
        public List<string> Names { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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
        public string PropertyId
        {
            get { return propertyId; }
            set
            {
                propertyId = value;
                OnPropertyChanged("PropertyId");
            }
        }
        private bool PropertyValidate()
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Property propertyInfo = context.Properties.FirstOrDefault(p => p.Id == PropertyId);
                if (propertyInfo != null)
                    return true;
                else return false;

            }
        }
        public ICommand SaveProperty
        {
            get
            {
                return new RelayCommand(ExecuteSaveProperty);
            }
        }
        public ICommand ResultCancel
        {
            get
            {
                return new RelayCommand(ExecuteResultCancel);
            }
        }
        public void ExecuteSaveProperty(object obj)
        {
            if (valProperty && valPrice && Currency != null && Name!=null && !string.IsNullOrWhiteSpace(PropertyId) && !string.IsNullOrWhiteSpace(PriceText))
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Property propertyAdd = new Property();
                    Collateral collateralAdd = context.Collaterals.FirstOrDefault(c => c.Collateral_agreement == CollateralAgreement);
                    propertyAdd.Collateral_agreement = CollateralAgreement;
                    propertyAdd.Currency_Id = Currency.Currency_Id;
                    propertyAdd.Price = double.Parse(PriceText);
                    propertyAdd.Id = PropertyId;
                    propertyAdd.Name = Name;
                    propertyAdd.Description = Description;
                    collateralAdd.Properties.Add(propertyAdd);

                    context.Properties.Add(propertyAdd);
                    context.SaveChanges();
                    CloseWnd(this, new EventArgs());
                }
                MessageBox.Show("Операция проведена успешно", "Регистрация предмета залога",
               MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
                MessageBox.Show("Не все поля заполены корректно", "Ошибка при регистрации залога",
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
                    case "PriceText":
                        if (PriceText != null)
                        {
                            double p;
                            if (!double.TryParse(PriceText, out p))
                            {
                                error = "Стоимость введена некорректно";
                                valPrice = false;
                            }
                            else
                            {
                                if (p <= 0)
                                {
                                    error = "Стоимость должна быть больше 0";
                                    valPrice = false;
                                }
                                else if (p > 0)
                                {
                                    valPrice = true;
                                }
                            }
                        }
                        break;
                    case "PropertyId":
                        if (PropertyId != null)
                        {
                            if (PropertyValidate())
                            {
                                MessageBox.Show("Такой предмет залога уже существует", "Ошибка при регистрации залога",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                error = "Такой предмет залога уже существует ";
                                valProperty = false;
                            }
                            else valProperty = true;
                        }
                        else valProperty = true;
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
