using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EvolentContacts.Models
{
    public class EvolentContactsContext : DbContext, IEvolentContactsContext
    {
        public EvolentContactsContext() : base("EvolentContactsContext")            
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void MarkAsModified(EvolentContact item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public void MarkAsAdded(EvolentContact item)
        {
            Entry(item).State = EntityState.Added;
        }

        public virtual DbSet<EvolentContact> EvolentContact { get; set;  }
    }
}