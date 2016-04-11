namespace VegiJ.DataAccess
{
    using System;
    using System.Security.Cryptography;

    public sealed class PasswordHash
    {
        private const int SaltSize = 16, HashSize = 20, HashIter = 1000;
        private readonly byte[] _salt, _hash;

        public PasswordHash(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }

        public override string ToString()
        {
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes.ToString();
        }
    }
}
