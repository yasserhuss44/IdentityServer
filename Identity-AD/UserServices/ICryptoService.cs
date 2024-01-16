namespace CustomIdentityServer4.UserServices
{
    public interface ICryptoService
    {
        string Decrypt(string input);
        string Encrypt(string input);
    }
}