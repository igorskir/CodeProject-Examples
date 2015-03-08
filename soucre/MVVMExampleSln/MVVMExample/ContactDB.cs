using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVVMExample
{
    public static class ContactDb
    {
        private static List<ContactModel> _contacts = new List<ContactModel>();

        static ContactDb()
        {
            var jeremy = new ContactModel {FirstName = "Jeremy", LastName = "Likness", PhoneNumber = "404-555-1212"};
            var bill = new ContactModel {FirstName = "Bill", LastName = "Gates", PhoneNumber = "425-555-1212"};
           
            _contacts.Add(jeremy);
            _contacts.Add(bill);
        }

        public static IQueryable<ContactModel> GetContacts() { return _contacts.AsQueryable(); }

        public static void Delete(ContactModel contact)
        {
            if (_contacts.Contains(contact))
            {
                _contacts.Remove(contact);
            }
        }
    }
}
