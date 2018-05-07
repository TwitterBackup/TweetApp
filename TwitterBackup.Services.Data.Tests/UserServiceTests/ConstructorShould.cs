using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwitterBackup.Data.Repository;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data;

namespace TwitterBackup.Services.Data.Tests.UserServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void Create_UserService_When_Called_With_Valid_Parameters()
        {
            var stubUserRepository = new Mock<IRepository<ApplicationUser>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();

            var UserService = new UserService(stubUserRepository.Object, stubMappingProvider.Object, stubUnitOfWork.Object);

            Assert.IsNotNull(UserService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeUser()
        {
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new UserService(null, stubMappingProvider.Object, stubUnitOfWork.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_UnitOfWork()
        {
            var stubUserRepository = new Mock<IRepository<ApplicationUser>>();
            
            var stubMappingProvider = new Mock<IMappingProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new UserService(stubUserRepository.Object, stubMappingProvider.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_MappingProvider()
        {
            var stubUserRepository = new Mock<IRepository<ApplicationUser>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();

            Assert.ThrowsException<ArgumentNullException>(() => new UserService(stubUserRepository.Object, null, stubUnitOfWork.Object));
        }

    }
}
