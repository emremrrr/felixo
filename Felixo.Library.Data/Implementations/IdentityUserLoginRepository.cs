using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Felixo.Library.Data.Implementations
{
    public class IdentityUserLoginRepository : Repository<IdentityUserLogin<string>>,IidentityUserLogin
    {
        private readonly AppDataContext _context;

        public IdentityUserLoginRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
