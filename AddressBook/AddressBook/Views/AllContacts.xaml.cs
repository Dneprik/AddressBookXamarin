using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Models;
using AddressBook.ViewModels;
using Xamarin.Forms;

namespace AddressBook.Pages
{
    public partial class AllContacts : ContentPage
    {
        
        public AllContacts()
        {
            InitializeComponent();
            BindingContext = new ContactListViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           // ContactList.BackgroundColor = Color.Aqua;
           // List<ContactModel> contactList = new List<ContactModel>();
            ContactList.ItemsSource = ContactListViewModel.GetAllConntacts();
        }

        
        

    }
}
