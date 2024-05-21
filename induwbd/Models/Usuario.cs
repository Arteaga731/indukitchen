using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace induwbd.Models
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<SoporteTecnico> SoporteTecnicoTickets { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }

        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
            SoporteTecnicoTickets = new HashSet<SoporteTecnico>();
            Mensajes = new HashSet<Mensaje>();
        }
    }
}