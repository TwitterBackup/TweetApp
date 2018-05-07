using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwitterBackup.Data.Repository;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data;

namespace TwitterBackup.Services.Data.Tests.TweetServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void Create_TweetDbService_When_Called_With_Valid_Parameters()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            var TweetService = new TweetService(stubTweetRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweetRepository.Object, stubTweetHashtag.Object, stubHashtag.Object);

            Assert.IsNotNull(TweetService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeTweet()
        {
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(null, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweetRepository.Object, stubTweetHashtag.Object, stubHashtag.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_UnitOfWork()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(stubTweetRepository.Object, null, stubMappingProvider.Object, stubUserTweetRepository.Object, stubTweetHashtag.Object, stubHashtag.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_MappingProvider()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(stubTweetRepository.Object, stubUnitOfWork.Object, null, stubUserTweetRepository.Object, stubTweetHashtag.Object, stubHashtag.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeUserTweet()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(stubTweetRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, null, stubTweetHashtag.Object, stubHashtag.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeTweetHashtag()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubHashtag = new Mock<IRepository<Hashtag>>();

            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(stubTweetRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweetRepository.Object, null, stubHashtag.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_RepositoryOfTypeHashtag()
        {
            var stubTweetRepository = new Mock<IRepository<Tweet>>();
            var stubUnitOfWork = new Mock<IUnitOfWork>();
            var stubMappingProvider = new Mock<IMappingProvider>();
            var stubUserTweetRepository = new Mock<IRepository<UserTweet>>();
            var stubTweetHashtag = new Mock<IRepository<TweetHashtag>>();
        
            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(stubTweetRepository.Object, stubUnitOfWork.Object, stubMappingProvider.Object, stubUserTweetRepository.Object, stubTweetHashtag.Object, null));
        }

    }
}
