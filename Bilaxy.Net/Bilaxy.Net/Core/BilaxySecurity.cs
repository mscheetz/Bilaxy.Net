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
            var addIt = parms.Count > 0 ? true : false;
            parms.Add("key", _apiKey);
            parms.Add("secret", _apiSecret);
            var keys = parms.Keys.ToList();
            keys.Sort();

            foreach (var key in keys)
            {
                if (!string.IsNullOrEmpty(stringify))
                    stringify += "&";

                stringify += $"{key}={parms[key]}";
            }

            //stringify += $"key={_apiKey}, ";
            //stringify += $"secret={_apiSecret}";
            //if(addIt)
            //    stringify += @"&";

            return stringify;
        }
        
        private static string SignMessage(string message)
        {
            var msgBytes = Encoding.UTF8.GetBytes(message);

            using (var sha = new SHA1CryptoServiceProvider())
            {
                var hash = sha.ComputeHash(msgBytes);

                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
