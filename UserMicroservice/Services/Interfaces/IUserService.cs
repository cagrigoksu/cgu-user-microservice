using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Models.Data;

namespace UserMicroservice.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDataModel> GetUserAsync(string email);
        Task<bool> IsUserExistAsync(string email);
        Task<UserProfileDataModel> GetUserProfileAsync(int userId);
        void AddUser(UserDataModel user);
        StatusCodeResult AddUserProfile(UserProfileDataModel profile);
        StatusCodeResult EditUserProfile(UserProfileDataModel userProfile);
        void DeleteUser(int id);

    }
}
