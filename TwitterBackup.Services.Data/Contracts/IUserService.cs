﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface IUserService
    {
        void Update(EditUserDto editUserDto);

        Task RemoveAsync(UserDto userDto);

        string FindUserIdByUserName(string userName);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<IEnumerable<UserDto>> GetAllActiveUsersAsync();

        Task<UserDto> FindUserByIdAsync(string userId);

        Task<UserDto> FindUserByNameAsync(string userName);

        Task<IEnumerable<UserDto>> SearchUserAsync(string searchString);

        bool UserIsDeleted(string userName);

    }
}
