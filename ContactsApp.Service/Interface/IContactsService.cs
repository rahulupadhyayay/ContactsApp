
using ContactsApp.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Service
{
    public interface IContactsService
    {
        public Task<List<ContactModel>> GetContacts(string jsonFile);
        public Task<bool> CreateUpdateContact(ContactModel inputModel, string jsonFile);
        public Task<bool> DeleteContact(int iContactId, string jsonFile);
        public Task<bool> EmailExists(int iContactId, string email, string jsonFile);
    }
}
