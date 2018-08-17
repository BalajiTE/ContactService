using System;
using System.Collections.Generic;
using System.Web.Http;
using Contacts.Models;
using Contacts.Repositories.Interfaces;

namespace Contacts.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactController : ApiController
    {
        #region private variables

        // variable to hold local ContactRepository instance 
        private readonly IContactRepository _contactRepository;

        #endregion

        #region Constructor
        public ContactController(IContactRepository contactRepository) 
        {
            // Assign the injected repository to the local instance 
            _contactRepository = contactRepository;
        }

        #endregion

        #region Service Operations

        // HttpGet method to pull all Contacts
        [HttpGet, Route("GetAllContacts")] 
        public IEnumerable<Contact> GetAllContacts()
        {
            try
            {
                var contact = _contactRepository.GetAllContacts();
                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull all Contacts
        [HttpGet, Route("GetAllActiveContacts")]
        public IEnumerable<Contact> GetAllActiveContacts()
        {
            try
            {
                var contact = _contactRepository.GetAllActiveContacts();
                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull all Contacts
        [HttpGet, Route("GetAllInActiveContacts")]
        public IEnumerable<Contact> GetAllInActiveContacts()
        {
            try
            {
                var contact = _contactRepository.GetAllInActiveContacts();
                return contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull Contacts for the given Id
        [HttpGet, Route("GetContactFor/{contactId:int}")]
        public Contact GetContactFor(int contactId)
        {
            try
            {
                var Contact = _contactRepository.GetContactFor((contactId));

                return Contact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPost method to create new Contacts
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [ActionName("CreateContact")]
        public bool CreateContact([FromBody]Contact contact)
        {
            try
            {
                var isAdded = _contactRepository.CreateContact(contact);
                return isAdded;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPut method to Update ontacts
        [AcceptVerbs("GET", "POST")]
        [HttpPut, Route("UpdateContact")]
        public bool UpdateContact([FromBody]Contact modifiedContact)
        {
            try
            {
                var isModified = _contactRepository.ModifyContactFor(modifiedContact);

                return isModified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPut method to Delete Contacts
        [AcceptVerbs("GET", "POST")]
        [HttpPut, Route("DeleteContact/{id:int}")]
        public bool DeleteContact(int id)
        {
            try
            {
                var isDeleted = _contactRepository.DeleteContactFor(id);

                return isDeleted;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
