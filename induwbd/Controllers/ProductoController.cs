using induwbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace induwbd.Controllers
{
    public class ProductoController : Controller
    {
        private static List<Producto> productos = new List<Producto>();

        // GET: Producto
        public ActionResult Index()
        {
            return View(productos);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            var producto = productos.FirstOrDefault(p => p.ProductoId == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.ProductoId = productos.Any() ? productos.Max(p => p.ProductoId) + 1 : 1;
                productos.Add(producto);
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            var producto = productos.FirstOrDefault(p => p.ProductoId == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var existingProducto = productos.FirstOrDefault(p => p.ProductoId == producto.ProductoId);
                if (existingProducto != null)
                {
                    existingProducto.Nombre = producto.Nombre;
                    existingProducto.Descripcion = producto.Descripcion;
                    existingProducto.Precio = producto.Precio;
                    existingProducto.Stock = producto.Stock;
                }
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            var producto = productos.FirstOrDefault(p => p.ProductoId == id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var producto = productos.FirstOrDefault(p => p.ProductoId == id);
            if (producto != null)
            {
                productos.Remove(producto);
            }
            return RedirectToAction("Index");
        }
    }
}
