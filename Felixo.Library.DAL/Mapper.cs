using AutoMapper;
using Felixo.Library.Entities.DTO;
using Felixo.Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Entities
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<Request, RequestDTO>();
            CreateMap<Book,BookDTO>();
            CreateMap<Category,CategoryDTO>();
            CreateMap<ApplicationUser, ApplicationUserDTO>();
        }
    }
}
