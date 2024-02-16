using RestSharp;
using System;

namespace Core.Externals
{
    public static class RestCall
    {
        public static T Get<T>(string apiUrl, string method, string token = null)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(apiUrl);
            RestRequest request = new RestRequest(method, RestSharp.Method.Get);
            RestResponse response = client.ExecuteAsync(request).Result;

            if (token != null)
                request.AddHeader("authorization", token);

            return GetResponse<T>(response);
        }

        public static T Post<T>(string apiUrl, string method, object body, string token = null)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(apiUrl);
            RestRequest request = new RestRequest(method, RestSharp.Method.Post);

            if (body != null)
                request.AddJsonBody(body);

            if (token != null)
                request.AddHeader("authorization", token);

            RestResponse response = client.ExecuteAsync(request).Result;

            return GetResponse<T>(response);
        }

        public static T Patch<T>(string apiUrl, string method, object body, string token = null)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(apiUrl);
            RestRequest request = new RestRequest(method, RestSharp.Method.Post);

            if (body != null)
                request.AddJsonBody(body);

            if (token != null)
                request.AddHeader("authorization", token);

            RestResponse response = client.ExecuteAsync(request).Result;

            return GetResponse<T>(response);
        }

        public static T Put<T>(string apiUrl, string method, object body, string token = null)
        {
            RestSharp.RestClient client = new RestSharp.RestClient(apiUrl);
            RestRequest request = new RestRequest(method, RestSharp.Method.Put);

            if (body != null)
                request.AddJsonBody(body);

            if (token != null)
                request.AddHeader("authorization", token);

            RestResponse response = client.ExecuteAsync(request).Result;

            return GetResponse<T>(response);
        }

        private static T GetResponse<T>(RestResponse response)
        {
            if (response.IsSuccessful || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                T apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
                return apiResponse;
            }
            else
                throw new Exception("El http response no es válido: " + response.StatusDescription + ". " + response.Content + ". " + response.ErrorException);
        }

    }
}
