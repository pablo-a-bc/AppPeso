using System;
using System.Collections.Generic;

#nullable disable

namespace Datos.Datos
{
    public partial class Datospeso
    {
        public Guid IdDatoPeso { get; set; }
        public float? Peso { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public long? UsuarioIdusuario { get; set; }

        public virtual Usuario UsuarioIdusuarioNavigation { get; set; }
    }
}
