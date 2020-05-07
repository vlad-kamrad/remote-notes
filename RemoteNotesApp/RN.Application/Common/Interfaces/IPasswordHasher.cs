namespace RN.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        string EncodePassword(string password);
        bool ValidatePassword(string password, string hash);
    }
}
