using UserMicroservice.Models.Data;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDataModel> GetUserAsync(string email);
        UserProfileDataModel GetUserProfile(int userId);
        void AddUser(UserDataModel user);
        void AddUserProfile(UserProfileDataModel profile);
        void EditUserProfile(UserProfileDataModel userProfile);
        void DeleteUser(int id);
        Task<bool> IsUserExistAsync(string email);
    }
}
