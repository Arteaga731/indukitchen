using induwbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace induwbd.Controllers
{
    public class PedidoController : Controller
    {
        private static List<Pedido> Pedidos = new List<Pedido>();
        private static List<PedidoDetalle> PedidoDetalles = new List<PedidoDetalle>();
        private static List<Producto> Productos = new List<Producto>
        {
            new Producto { ProductoId = 1, Nombre = "Producto A", Descripcion = "Descripción A", Precio = 10.00M, Stock = 100 },
            new Producto { ProductoId = 2, Nombre = "Producto B", Descripcion = "Descripción B", Precio = 20.00M, Stock = 50 }
        };

        // GET: Pedido
        public ActionResult Index()
        {
            int userId = (int)Session["UserId"];
            var pedidos = Pedidos.Where(p => p.UserId == userId).ToList();
            return View(pedidos);
        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            List<Producto> listaProductos = new List<Producto>
        {
            new Producto { ProductoId = 1, Nombre = "Horno Industrial", Descripcion = "Horno de alta capacidad", Precio = 1200.00m, Stock = 10 },
            new Producto { ProductoId = 2, Nombre = "Licuadora Profesional", Descripcion = "Licuadora de alta potencia", Precio = 300.00m, Stock = 15 },
            new Producto { ProductoId = 3, Nombre = "Cuchillo Chef", Descripcion = "Cuchillo de acero inoxidable", Precio = 50.00m, Stock = 50 },
            new Producto { ProductoId = 4, Nombre = "Batidora", Descripcion = "Batidora de alta velocidad", Precio = 200.00m, Stock = 20 },
            new Producto { ProductoId = 5, Nombre = "Sartén Anti-adherente", Descripcion = "Sartén de 30cm", Precio = 80.00m, Stock = 30 },
            new Producto { ProductoId = 6, Nombre = "Freidora", Descripcion = "Freidora doble capacidad", Precio = 500.00m, Stock = 8 },
            new Producto { ProductoId = 7, Nombre = "Refrigerador Industrial", Descripcion = "Refrigerador de gran capacidad", Precio = 1500.00m, Stock = 5 },
            new Producto { ProductoId = 8, Nombre = "Lavavajillas", Descripcion = "Lavavajillas de alta eficiencia", Precio = 700.00m, Stock = 12 },
            new Producto { ProductoId = 9, Nombre = "Procesador de Alimentos", Descripcion = "Procesador multifuncional", Precio = 250.00m, Stock = 25 },
            new Producto { ProductoId = 10, Nombre = "Microondas Industrial", Descripcion = "Microondas de alta potencia", Precio = 400.00m, Stock = 10 }
        };
            var items = listaProductos.Select(p => new SelectListItem { Value = p.ProductoId.ToString(), Text = p.Nombre }).ToList();

            // Insertar un elemento por defecto al inicio de la lista
            items.Insert(0, new SelectListItem { Value = "", Text = "Seleccione un producto" });

            // Pasar la lista de elementos SelectListItem a la vista utilizando ViewBag
            ViewBag.ProductoId = items;

            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,Cantidad")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                var pedido = new Pedido
                {
                    PedidoId = Pedidos.Count > 0 ? Pedidos.Max(p => p.PedidoId) + 1 : 1,
                    UserId = (int)Session["UserId"],
                    FechaPedido = DateTime.Now,
                    Estado = "Pending"
                };
                pedidoDetalle.PedidoDetalleId = PedidoDetalles.Count > 0 ? PedidoDetalles.Max(pd => pd.PedidoDetalleId) + 1 : 1;
                pedidoDetalle.PedidoId = pedido.PedidoId;

                Pedidos.Add(pedido);
                PedidoDetalles.Add(pedidoDetalle);

                // Simulate sending an order confirmation email
                // SendOrderConfirmationEmail(pedido.UserId);

                return RedirectToAction("Index");
            }

            ViewBag.Productos = new SelectList(Productos, "ProductoId", "Nombre", pedidoDetalle.ProductoId);
            return View(pedidoDetalle);
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = Pedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            var detalles = PedidoDetalles.Where(pd => pd.PedidoId == id).ToList();
            ViewBag.Detalles = detalles;
            return View(pedido);
        }
    }
}
