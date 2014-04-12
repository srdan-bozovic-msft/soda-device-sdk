using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class Consumer
    {
        public string Domain { get; private set; }
        public string Token { get; private set; }

        private const string TOKEN_HEADER = "X-App-Token";

        private HttpClient _client = new HttpClient();

        public DataTypesMapper DataTypesMapper { get; set; }

        private async Task<T> GetAsync<T>(string url, SodaResponseHandler<T> responseHandler)
        {
            //Log.d("socrata", String.format("Consumer : %s params: %s", url, params));
            //client.get(getAbsoluteUrl(url), params, responseHandler);
            try
            {
                var response = await _client.GetAsync(getAbsoluteUrl(url));
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    responseHandler.OnSuccess(
                        (int)response.StatusCode, 
                        response.Headers, 
                        JArray.Parse(content)
                        );
                }
                else
                {
                    responseHandler.OnFailure(
                        (int)response.StatusCode,
                        null,
                        JObject.Parse("{'code':'','message':'','data':null}")
                        );
                }
                return responseHandler.Response.Entity;
            }
            catch
            {
                return default(T);
            }
        }

        private string getAbsoluteUrl(string relativeUrl) {
            return "https://" + Domain + "/resource" + relativeUrl;
        }

        public Consumer(string domain)
        {
            Domain = domain;
            DataTypesMapper = new DataTypesMapper();
        }

        public Consumer(string domain, string token)
            : this(domain)
        {
            Token = token;
            _client.DefaultRequestHeaders.Add(TOKEN_HEADER, token);
        }

        public async Task<T> GetObjectAsync<T>(string dataset, string id)
        {
            return await GetAsync<T>(string.Format("/{0}/{1}", dataset, id)
                , new SodaResponseHandler<T>(typeof(T), DataTypesMapper));
        }

        public async Task<T> GetObjectAsync<T>(string dataset)
        {
            return await GetAsync<T>(string.Format("/{0}", dataset)
                , new SodaResponseHandler<T>(typeof(T), DataTypesMapper));
        }

        //public <T> void getObjects(String dataset, String query, Class<?> mapping, Callback<T> callback) {
        //    SodaCallbackResponseHandler<T> handler = new SodaCallbackResponseHandler<T>(mapping, callback, dataTypesMapper);
        //    RequestParams params = new RequestParams();
        //    params.put("$query", query);
        //    get(String.format("/%s", dataset), params, handler);
        //}
    
        //public <T> void getObjects(Query query, Callback<T> callback) {
        //    getObjects(query.getDataset(), query.build(), query.getMapping(), callback);
        //}
    
        //public <T> void searchObjects(String dataset, String keywords, Class<?> mapping, Callback<T> callback) {
        //    SodaCallbackResponseHandler<T> handler = new SodaCallbackResponseHandler<T>(mapping, callback, dataTypesMapper);
        //    RequestParams params = new RequestParams();
        //    params.put("$s", keywords);
        //    get(String.format("/%s", dataset), params, handler);
        //}

    }
}
