using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Felixo.Library.Entities.Models
{
    public class Request
    {
        [Key]
        public long Id { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }

        [ForeignKey("ApplicationUser")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Book")]
        public virtual long BookId { get; set; }

        public virtual Book Book { get; set; }

        [NotMapped]
        public string? ClientId { get; set; }
    }
}
