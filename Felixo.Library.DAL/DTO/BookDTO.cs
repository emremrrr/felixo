using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Entities.DTO
{
    public class BookDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }
        //
        public virtual CategoryDTO Category { get; set; }

    }
}
