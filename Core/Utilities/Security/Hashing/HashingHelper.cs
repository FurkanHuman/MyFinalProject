using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string pass, out byte[] passHash, out byte[] passSalt)
        {
            using (var Hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = Hmac.Key;
                passHash = Hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));
            }
        }
        public static bool VerifyPasswordHash(string pass, byte[] passHash, byte[] passSalt)
        {
            using (var Hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                var computeHash = Hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}

