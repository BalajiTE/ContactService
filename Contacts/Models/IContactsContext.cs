using System.Data.Entity;

namespace Contacts.Models
{
    public interface IContactsContext
    {
        DbSet<Contact> Contact { get; set; }

        int SaveChanges();

        void MarkAsModified(Contact item);

        void MarkAsAdded(Contact item);
    }
}
