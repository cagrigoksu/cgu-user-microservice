using UserMicroservice.Models.Data;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDataModel> GetUserAsync(string email);
        Task<UserProfileDataModel> GetUserProfileAsync(int userId);
        void AddUser(UserDataModel user);
        void AddUserProfile(UserProfileDataModel profile);
        void EditUserProfileAsync(UserProfileDataModel userProfile);
        void DeleteUserAsync(int id);
        Task<bool> IsUserExistAsync(string email);
    }
}
