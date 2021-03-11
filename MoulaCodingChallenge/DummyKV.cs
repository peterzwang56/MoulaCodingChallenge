using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MoulaCodingChallenge
{
    /// <summary>
    /// this is the dummy class to simulate Keyvault.
    /// </summary>
    public static class DummyKV
    {
        private static RsaSecurityKey _key;

        static DummyKV()
        {
            _key = new RsaSecurityKey(RSA.Create(2048));
        }

        public static RsaSecurityKey Key { get { return _key; } }
    }
}
