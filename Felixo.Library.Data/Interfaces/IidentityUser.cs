using Felixo.Library.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Data.Interfaces
{
    public interface IidentityUser : IRepository<ApplicationUser> 
    {
        User GetUserWithRole(string email);
    }
}
