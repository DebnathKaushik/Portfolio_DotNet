using Entity;
using Entity.General_Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DB_Context _db;    
        private readonly DbSet<User> _table;

        public UserRepo(DB_Context db)
        {
            _db = db;
            _table = _db.Set<User>();
        }

        // ------------------------------------------------------------------------------------------

        public List<User> GetAll()
        {
            return _table.ToList();
        }

        public User GetById(int id)
        {
            return _table.Find(id);
        }

        public User Create(User entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public User Update(User entity)
        {
            _table.Update(entity);
            _db.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _table.Find(id);
            if (entity == null) return false;
            _table.Remove(entity);
            _db.SaveChanges();
            return true;
        }

        
        
    }
}
