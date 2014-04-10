using MSC.Socrata.Device.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class SodaResponseHandler<T>
    {
        private const string FIELDS_HEADER = "X-SODA2-Fields";

        private const string TYPES_HEADER = "X-SODA2-Types";

        public Response<T> Response { get; private set; }

        private JsonAdapter _jsonAdapter;

        public SodaResponseHandler(Type target, DataTypesMapper dataTypesMapper)
        {
            _jsonAdapter = new JsonAdapter(target, dataTypesMapper);
            Response = new Response<T>();
        }

        public void OnSuccess(int status, HttpResponseHeaders headers, JObject jsonObject)
        {
            parseHeaders(status, headers);
            Response.Json = jsonObject;
            if (jsonObject != null)
            {
                T entity = default(T);
                try
                {
                    entity = (T)_jsonAdapter.FromJsonObject(jsonObject);
                }
                catch (Exception xcp)
                {
                    throw new SodaTypeConversionException(xcp);
                }
                Response.Entity = entity;
            }
        }

        public void OnSuccess(int status, HttpResponseHeaders headers, JArray jsonArray)
        {
            parseHeaders(status, headers);
            Response.Json = jsonArray;
            if (jsonArray != null)
            {
                T entity;
                try
                {

                    entity = (T)(object)_jsonAdapter.FromJsonArray(jsonArray);
                }
                catch (Exception xcp)
                {
                    throw new SodaTypeConversionException(xcp);
                }
                Response.Entity = entity;
            }
        }

        public void OnFailure(int statusCode, Exception xcp, JObject jsonObject)
        {
            parseHeaders(statusCode, null);
            var responseError = new ResponseError(
                    jsonObject.Value<string>("code"),
                    jsonObject.Value<string>("message"),
                    jsonObject.Value<JObject>("data")
            );
            responseError.Error = xcp;
            Response.Error = responseError;
            Response.Json = jsonObject;
        }

        public void OnFailure(int statusCode, Exception xcp, JArray jsonArray)
        {
            parseHeaders(statusCode, null);
            var responseError = new ResponseError();
            responseError.Error = xcp;
            Response.Error = responseError;
            Response.Json = jsonArray;
        }

        private void parseHeaders(int status, HttpResponseHeaders hds)
        {
            Response.Status = status;
            if (hds != null)
            {
                var headers = new Dictionary<string, string>();
                foreach (var header in hds)
                {
                    headers.Add(header.Key, string.Join(" ", header.Value.ToArray()));
                }
                Response.Headers = headers;
                var fieldsJson = headers[FIELDS_HEADER];
                var typesJson = headers[TYPES_HEADER];
                if (fieldsJson != null && typesJson != null)
                {
                    try
                    {
                        var fields = new JArray(fieldsJson);
                        var types = new JArray(typesJson);
                        for (int i = 0; i < fields.Count; i++)
                        {
                            var field = fields[i];
                            var type = types[i];
                            _jsonAdapter.AddFieldMapping(field.Value<string>(), type.Value<string>());
                        }
                    }
                    catch (Exception e)
                    {
                        throw new SodaTypeConversionException(e);
                    }
                }
            }
        }
    }
}
