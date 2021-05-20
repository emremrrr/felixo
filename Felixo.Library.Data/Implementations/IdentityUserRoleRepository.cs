using Felixo.Library.Entities.Models;
using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Felixo.Library.Data.Implementations
{
    public class IdentityUserRoleRepository : Repository<ApplicationUserRole>, IidentityUserRole
    {
        private readonly AppDataContext _context;

        public IdentityUserRoleRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }


    }
}
