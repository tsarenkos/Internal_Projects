using System;
using System.Linq;
using System.ComponentModel;
using Credits1.Infrastructure;
using System.Windows.Input;
using Credits1.Models;
using System.Windows;

namespace Credits1.ViewModels
{
    public class AddPersonDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler CloseWnd;
        private string personName;
        private string personId;
        private DateTime birthday;        
        private string address;
        private string phone;
        private string validatePerson;
        private string validateDate;
        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                OnPropertyChanged("PersonName");
            }
        }
        public string PersonId
        {
            get { return personId; }
            set
            {
                personId = value;
                OnPropertyChanged("PersonId");
            }
        }
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = (DateTime)value;
                OnPropertyChanged("Birthday");
            }
        }        
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
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
        public AddPersonDialogViewModel()
        {
            Birthday = DateTime.Now;
        }
        private bool PersonValidate()
        {
            using (CreditsFirmsContext context = new CreditsFirmsContext())
            {
                Individual indInfo = context.Individuals.FirstOrDefault(i => i.Id_Person == PersonId);
                if (indInfo != null)
                    return true;
                else return false;

            }
        }
        public ICommand SavePerson
        {
            get
            {
                return new RelayCommand(ExecuteSavePerson);
            }
        }
        public ICommand ResultCancel
        {
            get
            {
                return new RelayCommand(ExecuteResultCancel);
            }
        }
        public void ExecuteSavePerson(object obj)
        {
            if (string.IsNullOrWhiteSpace(validatePerson) && string.IsNullOrWhiteSpace(validateDate) && !string.IsNullOrWhiteSpace(Address)
                && !string.IsNullOrWhiteSpace(PersonName) && PersonId != null && Birthday != null)
            {
                using (CreditsFirmsContext context = new CreditsFirmsContext())
                {
                    Individual personAdd = new Individual();
                    personAdd.Name = PersonName;
                    personAdd.Id_Person = PersonId;
                    personAdd.Birthday = Birthday;
                    personAdd.Address = Address;
                    personAdd.Phone = Phone;
                    context.Individuals.Add(personAdd);
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
                    case "PersonId":
                        if (PersonId != null)
                        {
                            if (PersonValidate())
                            {
                                MessageBox.Show("Клиент с таким личным номером уже существует", "Ошибка при регистрации клиента",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                error = "Клиент с таким личным номером уже существует!";
                                validatePerson = error;
                                break;
                            }
                            else validatePerson = null;
                            if (!(PersonId.Length == 14))
                            {
                                error = "личный номер должен содержать 14 цифр";
                                validatePerson = error;
                                break;
                            }                            
                        }
                        else validatePerson = null;
                        break;
                    case "Birthday":
                        if ((Birthday.Date >= DateTime.Now))
                        {
                            error = "Некорректная дата рождения";
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
