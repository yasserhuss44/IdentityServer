﻿using Microsoft.AspNetCore.DataProtection;

namespace CustomIdentityServer4.UserServices
{
    public class CryptoService : ICryptoService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "my-very-long-key-of-no-exact-size";

        public CryptoService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }

    }
}
