using Entity;
using Entity.General_Entity;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DB_Context _db;   // database 

        public IUserRepo user { get; }
        public IBaseRepo<Project> project { get; }
        public IBaseRepo<Education> education { get; }
        public IBaseRepo<Experience> experience { get; }
        
        // Dependency Inject
        public UnitOfWork(
            DB_Context db, 
            IUserRepo user, 
            IBaseRepo<Project> project,
            IBaseRepo<Education> education,
            IBaseRepo<Experience> experience)
        {
            _db = db;
            this.user = user;
            this.project = project;
            this.education = education;
            this.experience = experience;

        }


        public int save()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }

    }
}
