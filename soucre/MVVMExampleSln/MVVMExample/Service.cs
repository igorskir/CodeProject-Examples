using System;
using System.Linq;
using System.Windows.Threading;

namespace MVVMExample
{
    public class Service : IService 
    {
        public void GetContacts(Action<IQueryable<ContactModel>> callback)
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += (o, e) =>
                              {
                                  ((DispatcherTimer) o).Stop();
                                  callback(ContactDb.GetContacts());
                              };
            timer.Start();
        }

        public void DeleteContact(ContactModel contact)
        {
            ContactDb.Delete(contact);            
        }
    }
}
