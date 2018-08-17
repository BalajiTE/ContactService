using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Models;
using Contacts.Repositories.Interfaces;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContext _context;
        public ContactRepository(ContactsContext context) 
        {
            _context = context;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            var Contacts = (from ec in _context.Contact
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return Contacts;
        }

        public IEnumerable<Contact> GetAllActiveContacts()
        {
            var contacts = (from ec in _context.Contact
                                   where ec.Status == true
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return contacts;
        }

        public IEnumerable<Contact> GetAllInActiveContacts()
        {
            var contacts = (from ec in _context.Contact
                                   where ec.Status == false
                                   orderby ec.ID descending
                                   select ec).AsEnumerable();
            return contacts;
        }

        public Contact GetContactFor(int id)
        {
            var contact = (from ec in _context.Contact
                                  where ec.ID == id
                                  select ec).FirstOrDefault();

            return contact;
        }

        public bool CreateContact(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    _context.MarkAsAdded(contact);
                    _context.Contact.Add(contact);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool ModifyContactFor(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    _context.MarkAsModified(contact);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool DeleteContactFor(int id)
        {
            var selectedContacts = GetContactFor(id);

            try
            {
                if (selectedContacts != null)
                {
                    selectedContacts.Status = false;
                    _context.MarkAsModified(selectedContacts);
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