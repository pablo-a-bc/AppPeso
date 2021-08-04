using System;
using System.Collections.Generic;

#nullable disable

namespace Datos.Datos
{
    public partial class Token
    {
        public Guid IdToken { get; set; }
        public string Tokenvalue { get; set; }
        public DateTime? FechaI { get; set; }
        public DateTime? FechaE { get; set; }
        public DateTime? FechaGen { get; set; }
        public string Modulo { get; set; }
        public long? UsuarioIdusuario { get; set; }

        public virtual Usuario UsuarioIdusuarioNavigation { get; set; }
    }
}
