﻿namespace VegiJ.DataAccess
{
    using System;
    using System.Security.Cryptography;

    public static class PasswordHash
    {
        //private const int SaltSize = 16, HashSize = 20, HashIter = 1000;
        //private readonly byte[] _salt, _hash;

        //public PasswordHash(string password)
        //{
        //    new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
        //    _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        //}

        //public override string ToString()
        //{
        //    byte[] hashBytes = new byte[SaltSize + HashSize];
        //    Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
        //    Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
        //    return hashBytes.ToString();
        //}

        private const int SaltSize = 24;
        private const int HashSize = 24;
        private const int HashIter = 10101; //6969

        public static string EncryptPassword(string password, string salt, int iterations = HashIter)
        {
            byte[] saltArray = ConvertToByteArray(salt);
            Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, saltArray);
            hashGenerator.IterationCount = iterations;
            return ConvertToString(hashGenerator.GetBytes(HashSize));
        }

        public static string GenerateSalt(int saltSize = SaltSize)
        {
            // matbe make another method that return the salt as string and this as byte?
            RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            saltGenerator.GetBytes(salt);

            return ConvertToString(salt);
        }

        public static bool ComparePasswords(string password, string passwordSalt, string passwordHash)
        {
            byte[] computedPasswordHash = ConvertToByteArray(EncryptPassword(password, passwordSalt));
            byte[] userPasswordHash = ConvertToByteArray(passwordHash);
            return AreHashesEqual(computedPasswordHash, userPasswordHash);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLength; i++)
            {
                xor |= firstHash[i] ^ secondHash[i];
            }

            return 0 == xor;
        }

        private static byte[] ConvertToByteArray(string input)
        {
            return Convert.FromBase64String(input);
        }

        private static string ConvertToString(byte[] array)
        {
            return Convert.ToBase64String(array);
        }
    }
}