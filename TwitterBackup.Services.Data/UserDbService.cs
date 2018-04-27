using System;
using System.Linq;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.User;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class UserDbService : IUserDbService
    {
        private readonly IRepository<ApplicationUser> userRepository;

        public UserDbService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void Update(UserDto user)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserDto user)
        {
            throw new NotImplementedException();
        }

        public string FindUserIdByUserName(string userName)
        {
            var user = userRepository.Find(x=>x.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return user.Id;
        }
    }
}
