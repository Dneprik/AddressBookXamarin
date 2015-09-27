//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using AddressBook.Annotations;
//using AddressBook.Models;
//using AddressBook.Pages;
//using AddressBook.Services;
//using Xamarin.Forms;

//namespace AddressBook.ViewModels
//{
//    class AddNewContactViewModel:INotifyPropertyChanged
//    {
//        public AddNewContactViewModel(INavigation navigation)
//        {
//            SaveNewContactCommand = new Command(SaveNewContact);
//            _navigation = navigation;
//        }

//        readonly  IContactService _contactService = new ContactService();
//        public ICommand SaveNewContactCommand { get; }
//        private INavigation _navigation;
//        private string _firstName;
//        private string _secondName;
//        private string _phoneNumber;
//        private string _additionalPhoneNumber;
//        private string _city;
//        private bool isValidData;
//        private string _errorLabel;

//        public event PropertyChangedEventHandler PropertyChanged;

//        [NotifyPropertyChangedInvocator]
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }



//        async private void SaveNewContact(object model)
//        {
//            isValidData = true;
//            ErrorLabel = "";
//            if (this.FirstName.Trim() == "") { ErrorLabel += "\nFirst Name is required";
//                isValidData = false;
//            }
//            if (this.SecondName.Trim() == "") {
//                ErrorLabel += "\nSecond Name is required";
//                isValidData = false;
//            }
//        if (this.PhoneNumber.Trim() == "") {
//                ErrorLabel += "\nPhone Number is required";
//            isValidData = false;
//        }

//            if (isValidData)
//            {

//                ContactModel cm = new ContactModel()
//                {
//                    FirstName = this.FirstName,
//                    SecondName = this.SecondName,
//                    PhoneNumber = this.PhoneNumber,
//                    AdditionalPhoneNumber = this.AdditionalPhoneNumber,
//                    City = this.City

//                };
//                _contactService.Add(cm);

//                await _navigation.PopAsync();
//            }
  
//        }
   
        

//        public string FirstName
//        {
//            get { return _firstName; }
//            set
//            {
//                if (value == _firstName) return;
//                _firstName = value;
//                OnPropertyChanged();
//            }
//        }

//        public string SecondName
//        {
//            get { return _secondName; }
//            set
//            {
//                if (value == _secondName) return;
//                _secondName = value;
//                OnPropertyChanged();
//            }
//        }

//        public string PhoneNumber
//        {
//            get { return _phoneNumber; }
//            set
//            {
//                if (value == _phoneNumber) return;
//                _phoneNumber = value;
//                OnPropertyChanged();
//            }
//        }

//        public string AdditionalPhoneNumber
//        {
//            get { return _additionalPhoneNumber; }
//            set
//            {
//                if (value == _additionalPhoneNumber) return;
//                _additionalPhoneNumber = value;
//                OnPropertyChanged();
//            }
//        }

//        public string City
//        {
//            get { return _city; }
//            set
//            {
//                if (value == _city) return;
//                _city = value;
//                OnPropertyChanged();
//            }
//        }

//        public string ErrorLabel
//        {
//            get { return _errorLabel; }
//            set
//            {
//                if (value == _errorLabel) return;
//                _errorLabel = value;
//                OnPropertyChanged();
//            }
//        }
//    }
//}
