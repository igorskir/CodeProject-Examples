using System;
using System.Linq;

namespace MVVMExample
{
    public interface IService
    {
        /// <summary>
        ///     Get the list of contacts
        /// </summary>
        /// <param name="callback"></param>
        void GetContacts(Action<IQueryable<ContactModel>> callback);

        /// <summary>
        ///     Delete the contact
        /// </summary>
        /// <param name="contact"></param>
        void DeleteContact(ContactModel contact);
    }
}
