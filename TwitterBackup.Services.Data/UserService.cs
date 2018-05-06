using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.User;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class UserService : IUserService

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository, IMappingProvider mappingProvider, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void Update(EditUserDto editUserDto)
        {
            var userForEdit = userRepository.SingleOrDefault(u => u.UserName == editUserDto.UserName);

            try
            {
                userForEdit.FirstName = editUserDto.FirstName;
                userForEdit.LastName = editUserDto.LastName;
                userForEdit.Email = editUserDto.Email;
                userForEdit.ModifiedOn = DateTime.Now;

                userForEdit.UserName = userForEdit.UserName; //in case of overposting attack

                unitOfWork.CompleteWorkAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public async Task RemoveAsync(UserDto userDto)
        {
            var user = this.userRepository.SingleOrDefault(u => u.UserName == userDto.UserName);
            user.IsDeleted = true;
            user.DeletedOn = DateTime.Now;
            await this.unitOfWork.CompleteWorkAsync();
        }

        public string FindUserIdByUserName(string userName)
        {
            var user = userRepository.Find(x => x.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return user.Id;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            var userDtos = mappingProvider.ProjectTo<ApplicationUser, UserDto>(users);
            return userDtos;
        }

        public async Task<IEnumerable<UserDto>> GetAllActiveUsersAsync()
        {
            var users = await userRepository.FindAsync(user => user.IsDeleted == false);
            var activeUserDtos = mappingProvider.ProjectTo<ApplicationUser, UserDto>(users);
            return activeUserDtos;
        }

        public async Task<IEnumerable<UserDto>> SearchUserAsync(string searchString)
        {
            var users = await userRepository.FindAsync(user => user.Id == searchString || user.FirstName.Contains(searchString) || user.LastName.Contains(searchString) || user.UserName.Contains(searchString) || user.Email.Contains(searchString));

            var userDtos = mappingProvider.ProjectTo<ApplicationUser, UserDto>(users);
            return userDtos;
        }

        public async Task<UserDto> FindUserByNameAsync(string userName)
        {
            var activeUsers = await userRepository.FindAsync(user => user.IsDeleted == false && user.UserName == userName);
            var userDto = mappingProvider.MapTo<UserDto>(activeUsers.SingleOrDefault());
            return userDto;
        }

        public async Task<UserDto> FindUserByIdAsync(string userId)
        {
            var activeUsers = await userRepository.FindAsync(user => user.IsDeleted == false && user.Id == userId);
            var userDto = mappingProvider.MapTo<UserDto>(activeUsers.SingleOrDefault());
            return userDto;
        }

        public bool UserIsDeleted(string userName)
        {
            var applicationUser = userRepository.SingleOrDefault(user => user.UserName == userName);
            return applicationUser.IsDeleted;
        }

    }
}
