using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface IUserDbService
    {
        void Update(UserDto user);

        void Delete(UserDto user);
    }
}
