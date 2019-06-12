using System;

namespace CustomUtilities
{
    public static class TemporaryPassword
    {
        /// <summary>
        /// Generate a temporary password
        /// </summary>
        /// <returns>String contains temporary password</returns>
        public static string Generate()
        {
            return Guid.NewGuid().ToString("N").ToLower()
                .Replace("1", "").Replace("o", "").Replace("0", "")
                .Substring(0, 10);
        }

        /// <summary>
        /// Generate a temporary password for a specifique length
        /// </summary>
        /// <param name="minLength">min length password</param>
        /// <param name="maxLength">max length password</param>
        /// <returns>string password</returns>
        public static string Generate(int minLength, int maxLength)
        {
            return Guid.NewGuid().ToString("N").ToLower()
                .Replace("1", "").Replace("o", "").Replace("0", "")
                .Substring(minLength, maxLength);
        }
    }
}
