using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class Response<T>
    {
        public static implicit operator T(Response<T> response)
        {
            return response.HasEntity ? response.Entity : default(T);
        }
        public int Status { get; internal set; }

        public IDictionary<string, string> Headers { get; internal set; }

        public T Entity { get; internal set; }

        public ResponseError Error { get; internal set; }

        public object Json { get; internal set; }

        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }

        public bool HasEntity
        {
            get
            {
                return Entity != null;
            }
        }

        internal Response()
        {
        }



    }
}
