using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AddressBook.Annotations;
using AddressBook.Models;
using AddressBook.Services;
using Xamarin.Forms;

namespace AddressBook.ViewModels
{
    public class ContactInfoViewModel : INotifyPropertyChanged
    {
        public enum ToolBarStates
        {
            Save = 0,
            Edit = 1
        }

        private readonly IContactService _contactService = new ContactService();
        private readonly ContactViewModel _contactViewModel;

        private readonly INavigation _navigation;

        private readonly string[] ToolBarValue = {"Save", "Edit"};
        private string _errorLabel;
        private bool _isEnabled;
        private ToolBarStates _toolBarStates;
        private string _toolBarText;

        private bool isValidData;

        public ContactInfoViewModel(INavigation navigation, int userId = 0)
        {
            _navigation = navigation;
            EditSaveContactCommand = new Command(EditSaveContact);

            //Проверяет какой тулбар item будет на странице Save\Edit
            if (userId == 0)
            {
                SetToolBarState(ToolBarStates.Save);
                IsEnabled = true;
                _contactViewModel = new ContactViewModel();
            }
            else
            {
                SetToolBarState(ToolBarStates.Edit);
                IsEnabled = false;
                _contactViewModel = (ContactViewModel) _contactService.GetContactById(userId);
            }
        }

        public ICommand EditSaveContactCommand { get; }
        

        public ToolBarStates ToolBarState
        {
            get { return _toolBarStates; }
            set
            {
                if (value == _toolBarStates) return;
                _toolBarStates = value;
                OnPropertyChanged();
            }
        }

        public string ToolBarText
        {
            get { return _toolBarText; }
            set
            {
                if (value == _toolBarText) return;
                _toolBarText = value;
                OnPropertyChanged();
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return _contactViewModel.Id; }
            set
            {
                if (value == _contactViewModel.Id) return;
                _contactViewModel.Id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _contactViewModel.FirstName; }
            set
            {
                if (value == _contactViewModel.FirstName) return;
                _contactViewModel.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string SecondName
        {
            get { return _contactViewModel.SecondName; }
            set
            {
                if (value == _contactViewModel.SecondName) return;
                _contactViewModel.SecondName = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get { return _contactViewModel.PhoneNumber; }
            set
            {
                if (value == _contactViewModel.PhoneNumber) return;
                _contactViewModel.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string AdditionalPhoneNumber
        {
            get { return _contactViewModel.AdditionalPhoneNumber; }
            set
            {
                if (value == _contactViewModel.AdditionalPhoneNumber) return;
                _contactViewModel.AdditionalPhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return _contactViewModel.City; }
            set
            {
                if (value == _contactViewModel.City) return;
                _contactViewModel.City = value;
                OnPropertyChanged();
            }
        }

        public string ErrorLabel
        {
            get { return _errorLabel; }
            set
            {
                if (value == _errorLabel) return;
                _errorLabel = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private async void SetToolBarState(ToolBarStates toolBarState)
        {
            //Set tool bar item state Save or Edit
            ToolBarState = toolBarState;
            //Bug with Toolbar. Text isn't updated. It's look likes Xamarin issue.
            ToolBarText = ToolBarValue[(int) ToolBarState];
        }

        private async void SaveNewContact()
        {
            isValidData = true;
            ErrorLabel = "";
            if (FirstName.Trim() == "")
            {
                ErrorLabel += "\nFirst Name is required";
                isValidData = false;
            }
            if (SecondName.Trim() == "")
            {
                ErrorLabel += "\nSecond Name is required";
                isValidData = false;
            }
            if (PhoneNumber.Trim() == "")
            {
                ErrorLabel += "\nPhone Number is required";
                isValidData = false;
            }

            if (isValidData)
            {
                var cm = new ContactModel
                {
                    Id = Id,
                    FirstName = FirstName,
                    SecondName = SecondName,
                    PhoneNumber = PhoneNumber,
                    AdditionalPhoneNumber = AdditionalPhoneNumber,
                    City = City
                };
                if (Id == 0)
                {
                    _contactService.Add(cm);
                }
                else
                {
                    _contactService.Edit(cm);
                }
                await _navigation.PopAsync();
            }
        }


        private void EditSaveContact(object obj)
        {
            if (ToolBarState == ToolBarStates.Save)
                SaveNewContact();
            else
            {
                IsEnabled = true;
                SetToolBarState(ToolBarStates.Save);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}