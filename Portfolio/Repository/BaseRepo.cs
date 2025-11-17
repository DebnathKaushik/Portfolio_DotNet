using Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DB_Context _db;    // The whole database
        private readonly DbSet<T> _table;  // Specific table for entity type T


        // This is Constructor Injection — a form of Dependency Injection
        // Framework automatically gives the DB_Context
        public BaseRepo(DB_Context db) 
        {
            _db = db;
            _table = _db.Set<T>();
        }

        // ---------------------------------------
        public List<T> GetAll()
        {
            return _table.ToList();
        }
        public T GetById(int id)
        {
            return _table.Find(id);
        }
        public T Create(T entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _table.Update(entity);
            _db.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
           var exist_entity = _table.Find(id);
            if(exist_entity == null) 
            {
                return false;
            }
            else{
                _table.Remove(exist_entity);
                _db.SaveChanges();
                return true;
            }

        }

    }
}
