using System.Data.Entity;

namespace EvolentContacts.Models
{
    public interface IEvolentContactsContext
    {
        DbSet<EvolentContact> EvolentContact { get; set; }

        int SaveChanges();

        void MarkAsModified(EvolentContact item);

        void MarkAsAdded(EvolentContact item);
    }
}
