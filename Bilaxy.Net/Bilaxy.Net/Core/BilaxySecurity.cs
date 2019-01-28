using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bilaxy.Net.Core
{
    public static class BilaxySecurity
    {
        public static string _apiKey = string.Empty;
        public static string _apiSecret = string.Empty;

        public static string GetSignature(string key, string secret)
        {
            _apiKey = key;
            _apiSecret = secret;

            var parms = new SortedDictionary<string, object>();

            var stringifiedMessage = Stringify(parms);

            var signature = SignMessage(stringifiedMessage);

            return signature;
        }

        public static string PostSignature(string key, string secret, SortedDictionary<string, object> parms)
        {
            _apiKey = key;
            _apiSecret = secret;
            
            var stringifiedMessage = Stringify(parms);

            var signature = SignMessage(stringifiedMessage);

            return signature;
        }

        private static string Stringify(SortedDictionary<string, object> parms)
        {
            var stringify = string.Empty;

            var keys = parms.Keys.ToList();
            keys.Sort();

            foreach (var key in keys)
            {
                stringify += $"{key}={parms[key]},";
            }

            stringify += $"key={_apiKey},";
            stringify += $"secret={_apiSecret}";
            stringify += @"&";

            return stringify;
        }

        private static string SignMessage(string message)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(message));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
