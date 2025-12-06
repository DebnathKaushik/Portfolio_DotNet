using Entity.General_Entity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepo user {  get; }
        public IBaseRepo<Project> project { get; }
        public IBaseRepo<Education> education { get; }
        public IBaseRepo<Experience> experience { get; }

        int save();

        IDbContextTransaction BeginTransaction();
    }
}
