using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Felixo.Library.Entities.Models
{
    public class Book
    {

        public Book()
        {
            Category = new Category();
        }

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }


        [ForeignKey("Category")]
        public virtual long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //[ForeignKey("Reuest")]
        //public virtual long ReuestId { get; set; }
        //public virtual Request Request { get; set; }
    }
}
