using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models.Data;

namespace UserMicroservice
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserDataModel> Users { get; set; }

        public DbSet<UserProfileDataModel> UserProfiles { get; set; }

    }
}
