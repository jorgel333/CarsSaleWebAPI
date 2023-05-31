namespace CarSalesWebAPI.Services.SecurityServices.CryptographyService
{
    public interface ICryptography
    {
        string EncryptPassword(string password);
        bool VerifyPassword(string password, string encryptPassword);
    }
}
