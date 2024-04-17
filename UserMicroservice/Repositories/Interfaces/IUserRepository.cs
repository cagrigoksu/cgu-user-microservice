using UserMicroservice.Models.Data;

namespace UserMicroservice.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDataModel> GetUser(string email);
        UserProfileDataModel GetUserProfile(int userId);
        void AddUser(UserDataModel user);
        void AddUserProfile(UserProfileDataModel profile);
        void EditUserProfile(UserProfileDataModel userProfile);
        void DeleteUser(int id);
        bool IsUserExist(string email);
    }
}
