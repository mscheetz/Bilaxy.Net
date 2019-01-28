// -----------------------------------------------------------------------------
// <copyright file="ErrorResponse" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 7:49:04 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using Newtonsoft.Json;

    #endregion Usings

    public class ErrorResponse
    {
        #region Properties

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        #endregion Properties
    }
}