using System;
using System.Security.Cryptography;

namespace CustomUtilities
{
    public static class PerformHashPassword
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private const int Pbkdf2Iterations = 1000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;
        
        /// <summary>
        /// Perform Hash when passing a clear text password.
        /// </summary>
        /// <param name="password">Cleartext password</param>
        /// <returns>Formatted hashed string password</returns>
        public static string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return string.Format("{0}:{1}:{2}", Pbkdf2Iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Perform Hash when passing a clear text password and a byte table Salt.
        /// </summary>
        /// <param name="password">Cleartext password</param>
        /// <param name="saltByte">Byte table salt</param>
        /// <returns>Formatted hashed string password</returns>
        public static string HashPassword(string password, byte[] saltByte)
        {
            var hash = GetPbkdf2Bytes(password, saltByte, Pbkdf2Iterations, HashByteSize);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Generate a new table byte salt. Use Convert.ToBase64String() method to retreive a salt string value.
        /// </summary>
        /// <returns>Table byte salt</returns>
        public static byte[] GenerateSaltByte()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[SaltByteSize];
            cryptoProvider.GetBytes(saltByte);

            return saltByte;
        }

        /// <summary>
        /// Validate Password using his correct hash from any Data Source (Like SQL Server Database).
        /// </summary>
        /// <param name="clearPassword">Cleartext password to validate</param>
        /// <param name="correctHash">Correct Hash password</param>
        /// <returns>Validation password statut. True or False</returns>
        public static bool ValidatePassword(string clearPassword, string correctHash)
        {
            char[] delimiter = { ':' };
            var split = correctHash.Split(delimiter);
            var iterations = int.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(clearPassword, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Validate Password using his correct hash and passwordSalt from any Data Source (Like SQL Server Database).
        /// </summary>
        /// <param name="clearPassword">Cleartext password to validate</param>
        /// <param name="passwordSalt">Password salt</param>
        /// <param name="passwordHashed">Hashed password</param>
        /// <returns>Validation password statut. True or False</returns>
        public static bool ValidatePassword(string clearPassword, string passwordSalt, string passwordHashed)
        {
            var salt = Convert.FromBase64String(passwordSalt);
            var hash = Convert.FromBase64String(passwordHashed);

            var testHash = GetPbkdf2Bytes(clearPassword, salt, Pbkdf2Iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        // Compare two instances of byte table.
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        // Return Hash password in a byte table
        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
