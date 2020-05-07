namespace RN.Infrastructure.Services
{
    public class BCryptPasswordHasher : Application.Common.Interfaces.IPasswordHasher
    {
        public string EncodePassword(string password) => 
            BCrypt.Net.BCrypt.HashPassword(password);
        public bool ValidatePassword(string password, string hash) => 
            BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
