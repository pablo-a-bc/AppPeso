using Datos.Datos;
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
        public float DifPeso { get; set; }
        public ResponseDataBase Response { get; set; }
    }

    public class ResponsePesoHistorial
    {
        public List<Datospeso> Historial { get; set; }
        public ResponseDataBase Response { get; set; }
    }
}
