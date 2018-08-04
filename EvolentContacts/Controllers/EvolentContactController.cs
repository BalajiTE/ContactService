using System;
using System.Collections.Generic;
using System.Web.Http;
using EvolentContacts.Models;
using EvolentContacts.Repositories.Interfaces;

namespace EvolentContacts.Controllers
{
    [RoutePrefix("api/EvolentContacts")]
    public class EvolentContactController : ApiController
    {
        #region private variables

        // variable to hold local EvolentContactRepository instance 
        private readonly IEvolentContactRepository _evolentContactRepository;

        #endregion

        #region Constructor
        public EvolentContactController(IEvolentContactRepository evolentContactRepository) 
        {
            // Assign the injected repository to the local instance 
            _evolentContactRepository = evolentContactRepository;
        }

        #endregion

        #region Service Operations

        // HttpGet method to pull all EvolentContacts
        [HttpGet, Route("GetAllEvolentContacts")] 
        public IEnumerable<EvolentContact> GetAllEvolentContacts()
        {
            try
            {
                var evolentContact = _evolentContactRepository.GetAllEvolentContacts();
                return evolentContact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull all EvolentContacts
        [HttpGet, Route("GetAllActiveEvolentContacts")]
        public IEnumerable<EvolentContact> GetAllActiveEvolentContacts()
        {
            try
            {
                var evolentContact = _evolentContactRepository.GetAllActiveEvolentContacts();
                return evolentContact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull all EvolentContacts
        [HttpGet, Route("GetAllInActiveEvolentContacts")]
        public IEnumerable<EvolentContact> GetAllInActiveEvolentContacts()
        {
            try
            {
                var evolentContact = _evolentContactRepository.GetAllInActiveEvolentContacts();
                return evolentContact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpGet method to pull EvolentContacts for the given Id
        [HttpGet, Route("GetEvolentContactFor/{evolentContactId:int}")]
        public EvolentContact GetEvolentContactFor(int evolentContactId)
        {
            try
            {
                var evolentContact = _evolentContactRepository.GetEvolentContactFor((evolentContactId));

                return evolentContact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPost method to create new EvolentContacts
        [AcceptVerbs("GET", "POST")]
        [HttpPost]
        [ActionName("CreateEvolentContact")]
        public bool CreateEvolentContact([FromBody]EvolentContact evolentContact)
        {
            try
            {
                var isAdded = _evolentContactRepository.CreateEvolentContact(evolentContact);
                return isAdded;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPut method to Update EvolentContacts
        [AcceptVerbs("GET", "POST")]
        [HttpPut, Route("UpdateEvolentContact")]
        public bool UpdateEvolentContact([FromBody]EvolentContact modifiedEvolentContact)
        {
            try
            {
                var isModified = _evolentContactRepository.ModifyEvolentContactFor(modifiedEvolentContact);

                return isModified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // HttpPut method to Delete EvolentContacts
        [AcceptVerbs("GET", "POST")]
        [HttpPut, Route("DeleteEvolentContact/{id:int}")]
        public bool DeleteEvolentContact(int id)
        {
            try
            {
                var isDeleted = _evolentContactRepository.DeleteEvolentContactFor(id);

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
