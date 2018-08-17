using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contacts.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Contacts.Controllers;
using Contacts.Repositories;
using Moq;
using System.Data.Entity;
using System.Reflection;

namespace ContactsTests
{
    [TestClass]
    public class ContactsTests
    {
        #region private variables

        private readonly Contact _Contacts;
        private readonly ValidationContext _ContactsContext;
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        #endregion

        #region Constructror
        public ContactsTests()
        {
            _Contacts = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@gmail.com",
                Phone = "1111111111",
                Status = true,
                CreatedDate = DateTime.Now
            };

            _ContactsContext = new ValidationContext(_Contacts, null, null);
        }

        #endregion

        #region entity validation test cases

        [TestMethod]
        public void FailedFirstNameIsRequiredTest()
        {
            _Contacts.FirstName = string.Empty;

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext,_validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("First Name is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedFirstNameLengthExceededTest()
        {
            _Contacts.FirstName = _Contacts.FirstName + "123456789012345678901234567890123456789012345678901234567890";

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults,validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("First Name cannot Exceed 50 Character length", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedLastNameIsRequiredTest()
        {
            _Contacts.LastName = string.Empty;

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Last Name is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedLastNameLengthExceededTest()
        {
            _Contacts.LastName = _Contacts.LastName + "123456789012345678901234567890123456789012345678901234567890";

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Last Name cannot Exceed 50 Character length", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedFormattedEmailTest()
        {
            _Contacts.Email = "abc.com";

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Email address", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedInvalidPhoeNumberTest()
        {
            _Contacts.Phone = "111222333a";

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Phone Number", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedPhoeNumberLengthTest()
        {
            _Contacts.Phone = "11122233334";

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Phone Number", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedPhoneOrEmailDetailsTest()
        {
            _Contacts.Phone = null;
            _Contacts.Email = null;

            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Either Email or Phone Data is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void PassedEvolntContactTest()
        {
            var validationResult = Validator.TryValidateObject(_Contacts, _ContactsContext, _validationResults, validateAllProperties: true);

            Assert.IsTrue(validationResult);
        }

        #endregion

        #region CRUD operation test cases

        [TestMethod]
        public void GetAllContactssTest()
        {
            var mockContext = new Mock<ContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.GetAllContacts();

                Assert.AreEqual(2, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetAllActiveContactssTest()
        {
            var mockContext = new Mock<ContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.GetAllActiveContacts();

                Assert.AreEqual(2, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetAllInActiveContactssTest()
        {
            var mockContext = new Mock<ContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.GetAllInActiveContacts();

                Assert.AreEqual(0, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetContactssForTest()
        {
            var mockContext = new Mock<ContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.GetContactFor(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ID);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void CreateNewContactsTest()
        {
            var mockContext = new Mock<ContactsContext>();
            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                var newContacts = new Contact() {
                                            FirstName = "defTest",
                                            LastName = "defTest",
                                            Email = "defTest@gmail.com",
                                            Phone = "2222222222",
                                            Status = true,
                                            CreatedDate = DateTime.Now
                                        };
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.CreateContact(newContacts);

                Assert.IsTrue(result);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void UpdateContactsTest()
        {
            var mockContext = new Mock<ContactsContext>();
            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var selectedContacts = controller.GetContactFor(1);

                // Updated Phone number with new number
                selectedContacts.Phone = "3333333333";

                var result = controller.UpdateContact(selectedContacts);

                Assert.IsTrue(result);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void DeleteContactsTest()
        {
            var mockContext = new Mock<ContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.Contact).Returns(mockSet.Object);

                var repository = new ContactRepository(mockContext.Object);
                var controller = new ContactController(repository);

                var result = controller.DeleteContact(1);

                Assert.IsTrue(result);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        #endregion  

        #region static private helper method(s)

        IQueryable<Contact> GetEvolventContactsMock()
        {
            var ContactssMock = new List<Contact>
                {
                    new Contact() { ID = 1, FirstName = "abcTest", LastName = "abcTest", Email = "abcTest@gmail.com",
                                          Phone = "1111111111", Status = true, CreatedDate = DateTime.Now},
                    new Contact() { ID = 2, FirstName = "defTest", LastName = "defTest", Email = "defTest@gmail.com",
                                          Phone = "2222222222", Status = true, CreatedDate = DateTime.Now}
                }.AsQueryable();

            return ContactssMock;
        }
        
        private static Mock<DbSet<Contact>> MockSet(IQueryable<Contact> ContactssMock)
        {
            var mockSet = new Mock<DbSet<Contact>>();
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(ContactssMock.Provider);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(ContactssMock.Expression);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(ContactssMock.ElementType);
            mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(ContactssMock.GetEnumerator());
            return mockSet;
        }

        #endregion
    }
}
