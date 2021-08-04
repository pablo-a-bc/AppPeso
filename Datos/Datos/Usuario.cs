using System;
using System.Collections.Generic;

#nullable disable

namespace Datos.Datos
{
    public partial class Usuario
    {
        public Usuario()
        {
            Datospesos = new HashSet<Datospeso>();
            Tokens = new HashSet<Token>();
        }

        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public float? Estatura { get; set; }
        public string Password { get; set; }
        public int? FlagActivo { get; set; }
        public int? RolesIdRoles { get; set; }

        public virtual Role RolesIdRolesNavigation { get; set; }
        public virtual ICollection<Datospeso> Datospesos { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
