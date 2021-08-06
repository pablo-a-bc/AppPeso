using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppPeso.Models
{
    public class DatoPesoModel
    {
    }
    public class DatoPesoInsert
    {
        public float Peso { get; set; }
        public string Fecha { get; set; }
        public int Idusuario { get; set; }
    }
}
