using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Felixo.Library.Entities.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string CategoryName { get; set; }
        
    }
}
