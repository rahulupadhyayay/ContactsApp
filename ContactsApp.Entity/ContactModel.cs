using System;

namespace ContactsApp.Entity
{
    public class ContactModel
    {
        public int iContactId { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public string strEmail { get; set; }
        public bool tIsDeleted { get; set; } = false;
    }
    public class ContactResponseModel
    {
        public int iContactId { get; set; }
        public string strFirstName { get; set; }
        public string strLastName { get; set; }
        public string strEmail { get; set; }
    }
}
