﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace HouseSource.Services
{
    public sealed class RestSharpHelper<T>
    {
        //private static readonly object padlock = new object();

        private static readonly Lazy<RestSharpHelper<T>> lazy = new Lazy<RestSharpHelper<T>>(() => new RestSharpHelper<T>());

        public static RestSharpHelper<T> Instance
        {
            get { return lazy.Value; }
        }

        private RestSharpHelper()
        {

        }

        //static RestClient _restClient = new RestClient("http://47.108.202.57:8081/WebService.asmx");
        static RestClient _restClient = new RestClient("http://47.108.202.57:8087/WebService.asmx");
        //static RestClient _restClient = new RestClient("http://120.26.3.153:7777");

        static RestClient _restClient2 = new RestClient();

        /// <summary>
        /// Post 异步 反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse responsePost = await _restClient.ExecuteAsync(requestPost);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            T t = JsonConvert.DeserializeObject<T>(responsePost.Content);
            return t;
        }

        /// <summary>
        /// Get 异步 反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = await _restClient.ExecuteAsync(requestGet);

            if (responseGet.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responseGet.ErrorException);
                throw twilioException;
            }

            T t = JsonConvert.DeserializeObject<T>(responseGet.Content);
            return t;
        }

        /// <summary>
        /// Get 反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T Get(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = _restClient.Execute(requestGet);

            if (responseGet.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responseGet.ErrorException);
                throw twilioException;
            }

            T t = JsonConvert.DeserializeObject<T>(responseGet.Content);
            return t;
        }

        /// <summary>
        /// Post 反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Post(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse responsePost = _restClient.Execute(requestPost);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            T t = JsonConvert.DeserializeObject<T>(responsePost.Content);
            return t;
        }

        /// <summary>
        /// Get 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWithoutDeserialization(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = _restClient.Execute(requestGet);

            if (responseGet.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responseGet.ErrorException);
                throw twilioException;
            }

            if (responseGet.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responseGet.Content;
        }

        /// <summary>
        /// Post 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostWithoutDeserialization(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse responsePost = _restClient.Execute(requestPost);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            if (responsePost.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responsePost.Content;
        }

        /// <summary>
        /// Get 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsyncWithoutDeserialization(string url)
        {
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = await _restClient.ExecuteAsync(requestGet);

            if (responseGet.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responseGet.ErrorException);
                throw twilioException;
            }

            if (responseGet.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responseGet.Content;
        }

        /// <summary>
        /// Post 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<string> PostAsyncWithoutDeserialization(string url, string json)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse responsePost = await _restClient.ExecuteAsync(requestPost);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            if (responsePost.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responsePost.Content;
        }

        /// <summary>
        /// Post Form表单 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static async Task<string> PostFormAsyncWithoutDeserialization(string url, string form)
        {
            var requestPost = new RestRequest(url, Method.POST);
            requestPost.AddHeader("content-type", "application/x-www-form-urlencoded");
            requestPost.AddParameter("application/x-www-form-urlencoded", form, ParameterType.RequestBody);
            IRestResponse responsePost = await _restClient.ExecuteAsync(requestPost);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            if (responsePost.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responsePost.Content;
        }

        /// <summary>
        /// Post Form表单 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static async Task<string> PostFormAsyncWithoutDeserialization(RestRequest restRequest)
        {
            IRestResponse responsePost = await _restClient.ExecuteAsync(restRequest);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is ";
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            if (responsePost.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responsePost.Content;
        }

        /// <summary>
        /// Get 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsyncWithoutDeserialization(string baseUrl, string url)
        {
            _restClient2.BaseUrl = new Uri(baseUrl);
            var requestGet = new RestRequest(url, Method.GET);
            IRestResponse responseGet = await _restClient2.ExecuteAsync(requestGet);

            if (responseGet.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is " + url;
                var twilioException = new ApplicationException(message, responseGet.ErrorException);
                throw twilioException;
            }

            if (responseGet.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responseGet.Content;
        }

        /// <summary>
        /// Post Form表单 异步 不反序列化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static async Task<string> PostFormAsyncWithoutDeserialization(string baseUrl, RestRequest restRequest)
        {
            _restClient2.BaseUrl = new Uri(baseUrl);
            IRestResponse responsePost = await _restClient2.ExecuteAsync(restRequest);

            if (responsePost.ErrorException != null)
            {
                string message = "Error retrieving response. The Url is ";
                var twilioException = new ApplicationException(message, responsePost.ErrorException);
                throw twilioException;
            }

            if (responsePost.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            return responsePost.Content;
        }
    }
}
