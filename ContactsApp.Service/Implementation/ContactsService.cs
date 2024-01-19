using ContactsApp.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsApp.Service
{
    public class ContactsService : IContactsService
    {

        public ContactsService()
        {

        }

        /// <summary>
        /// returns all contacts which are not deleted based on tIsDelete flag
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public async Task<List<ContactModel>> GetContacts(string jsonFile)
        {
            var jsonString = await System.IO.File.ReadAllTextAsync(jsonFile);
            List<ContactModel> item = JsonConvert.DeserializeObject<List<ContactModel>>(jsonString);
            if (item == null) return null;
            return item.Where(x => x.tIsDeleted == false).ToList();
        }

        /// <summary>
        /// create contact if no contactid found and update existing if contactid passed
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public async Task<bool> CreateUpdateContact(ContactModel inputModel, string jsonFile)
        {
            var jsonString = await System.IO.File.ReadAllTextAsync(jsonFile);
            var contactList = JsonConvert.DeserializeObject<List<ContactModel>>(jsonString);
            if (inputModel.iContactId > 0)
            {
                var contact = contactList.Where(obj => obj.iContactId == inputModel.iContactId).FirstOrDefault();
                contact.strFirstName = inputModel.strFirstName;
                contact.strLastName = inputModel.strLastName;
                contact.strEmail = inputModel.strEmail;
                string output = JsonConvert.SerializeObject(contactList, Formatting.Indented);
                await File.WriteAllTextAsync(jsonFile, output);
            }
            else
            {
                if (contactList == null)
                    contactList = new List<ContactModel>();
                inputModel.iContactId = contactList.Count + 1;
                contactList.Add(inputModel);
                var updatedJsonString = JsonConvert.SerializeObject(contactList);
                await System.IO.File.WriteAllTextAsync(jsonFile, updatedJsonString);
            }
            return true;
        }

        /// <summary>
        /// soft delete the contact for given contactid by setting flag to true
        /// </summary>
        /// <param name="iContactId"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContact(int iContactId, string jsonFile)
        {
            var jsonString = await System.IO.File.ReadAllTextAsync(jsonFile);
            var contactList = JsonConvert.DeserializeObject<List<ContactModel>>(jsonString);
            foreach (var contact in contactList.Where(obj => obj.iContactId == iContactId))
            {
                contact.tIsDeleted = true;
            }
            string output = JsonConvert.SerializeObject(contactList, Formatting.Indented);
            await File.WriteAllTextAsync(jsonFile, output);
            return true;
        }

        /// <summary>
        /// To check for Duplicate email
        /// </summary>
        /// <param name="iContactId"></param>
        /// <param name="email"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        public async Task<bool> EmailExists(int iContactId, string email, string jsonFile)
        {
            var jsonString = await System.IO.File.ReadAllTextAsync(jsonFile);
            var contactList = JsonConvert.DeserializeObject<List<ContactModel>>(jsonString);
            if (iContactId > 0)
            {
                var contact = contactList.Where(obj => obj.iContactId != iContactId && obj.strEmail.ToLower() == email && !obj.tIsDeleted).FirstOrDefault();
                if (contact != null && contact.iContactId > 0)
                {
                    return false;
                }
            }
            else
            {
                var contact = contactList.Where(obj => obj.strEmail.ToLower() == email && !obj.tIsDeleted).FirstOrDefault();
                if (contact != null && contact.iContactId > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
