using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Entities.DTO
{
    public class RequestDTO
    {
        public long Id { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }

        public virtual ApplicationUserDTO ApplicationUser { get; set; }

        public BookDTO Book { get; set; }


    }
}
