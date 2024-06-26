﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<UserDataModel> GetUserAsync(string email)
        {
            var user = _db.Users.
                FirstOrDefaultAsync(x => x.Email == email
                                    && x.IsDeleted == false);

            return await user;
        }

        public async Task<UserProfileDataModel> GetUserProfileAsync(int userId)
        {
            var profile = _db.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId && x.IsDeleted == false);

            return await profile;
        }

        public void AddUser(UserDataModel user)
        {
            user.LogOnDate = DateTime.Now;
            _db.Add(user);
            _db.SaveChanges();
        }

        public StatusCodeResult AddUserProfile(UserProfileDataModel userProfile)
        {
            try
            {
                userProfile.LastEditDate = DateTime.Now;
                _db.Add(userProfile);
                _db.SaveChanges();
                return new StatusCodeResult(200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }

        }

        public StatusCodeResult EditUserProfile(UserProfileDataModel userProfile)
        {
            try
            {
                var data = _db.UserProfiles.First(x => x.UserId == userProfile.UserId);

                if (data != null)
                {
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

                return new StatusCodeResult(200);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusCodeResult(400);
            }
        }

        public async void DeleteUserAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);

            user.IsDeleted = true;
            user.DeleteUser = Globals.UserId;
            user.DeleteDate = DateTime.Now;

            _db.Update(user);
            _db.SaveChanges();
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user != null;
        }
    }
}
