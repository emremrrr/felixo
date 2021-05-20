using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace Felixo.Library.Entities.Models
{
    public class User
    {
        public ApplicationUser IdentityUser{ get; set; }
        public ICollection<ApplicationUserRole> Roles { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
