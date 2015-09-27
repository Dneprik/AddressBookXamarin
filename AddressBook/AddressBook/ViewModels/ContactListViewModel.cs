using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AddressBook.Annotations;
using AddressBook.Models;
using AddressBook.Pages;
using AddressBook.Services;
using Xamarin.Forms;

namespace AddressBook.ViewModels
{
    internal class ContactListViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        public static INavigation _navigation;

        private static readonly IContactService _contactService = new ContactService();

        static ContactListViewModel()
        {
            _contactList = new ObservableCollection<ContactViewModel>();
        }

        public ContactListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            AddNewContactCommand = new Command(AddNewContact);
        }


        //Collection of all contacts from DB
        public static ObservableCollection<ContactViewModel> _contactList { get; set; }

        public ICommand AddNewContactCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;


        public async void ContactSelect(object obj)
        {
            await _navigation.PushAsync(new ContactInfo(Convert.ToInt32(obj)));
        }


        private async void AddNewContact()
        {
            await _navigation.PushAsync(new ContactInfo());
            //await _navigation.PushAsync(new AddNewContact());
        }

        public static IEnumerable<ContactViewModel> GetAllConntacts()
        {
            _contactList.Clear();
            foreach (var contactModel in _contactService.GetAllContacts())
            {
                _contactList.Add((ContactViewModel) contactModel);
            }
            return _contactList;
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ContactViewModel : INotifyPropertyChanged
    {
        private static readonly IContactService _contactService = new ContactService();


        private string _additionalPhoneNumber;
        private string _city;
        private string _firstName;
        private int _id;
        private string _phoneNumber;
        private string _secondName;

        public ContactViewModel()
        {
            DeleteContactCommand = new Command(DeleteContact);
            ContactSelectCommand = new Command(ContactSelect);
        }

        public ICommand DeleteContactCommand { get; }
        public ICommand ContactSelectCommand { get; }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string SecondName
        {
            get { return _secondName; }
            set
            {
                if (value == _secondName) return;
                _secondName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value == _phoneNumber) return;
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string AdditionalPhoneNumber
        {
            get { return _additionalPhoneNumber; }
            set
            {
                if (value == _additionalPhoneNumber) return;
                _additionalPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return FirstName + " " + SecondName; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteContact(object obj)
        {
            _contactService.DeleteById(Convert.ToInt32(obj));
            var itemForRemoving =
                ContactListViewModel._contactList.FirstOrDefault(item => item.Id == Convert.ToInt32(obj));
            ContactListViewModel._contactList.Remove(itemForRemoving);
        }

        private async void ContactSelect(object obj)
        {
            await ContactListViewModel._navigation.PushAsync(new ContactInfo(Convert.ToInt32(obj)));
        }


        public static explicit operator ContactViewModel(ContactModel v)
        {
            return new ContactViewModel
            {
                FirstName = v.FirstName,
                SecondName = v.SecondName,
                Id = v.Id,
                City = v.City,
                PhoneNumber = v.PhoneNumber,
                AdditionalPhoneNumber = v.AdditionalPhoneNumber
            };
        }
    }
}