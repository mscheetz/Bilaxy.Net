// -----------------------------------------------------------------------------
// <copyright file="BilaxyHelper" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:51:11 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Core
{
    using Bilaxy.Net.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion Usings

    public static class BilaxyHelper
    {
        /// <summary>
        /// Convert a Dictionary to a query string
        /// </summary>
        /// <param name="parms">Dictionary of paramters</param>
        /// <returns>string of all keys and values as a query string</returns>
        public static string ParmsToQueryString(Dictionary<string, object> parms)
        {
            var queryString = string.Empty;

            foreach (var kvp in parms)
            {
                var prefix = string.IsNullOrEmpty(queryString) ? "?" : "&";
                queryString += $@"{prefix}{kvp.Key}={kvp.Value}";
            }

            return queryString;
        }


        public static Asset GetAsset(string pair)
        {
            return Assets.Get()
                        .Where(a => a.Pair.Equals(pair) || a.DashedPair.Equals(pair))
                        .FirstOrDefault();
        }

        public static Asset GetAsset(int pairId)
        {
            return Assets.Get()
                        .Where(a => a.AssetId == pairId)
                        .FirstOrDefault();
        }

        public static int GetId(string pair)
        {
            var pairId = Assets.Get()
                                .Where(a => a.Pair.Equals(pair) || a.DashedPair.Equals(pair))
                                .Select(a => a.AssetId).FirstOrDefault();
            return pairId;
        }
    }
}