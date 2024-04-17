namespace UserMicroservice.Models
{
    public class LogonModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConf  { get; set; }
        public bool CompanyUser { get; set; }
    }
}
