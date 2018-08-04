using System.Collections.Generic;
using EvolentContacts.Models;

namespace EvolentContacts.Repositories.Interfaces
{
    public interface IEvolentContactRepository
    {
        IEnumerable<EvolentContact> GetAllEvolentContacts();

        IEnumerable<EvolentContact> GetAllActiveEvolentContacts();

        IEnumerable<EvolentContact> GetAllInActiveEvolentContacts();

        EvolentContact GetEvolentContactFor(int id);

        // NOTE: Below three methods can be moved to a base class and current class can be derived from that base class
        bool ModifyEvolentContactFor(EvolentContact evolentContact);

        bool CreateEvolentContact(EvolentContact evolentContact);

        bool DeleteEvolentContactFor(int id);
    }
}
