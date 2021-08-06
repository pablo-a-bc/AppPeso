using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datos.Helper
{
    public class ResponseDataBase
    {
        public bool Resp { get; set; }
        public string Message { get; set; }
    }

    public class ResponsePesoMinMax
    {
        public float PesoMin { get; set; }
        public float PesoMax { get; set; }

        public ResponseDataBase Response { get; set; }
    }
}
