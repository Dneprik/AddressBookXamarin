using System.Collections.Generic;
using System.Linq;
using AddressBook.Models;
using AddressBook.Services.DataBase;
using SQLite;
using Xamarin.Forms;

namespace AddressBook.Services
{
    public interface IContactService
    {
        void Add(ContactModel contactModel);
        void Edit(ContactModel contactModel);
        void DeleteById(int Id);
        ContactModel GetContactById(int Id);
        List<ContactModel> GetAllContacts();
    }

    public class ContactService : IContactService
    {
        public readonly SQLiteConnection _connection;

        public ContactService()
        {
            _connection = DependencyService.Get<ISQLite>().GetConnection();
            _connection.CreateTable<ContactModel>();
        }

        public void Add(ContactModel contactModel)
        {
            _connection.Insert(contactModel);
        }

        public void Edit(ContactModel contactModel)
        {
            _connection.Table<ContactModel>().Delete(model => model.Id == contactModel.Id);
            Add(contactModel);
        }

        public void DeleteById(int Id)
        {
            _connection.Table<ContactModel>().Delete(model => model.Id == Id);
        }

        public ContactModel GetContactById(int Id)
        {
            return _connection.Table<ContactModel>().FirstOrDefault(model => model.Id == Id);
        }

        public List<ContactModel> GetAllContacts()
        {
            return _connection.Table<ContactModel>().ToList().OrderBy(model => model.FirstName).ToList();
        }
    }
}