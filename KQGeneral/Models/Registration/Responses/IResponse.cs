using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KQGeneral.Models.Registration.Responses
{
    public class ServerResponse
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public object Data { get; set; }
    }
}
