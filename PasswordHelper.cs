using System;
using System.Security.Cryptography;

namespace Parks_LabAssignment8
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static byte[] CreatePasswordHash(string password, out byte[] salt)
        {
            salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return deriveBytes.GetBytes(HashSize);
            }
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        {
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] submittedHash = deriveBytes.GetBytes(HashSize);
                return ConstantTimeEquals(submittedHash, storedHash);
            }
        }

        private static bool ConstantTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int difference = 0;
            for (int i = 0; i < a.Length; i++)
                difference |= a[i] ^ b[i];

            return difference == 0;
        }
    }
}
