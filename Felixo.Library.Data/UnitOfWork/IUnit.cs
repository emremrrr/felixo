using Felixo.Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Felixo.Library.Data.UnitOfWork
{
    public interface IUnit : IDisposable
    {
        public IBookRepository BookRepository { get; }
        public IRequestRepository RequestRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IidentityUserLogin IdentityUserLoginRepository { get; }
        public IidentityUser IdentityUserRepository { get; }
        public IidentityUserRole IdentityUserRoleRepository { get; }
        public IidentityRole IdentityRoleRepository { get; }


        Task<int> Commit();
    }
}
