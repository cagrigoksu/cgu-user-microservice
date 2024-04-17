using System.Security.Cryptography;
using System.Text;
using UserMicroservice.Services.Interfaces;

namespace UserMicroservice.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly string _pepper = "cgu-jobboard";
        public string GenerateSalt()
        {
            var randomNumber = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            randomNumber.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;
        }

        public string Hasher(string pwd, string salt, int iter)
        {
            if (iter > 0)
            {
                var sha256 = SHA256.Create();
                var pwdSaltPepper = $"{pwd}{salt}{_pepper}";
                var byteValue = Encoding.UTF8.GetBytes(pwdSaltPepper);
                var byteHash = sha256.ComputeHash(byteValue);
                var hashText = Convert.ToBase64String(byteHash);
                return Hasher(hashText, salt, iter-1);
            }

            return pwd;
        }
    }
}
