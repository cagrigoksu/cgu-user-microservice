using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models.Data;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserDataModel> GetUser(string email)
        {
            var user = _db.Users.
                FirstOrDefaultAsync(x => x.Email == email
                                    && x.IsDeleted == false);

            return await user;
        }

        public UserProfileDataModel GetUserProfile(int userId)
        {
            var profile = _db.UserProfiles.FirstOrDefault(x => x.UserId == userId && x.IsDeleted == false);

            return profile;
        }

        public void AddUser(UserDataModel user)
        {
            user.LogOnDate = DateTime.Now;
            _db.Add(user);
            _db.SaveChanges();
        }

        public void AddUserProfile(UserProfileDataModel userProfile)
        {
            userProfile.LastEditDate = DateTime.Now;
            _db.Add(userProfile);
            _db.SaveChanges();
        }

        public void EditUserProfile(UserProfileDataModel userProfile)
        {
            var data = _db.UserProfiles.First(x => x.UserId == Globals.UserId);

            data.Name = userProfile.Name;
            data.Surname = userProfile.Surname;
            data.PhoneNumber = userProfile.PhoneNumber;
            data.Email = userProfile.Email;
            data.UrlResume = userProfile.UrlResume;
            data.UrlMotivationLetter = userProfile.UrlMotivationLetter;
            data.LastEditDate = DateTime.Now;

            _db.Update(data);
            _db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _db.Users.Find(id);

            user.IsDeleted = true;
            user.DeleteUser = Globals.UserId;
            user.DeleteDate = DateTime.Now;

            _db.Update(user);
            _db.SaveChanges();
        }

        public bool IsUserExist(string email)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);
            return user != null;
        }
    }
}
