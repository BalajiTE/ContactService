using System.Collections.Generic;
using Contacts.Models;

namespace Contacts.Repositories.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts();

        IEnumerable<Contact> GetAllActiveContacts();

        IEnumerable<Contact> GetAllInActiveContacts();

        Contact GetContactFor(int id);

        // NOTE: Below three methods can be moved to a base class and current class can be derived from that base class
        bool ModifyContactFor(Contact Contacts);

        bool CreateContact(Contact Contacts);

        bool DeleteContactFor(int id);
    }
}
