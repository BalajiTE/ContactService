using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Contacts.Models
{
    public class ContactsContext : DbContext, IContactsContext
    {
        public ContactsContext() : base("ContactsContext")            
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void MarkAsModified(Contact item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsAdded(Contact item)
        {
            Entry(item).State = EntityState.Added;
        }

        public virtual DbSet<Contact> Contact { get; set;  }
    }
}