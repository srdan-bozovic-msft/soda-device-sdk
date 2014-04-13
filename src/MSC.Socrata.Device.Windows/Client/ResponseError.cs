using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class ResponseError
    {
        public string Code { get; private set; }

        public string Message { get; private set; }

        public JObject Data { get; private set; }

        public Exception Error { get; internal set; }

        public ResponseError()
        {
        }

        public ResponseError(String code, String message, JObject data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
