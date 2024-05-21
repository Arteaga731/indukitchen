using induwbd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace induwbd.Controllers
{
    public class SoporteTecnicoController : Controller
    {
            private static List<SoporteTecnico> SoporteTecnicos = new List<SoporteTecnico>();

            // GET: SoporteTecnico
            public ActionResult Index()
            {
                int userId = (int)Session["UserId"];
                var tickets = SoporteTecnicos.Where(t => t.UserId == userId).ToList();
                return View(tickets);
            }

            // GET: SoporteTecnico/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: SoporteTecnico/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "IssueDescription")] SoporteTecnico ticket)
            {
                if (ModelState.IsValid)
                {
                    ticket.TicketId = SoporteTecnicos.Count > 0 ? SoporteTecnicos.Max(t => t.TicketId) + 1 : 1;
                    ticket.UserId = (int)Session["UserId"];
                    ticket.Status = "Open";
                    ticket.CreatedAt = DateTime.Now;
                    SoporteTecnicos.Add(ticket);
                    return RedirectToAction("Index");
                }

                return View(ticket);
            }

            // GET: SoporteTecnico/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SoporteTecnico ticket = SoporteTecnicos.FirstOrDefault(t => t.TicketId == id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }

            // POST: SoporteTecnico/Close/5
            [HttpPost, ActionName("Close")]
            [ValidateAntiForgeryToken]
            public ActionResult CloseConfirmed(int id)
            {
                SoporteTecnico ticket = SoporteTecnicos.FirstOrDefault(t => t.TicketId == id);
                if (ticket != null)
                {
                    ticket.Status = "Closed";
                    ticket.ResolvedAt = DateTime.Now;
                }
                return RedirectToAction("Index");
            }
        
    }
}
