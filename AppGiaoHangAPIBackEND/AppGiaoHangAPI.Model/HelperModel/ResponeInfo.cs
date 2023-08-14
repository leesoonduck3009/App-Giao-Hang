using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppGiaoHangAPI.Model.HelperModel
{
    public class ResponeInfo
    {
        public HttpStatusCode statusCode { get; set; }
        public string? error_code { get; set; }
        public string? message { get; set; }
        public object data { get; set; }
    }
}
