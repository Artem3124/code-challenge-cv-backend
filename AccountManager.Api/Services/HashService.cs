﻿using AccountManager.Api.Interfaces;
using System.Security.Cryptography;

namespace AccountManager.Api.Services
{
    public class HashService : IHashService
    {
        public string Hash(string value)
        {
            var salt = new byte[16];
            RandomNumberGenerator.Fill(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 100000);

            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string value, string hashValue)
        {
            var hashBytes = Convert.FromBase64String(hashValue);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
