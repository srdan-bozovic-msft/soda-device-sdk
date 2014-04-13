using MSC.Socrata.Device.Soql;
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

        private async Task<Response<T>> GetAsync<T>(string url, SodaResponseHandler<T> responseHandler)
        {
            HttpResponseMessage response = null;
            //Log.d("socrata", String.format("Consumer : %s params: %s", url, params));
            try
            {
                response = await _client.GetAsync(GetAbsoluteUrl(url)).ConfigureAwait(false);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    responseHandler.OnSuccess(
                        (int)response.StatusCode,
                        response.Headers, 
                        JObject.Parse(content)
                        );
                }
                else
                {
                    responseHandler.OnFailure(
                        (int)response.StatusCode,
                        response.Headers,
                        null,
                        JObject.Parse(string.Format("{{'code':'{0}','message':'{1}','data':null}}",(int)response.StatusCode,response.ReasonPhrase))
                        );
                }
            }
            catch(Exception xcp)
            {
                responseHandler.OnFailure(0, response != null ? response.Headers : null, xcp, null);
            }
            return responseHandler.Response;
        }

        private async Task<Response<T[]>> GetArrayAsync<T>(string url, SodaResponseHandler<T[]> responseHandler)
        {
            //Log.d("socrata", String.format("Consumer : %s params: %s", url, params));
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(GetAbsoluteUrl(url)).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                        response.Headers,
                        null,
                        JObject.Parse(string.Format("{{'code':'{0}','message':'{1}','data':null}}",(int)response.StatusCode,response.ReasonPhrase))
                        );
                }
            }
            catch (Exception xcp)
            {
                responseHandler.OnFailure(0, response != null ? response.Headers : null, xcp, null);
            }
            return responseHandler.Response;
        }

        private string GetAbsoluteUrl(string relativeUrl) {
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

        public async Task<Response<T>> GetObjectAsync<T>(string dataset, string id)
        {
            return await GetAsync<T>(string.Format("/{0}/{1}", dataset, id),
                new SodaResponseHandler<T>(DataTypesMapper)).ConfigureAwait(false);
        }

        public async Task<Response<T[]>> GetObjectsAsync<T>(string dataset)
        {
            return await GetArrayAsync<T>(string.Format("/{0}", dataset),
                new SodaResponseHandler<T[]>(DataTypesMapper)).ConfigureAwait(false);
        }

        public async Task<Response<T[]>> GetObjectsAsync<T>(string dataset, string query)
        {
            return await GetArrayAsync<T>(string.Format("/{0}?$query={1}", dataset, query),
                new SodaResponseHandler<T[]>(DataTypesMapper)).ConfigureAwait(false);
        }
    
        public async Task<Response<T[]>> GetObjectsAsync<T>(Query<T> query)
        {
            return await GetObjectsAsync<T>(query.Dataset, query.Build());
        }
    
        public async Task<Response<T[]>> SearchObjectsAsync<T>(string dataset, string keywords)
        {
            return await GetArrayAsync<T>(string.Format("/{0}?$s=", dataset, keywords),
                new SodaResponseHandler<T[]>(DataTypesMapper)).ConfigureAwait(false);
        }
    }
}
