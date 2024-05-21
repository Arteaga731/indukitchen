using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace induwbd.Models
{
    public class SoporteTecnico
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int PedidoId { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}