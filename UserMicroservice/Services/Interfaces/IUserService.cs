using UserMicroservice.Models.Data;

namespace UserMicroservice.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDataModel> GetUser(string email);
        bool IsUserExist(string email);
        UserProfileDataModel GetUserProfile(int userId);
        void AddUser(UserDataModel user);
        void AddUserProfile(UserProfileDataModel profile);
        void EditUserProfile(UserProfileDataModel userProfile);
        void DeleteUser(int id);

    }
}
