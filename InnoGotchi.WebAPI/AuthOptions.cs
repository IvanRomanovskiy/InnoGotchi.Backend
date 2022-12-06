using Microsoft.IdentityModel.Tokens;
using System.Text;
using InnoGotchi.WebAPI.Properties;

namespace InnoGotchi.WebAPI
{
    public static class AuthOptions
    {
        public const string ISSUER = "InnoGotchiServer";
        public const string AUDIENCE = "InnoGotchiClient";
        public static string KEY = "";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
