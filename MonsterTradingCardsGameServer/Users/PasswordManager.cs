using System;
using System.Security.Cryptography;

namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// Creates password hash with salt value and compares passwords
    /// </summary>
    public static class PasswordManager
    {
        /// <summary>
        /// Compares two passwords
        /// </summary>
        /// <param name="storedPw">the stored password</param>
        /// <param name="userPw">password provided of user</param>
        /// <returns>if passwords are equal</returns>
        public static bool ComparePasswords(string storedPw, string userPw)
        {
            /* Extract the bytes */
            var hashBytes = Convert.FromBase64String(storedPw);
            /* Get the salt */
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(userPw, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        /// <summary>
        /// Creates hash from password
        /// </summary>
        /// <param name="password">the clear text password</param>
        /// <returns>the password hash</returns>
        public static string CreatePwHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}