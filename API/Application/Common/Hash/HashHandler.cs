using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Hash
{
    public class HashHandler : IHashHandler
    {
        public string Hash(string value)
        {
            var inputBytes = Encoding.UTF8.GetBytes(value);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }
    }
}
