using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwitterBackup.Data.Repository;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data;

namespace UnitTestProject1.TweeterServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void Create_TweeterDbService_When_Called_With_Valid_Parameters()
        {
            var stubTweeterRepository = new Mock<EntityFrameworkRepository<Tweeter>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweeterRepository = new Mock<IRepository<UserTweeter>>();

            var tweeterService = new TweeterService(stubTweeterRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweeterRepository.Object);

            Assert.IsNotNull(tweeterService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeTweeter()
        {
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweeterRepository = new Mock<IRepository<UserTweeter>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(null, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweeterRepository.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_UnitOfWork()
        {
            var stubTweeterRepository = new Mock<EntityFrameworkRepository<Tweeter>>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweeterRepository = new Mock<IRepository<UserTweeter>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(stubTweeterRepository.Object, null, stubMappingProvider.Object, stubUserTweeterRepository.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_MappingProvider()
        {
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubTweeterRepository = new Mock<EntityFrameworkRepository<Tweeter>>();
            var stubUserTweeterRepository = new Mock<IRepository<UserTweeter>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(stubTweeterRepository.Object, stubUnitOfWork.Object, null, stubUserTweeterRepository.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeUserTweeter()
        {
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubTweeterRepository = new Mock<EntityFrameworkRepository<Tweeter>>();
            var stubMappingProvider = new Mock<IMappingProvider>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(stubTweeterRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, null));
        }

    }
}
