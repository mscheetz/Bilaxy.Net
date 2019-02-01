// -----------------------------------------------------------------------------
// <copyright file="RepositoryBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="1/27/2019 2:48:53 PM" />
// -----------------------------------------------------------------------------

namespace Bilaxy.Net.Repository
{
    using Bilaxy.Net.Contracts;
    using Bilaxy.Net.Core;
    using Bilaxy.Net.Interface;
    using DateTimeHelpers;
    //using RESTApiAccess;
    //using RESTApiAccess.Interface;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class RepositoryBase
    {
        #region Properties

        private IRESTRepository _restRepo;
        private string baseUrl = "https://api.bilaxy.com";
        private ApiCredentials _apiCreds;
        private DateTimeHelper _dtHelper;

        #endregion Properties

        public RepositoryBase()
        {
            LoadBase();
        }

        public RepositoryBase(ApiCredentials apiCredentials)
        {
            _apiCreds = apiCredentials;
            LoadBase();
        }

        private void LoadBase()
        {
            _restRepo = new RESTRepository();
            _dtHelper = new DateTimeHelper();
        }

        public void SetApiKey(ApiCredentials apiCredentials)
        {
            _apiCreds = apiCredentials;
        }

        /// <summary>
        /// Initiate a Get request
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="endpoint">Endpoint of request</param>
        /// <param name="parms">Parameters to pass</param>
        /// <param name="secure">Secure endpoint?</param>
        /// <returns>Object from response</returns>
        public async Task<T> GetRequest<T>(string endpoint, SortedDictionary<string, object> parms, bool secure = false)
        {
            var queryString = BilaxyHelper.ParmsToQueryString(parms);

            if(secure)
            {
                var signature = GetSignature(parms);
                if (!string.IsNullOrEmpty(queryString))
                    queryString += @"&";

                queryString += $@"key={_apiCreds.ApiKey}&sign={signature}";
            }

            endpoint += $@"{queryString}";

            return await OnGetRequest<T>(endpoint);
        }

        /// <summary>
        /// Initiate a Get request
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="endpoint">Endpoint of request</param>
        /// <param name="secure">Secure endpoint?</param>
        /// <returns>Object from response</returns>
        public async Task<T> GetRequest<T>(string endpoint, bool secure = false)
        {
            if (secure)
            {
                var signature = GetSignature();

                endpoint += $@"?key={_apiCreds.ApiKey}&sign={signature}";
            }

            return await OnGetRequest<T>(endpoint);
        }

        /// <summary>
        /// Initiate a Get request
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="endpoint">Endpoint of request</param>
        /// <returns>Object from response</returns>
        private async Task<T> OnGetRequest<T>(string endpoint)
        {
            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<BilaxyResponse<T>>(url);

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Initiate a Get request
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="endpoint">Endpoint of request</param>
        /// <returns>Object from response</returns>
        public async Task<T> GetRequest<T>(string endpoint)
        {
            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<BilaxyResponse<T>>(url);

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Initiate a Post request
        /// </summary>
        /// <typeparam name="T">Object to return</typeparam>
        /// <param name="endpoint">Endpoint of request</param>
        /// <param name="body">Request body data</param>
        /// <returns>Object from response</returns>
        public async Task<T> PostRequest<T>(string endpoint, SortedDictionary<string, object> body)
        {
            var signature = GetSignature(body);
            body.Add("key", _apiCreds.ApiKey);
            body.Add("sign", signature);

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.PostApi<BilaxyResponse<T>, SortedDictionary<string, object>>(url, body);

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a signature when no parameters passed
        /// </summary>
        /// <returns>signature string</returns>
        private string GetSignature()
        {
            return BilaxySecurity.GetSignature(_apiCreds.ApiKey, _apiCreds.ApiSecret);
        }

        /// <summary>
        /// Get a signature when parameters passed
        /// </summary>
        /// <param name="parms">Parameters passed to request</param>
        /// <returns>signature string</returns>
        private string GetSignature(SortedDictionary<string, object> parms)
        {
            return BilaxySecurity.PostSignature(_apiCreds.ApiKey, _apiCreds.ApiSecret, parms);
        }
    }
}