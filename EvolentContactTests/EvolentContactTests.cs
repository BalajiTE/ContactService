using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolentContacts.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using EvolentContacts.Controllers;
using EvolentContacts.Repositories;
using Moq;
using System.Data.Entity;
using System.Reflection;

namespace EvolentContactTests
{
    [TestClass]
    public class EvolentContactTests
    {
        #region private variables

        private readonly EvolentContact _evolentContact;
        private readonly ValidationContext _evolentContactContext;
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        #endregion

        #region Constructror
        public EvolentContactTests()
        {
            _evolentContact = new EvolentContact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@gmail.com",
                Phone = "1111111111",
                Status = true,
                CreatedDate = DateTime.Now
            };

            _evolentContactContext = new ValidationContext(_evolentContact, null, null);
        }

        #endregion

        #region entity validation test cases

        [TestMethod]
        public void FailedFirstNameIsRequiredTest()
        {
            _evolentContact.FirstName = string.Empty;

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext,_validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("First Name is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedFirstNameLengthExceededTest()
        {
            _evolentContact.FirstName = _evolentContact.FirstName + "123456789012345678901234567890123456789012345678901234567890";

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults,validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("First Name cannot Exceed 50 Character length", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedLastNameIsRequiredTest()
        {
            _evolentContact.LastName = string.Empty;

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Last Name is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedLastNameLengthExceededTest()
        {
            _evolentContact.LastName = _evolentContact.LastName + "123456789012345678901234567890123456789012345678901234567890";

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Last Name cannot Exceed 50 Character length", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedFormattedEmailTest()
        {
            _evolentContact.Email = "abc.com";

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Email address", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedInvalidPhoeNumberTest()
        {
            _evolentContact.Phone = "111222333a";

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Phone Number", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedPhoeNumberLengthTest()
        {
            _evolentContact.Phone = "11122233334";

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Invalid Phone Number", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void FailedPhoneOrEmailDetailsTest()
        {
            _evolentContact.Phone = null;
            _evolentContact.Email = null;

            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsFalse(validationResult);
            Assert.AreEqual(1, _validationResults.Count);
            Assert.AreEqual("Either Email or Phone Data is Required", _validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void PassedEvolntContactTest()
        {
            var validationResult = Validator.TryValidateObject(_evolentContact, _evolentContactContext, _validationResults, validateAllProperties: true);

            Assert.IsTrue(validationResult);
        }

        #endregion

        #region CRUD operation test cases

        [TestMethod]
        public void GetAllEvolentContactsTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.GetAllEvolentContacts();

                Assert.AreEqual(2, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetAllActiveEvolentContactsTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.GetAllActiveEvolentContacts();

                Assert.AreEqual(2, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetAllInActiveEvolentContactsTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.GetAllInActiveEvolentContacts();

                Assert.AreEqual(0, result.Count());

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void GetEvolentContactsForTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.GetEvolentContactFor(1);

                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.ID);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void CreateNewEvolentContactTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();
            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                var newEvolentContact = new EvolentContact() {
                                            FirstName = "defTest",
                                            LastName = "defTest",
                                            Email = "defTest@gmail.com",
                                            Phone = "2222222222",
                                            Status = true,
                                            CreatedDate = DateTime.Now
                                        };
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.CreateEvolentContact(newEvolentContact);

                Assert.IsTrue(result);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void UpdateEvolentContactTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();
            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var selectedEvolentContact = controller.GetEvolentContactFor(1);

                // Updated Phone number with new number
                selectedEvolentContact.Phone = "3333333333";

                var result = controller.UpdateEvolentContact(selectedEvolentContact);

                Assert.IsTrue(result);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void DeleteEvolentContactTest()
        {
            var mockContext = new Mock<EvolentContactsContext>();

            var mockSet = MockSet(GetEvolventContactsMock());

            try
            {
                //Setting up the mockSet to mockContext
                mockContext.Setup(c => c.EvolentContact).Returns(mockSet.Object);

                var repository = new EvolentContactRepository(mockContext.Object);
                var controller = new EvolentContactController(repository);

                var result = controller.DeleteEvolentContact(1);

                Assert.IsTrue(result);

            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        #endregion  

        #region static private helper method(s)

        IQueryable<EvolentContact> GetEvolventContactsMock()
        {
            var evolentContactsMock = new List<EvolentContact>
                {
                    new EvolentContact() { ID = 1, FirstName = "abcTest", LastName = "abcTest", Email = "abcTest@gmail.com",
                                          Phone = "1111111111", Status = true, CreatedDate = DateTime.Now},
                    new EvolentContact() { ID = 2, FirstName = "defTest", LastName = "defTest", Email = "defTest@gmail.com",
                                          Phone = "2222222222", Status = true, CreatedDate = DateTime.Now}
                }.AsQueryable();

            return evolentContactsMock;
        }
        
        private static Mock<DbSet<EvolentContact>> MockSet(IQueryable<EvolentContact> evolentContactsMock)
        {
            var mockSet = new Mock<DbSet<EvolentContact>>();
            mockSet.As<IQueryable<EvolentContact>>().Setup(m => m.Provider).Returns(evolentContactsMock.Provider);
            mockSet.As<IQueryable<EvolentContact>>().Setup(m => m.Expression).Returns(evolentContactsMock.Expression);
            mockSet.As<IQueryable<EvolentContact>>().Setup(m => m.ElementType).Returns(evolentContactsMock.ElementType);
            mockSet.As<IQueryable<EvolentContact>>().Setup(m => m.GetEnumerator()).Returns(evolentContactsMock.GetEnumerator());
            return mockSet;
        }

        #endregion
    }
}
