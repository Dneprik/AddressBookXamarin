using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.ViewModels;
using Xamarin.Forms;

namespace AddressBook.Pages
{
    public partial class ContactInfo : ContentPage
    {
        public ContactInfo(int userId=0)
        {
            InitializeComponent();
            BindingContext=new ContactInfoViewModel(Navigation, userId);
        }
    }
}
