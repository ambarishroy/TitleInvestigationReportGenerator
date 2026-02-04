using System.Security.Cryptography;

namespace TIR.DocumentService.Common
{
    public static class DocumentHashService
    {
        public static string ComputeHash(Stream stream)
        {
            using var sha= SHA256.Create();
            var hashBytes = sha.ComputeHash(stream);
            return Convert.ToHexString(hashBytes);
        }
    }
}
