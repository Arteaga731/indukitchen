using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace induwbd.Models
{
    public class Mensaje
    {
        public int MensajeId { get; set; }
        public int TicketId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public virtual SoporteTecnico SoporteTecnico { get; set; }
        public virtual Usuario FromUser { get; set; }
        public virtual Usuario ToUser { get; set; }
    }
}