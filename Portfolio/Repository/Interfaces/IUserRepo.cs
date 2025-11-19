using Entity.General_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepo
    {
        List<User> GetAll();
        User GetById(int id);
        User Create(User entity);
        User Update(User entity);
        bool Delete(int id);
    }
}
