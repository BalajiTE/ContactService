using System;
using System.Collections.Generic;
using System.Linq;
using EvolentContacts.Models;
using EvolentContacts.Repositories.Interfaces;

namespace EvolentContacts.Repositories
{
    public class EvolentContactRepository : IEvolentContactRepository
    {
        private readonly EvolentContactsContext _context;
        public EvolentContactRepository(EvolentContactsContext context) 
        {
            _context = context;
        }

        public IEnumerable<EvolentContact> GetAllEvolentContacts()
        {
            var evolentContacts = (from ec in _context.EvolentContact
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return evolentContacts;
        }

        public IEnumerable<EvolentContact> GetAllActiveEvolentContacts()
        {
            var evolentContacts = (from ec in _context.EvolentContact
                                   where ec.Status == true
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return evolentContacts;
        }

        public IEnumerable<EvolentContact> GetAllInActiveEvolentContacts()
        {
            var evolentContacts = (from ec in _context.EvolentContact
                                   where ec.Status == false
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return evolentContacts;
        }

        public EvolentContact GetEvolentContactFor(int id)
        {
            var evolentContact = (from ec in _context.EvolentContact
                                  where ec.ID == id
                                  select ec).FirstOrDefault();

            return evolentContact;
        }

        public bool CreateEvolentContact(EvolentContact evolentContact)
        {
            try
            {
                if (evolentContact != null)
                {
                    _context.MarkAsAdded(evolentContact);
                    _context.EvolentContact.Add(evolentContact);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool ModifyEvolentContactFor(EvolentContact evolentContact)
        {
            try
            {
                if (evolentContact != null)
                {
                    _context.MarkAsModified(evolentContact);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool DeleteEvolentContactFor(int id)
        {
            var selectedEvolentContact = GetEvolentContactFor(id);

            try
            {
                if (selectedEvolentContact != null)
                {
                    selectedEvolentContact.Status = false;
                    _context.MarkAsModified(selectedEvolentContact);
                    _context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}