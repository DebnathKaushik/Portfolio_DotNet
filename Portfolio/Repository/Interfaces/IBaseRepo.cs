using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBaseRepo<T> where T : class 
    {
        List<T> GetAll();
        IQueryable<T> Get_ByUserId();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
