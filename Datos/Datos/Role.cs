using System;
using System.Collections.Generic;

#nullable disable

namespace Datos.Datos
{
    public partial class Role
    {
        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRoles { get; set; }
        public string Nombre { get; set; }
        public int? FlagActivo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
