using System.Security.Cryptography;
using System.Text;

namespace InnoGotchi.Application.Common.Extentions
{
    public static class Extentions
    {
        public static string ToShaHash(this string value) => 
            Convert.ToHexString(SHA512.Create().
            ComputeHash(Encoding.ASCII.GetBytes(value)));

        public static string ToBase64String(this byte[] bytes) => Convert.ToBase64String(bytes);
        public static byte[] ToByteFromBase64(this string baseStr) => Convert.FromBase64String(baseStr);
    }
}
