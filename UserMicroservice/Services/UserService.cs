using UserMicroservice.Models.Data;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDataModel> GetUserAsync(string email)
        {
            var user = await _userRepository.GetUserAsync(email);

            return user;
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            return await _userRepository.IsUserExistAsync(email);
        }

        public async Task<UserProfileDataModel> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserProfileAsync(userId);

            return user;
        }

        public void AddUser(UserDataModel user)
        {
            _userRepository.AddUser(user);
        }

        public void AddUserProfile(UserProfileDataModel profile)
        {
            _userRepository.AddUserProfile(profile);
        }

        public void EditUserProfile(UserProfileDataModel userProfile)
        {
            _userRepository.EditUserProfileAsync(userProfile);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUserAsync(id);
        }
    }
}
