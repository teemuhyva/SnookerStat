using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SnookerStat.Model
{
    public class PasswordHash
    {
        public static string CreateSalt(int size)
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateSHA256Hash(String input, String salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256hashstring = SHA256.Create();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public static String ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static bool VerifyPassword(String password, string passwordSalt, string passwordHash)
        {
            string computedHash = GenerateSHA256Hash(password, passwordSalt);
            return AreHashesEqual(computedHash, passwordHash);
        }

        //Length constant verification - prevents timing attack
        private static bool AreHashesEqual(string firstHash, string secondHash)
        {
            int minHashLenght = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLenght; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
