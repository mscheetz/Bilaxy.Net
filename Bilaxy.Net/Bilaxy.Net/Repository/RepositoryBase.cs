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
    using DateTimeHelpers;
    using RESTApiAccess;
    using RESTApiAccess.Interface;
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
        public async Task<T> GetRequest<T>(string endpoint, Dictionary<string, object> parms, bool secure = false)
        {
            var queryString = BilaxyHelper.ParmsToQueryString(parms);

            if(secure)
            {
                var signature = GetSignature(parms);

                queryString += $@"key={_apiCreds.ApiKey}&sign={signature}";
            }

            endpoint += $@"?{queryString}";

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

                endpoint += $@"&key={_apiCreds.ApiKey}&sign={signature}";
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
        /// <param name="timestamp">Timestamp for transaction</param>
        /// <returns>Object from response</returns>
        public async Task<T> GetRequest<T>(string endpoint, long timestamp)
        {
            var headers = GetRequestHeaders(HttpMethod.Get, endpoint, timestamp);

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.GetApiStream<BilaxyResponse<T>>(url, headers);

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
        /// <param name="timestamp">Timestamp for transaction</param>
        /// <param name="body">Request body data</param>
        /// <returns>Object from response</returns>
        public async Task<T> PostRequest<T>(string endpoint, long timestamp, SortedDictionary<string, object> body)
        {
            var headers = GetRequestHeaders(HttpMethod.Post, endpoint, timestamp, body);

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.PostApi<BilaxyResponse<T>, SortedDictionary<string, object>>(url, body, headers);

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
        /// <param name="timestamp">Timestamp for transaction</param>
        /// <returns>Object from response</returns>
        public async Task<T> DeleteRequest<T>(string endpoint, long timestamp)
        {
            var headers = GetRequestHeaders(HttpMethod.Delete, endpoint, timestamp);

            var url = baseUrl + endpoint;

            try
            {
                var response = await _restRepo.DeleteApi<BilaxyResponse<T>>(url, headers);

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetSignature()
        {
            return BilaxySecurity.GetSignature(_apiCreds.ApiKey, _apiCreds.ApiSecret);
        }

        private string GetSignature(Dictionary<string, object> parms)
        {
            return BilaxySecurity.PostSignature(_apiCreds.ApiKey, _apiCreds.ApiSecret, parms);
        }

        /// <summary>
        /// Get Request headers
        /// </summary>
        /// <param name="httpMethod">Http Method</param>
        /// <param name="endpoint">Endpoint to access</param>
        /// <param name="timestamp">Timestamp for transaction</param>
        /// <param name="body">Body data to be passed</param>
        /// <returns>Dictionary of request headers</returns>
        private Dictionary<string, string> GetRequestHeaders(HttpMethod httpMethod, string endpoint, long timestamp, SortedDictionary<string, object> body = null)
        {
            var headers = new Dictionary<string, string>();

            headers.Add("KC-API-KEY", _apiCreds.ApiKey);
            headers.Add("KC-API-SIGN", BilaxySecurity.GetSignature(httpMethod, endpoint, timestamp, _apiCreds.ApiSecret, body));
            headers.Add("KC-API-TIMESTAMP", timestamp.ToString());
            headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)");

            return headers;
        }
    }
}