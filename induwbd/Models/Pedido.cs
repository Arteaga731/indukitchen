using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace induwbd.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int UserId { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; }

        public Pedido()
        {
            PedidoDetalles = new HashSet<PedidoDetalle>();
        }
    }

    public class PedidoDetalle
    {
        public int PedidoDetalleId { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}