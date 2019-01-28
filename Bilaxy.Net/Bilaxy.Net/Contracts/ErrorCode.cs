// -----------------------------------------------------------------------------
// <copyright file="ErrorCode" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 7:48:52 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Contracts
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class ErrorCode
    {
        #region Properties

        public int Code { get; set; }
        public string Description { get; set; }

        #endregion Properties

        public ErrorCode(int code, string description)
        {
            Code = code;
            Description = description;
        }
    }

    public static class ErrorCodes
    {
        /// <summary>
        /// Get all error codes
        /// </summary>
        /// <returns>Collection of ErrorCodes</returns>
        public static List<ErrorCode> Get()
        {
            var errors = new List<ErrorCode>();
            errors.Add(new ErrorCode(101, @"The required parameters cannot be empty"));
            errors.Add(new ErrorCode(102, @"API key dose not exist"));
            errors.Add(new ErrorCode(103, @"API is no longer used"));
            errors.Add(new ErrorCode(104, @"Permissions closed"));
            errors.Add(new ErrorCode(105, @"Insufficient authority"));
            errors.Add(new ErrorCode(106, @"Signature mismatch"));
            errors.Add(new ErrorCode(201, @"The asset does not exist"));
            errors.Add(new ErrorCode(202, @"The asset cannot be deposit or withdraw"));
            errors.Add(new ErrorCode(203, @"The asset is not yet allocated to the wallet address"));
            errors.Add(new ErrorCode(204, @"Failed to cancel the order"));
            errors.Add(new ErrorCode(205, @"The transaction amount must not be less than 0.0001"));
            errors.Add(new ErrorCode(206, @"The transaction price must not be less than 0.0001"));
            errors.Add(new ErrorCode(-100, @"The transaction is lock"));
            errors.Add(new ErrorCode(208, @"Insufficient base currency balance"));
            errors.Add(new ErrorCode(209, @"The transaction password is error"));
            errors.Add(new ErrorCode(210, @"The transaction price is not within the limit price"));
            errors.Add(new ErrorCode(-4, @"Insufficient currency balance"));
            errors.Add(new ErrorCode(212, @"The maximum amount of the transaction is limited"));
            errors.Add(new ErrorCode(213, @"The minimum total amount of the transaction is limited"));
            errors.Add(new ErrorCode(401, @"Illegal parameter"));
            errors.Add(new ErrorCode(402, @"System error"));

            return errors;
        }
    }
}