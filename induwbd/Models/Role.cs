using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace induwbd.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public Role()
        {
            Usuarios = new HashSet<Usuario>();
        }
    }
}