using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface IUserService
    {
        void Update(UserDto user);

        void Delete(UserDto user);

        string FindUserIdByUserName(string userName);

    }
}
