using System.Security.Cryptography;
using System.Text;

namespace CarSalesWebAPI.Services.SecurityServices.CryptographyService
{
    public class Cryptography : ICryptography
    {
        private readonly HashAlgorithm _algorithm;

        public Cryptography()
        {
            _algorithm = SHA256.Create();
        }
        public string EncryptPassword(string password)
        {
            var passwordEncrypted = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            var passwordStringBuilder = new StringBuilder();
            foreach (var caractere in passwordEncrypted)
            {
                passwordStringBuilder.Append(caractere.ToString("X2"));
            }

            return passwordStringBuilder.ToString();
        }

        public bool VerifyPassword(string password, string encryptPassword)
        {
            return EncryptPassword(password) == encryptPassword;
        }
    }
}
