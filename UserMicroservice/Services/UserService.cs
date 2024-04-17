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

        public async Task<UserDataModel> GetUser(string email)
        {
            var user = _userRepository.GetUser(email);

            return await user;
        }

        public bool IsUserExist(string email)
        {
            return _userRepository.IsUserExist(email);
        }

        public UserProfileDataModel GetUserProfile(int userId)
        {
            var user = _userRepository.GetUserProfile(userId);

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
            _userRepository.EditUserProfile(userProfile);
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
