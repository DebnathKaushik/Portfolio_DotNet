using Entity.Business_Entity;
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

        UserFullDetailsVM GetUserFullDetails(int userId);   // For Show all User Details
          
        List<User> SearchUserByUserName(string userName);  // For search functionality

        IQueryable<User> GetAllUserPagination();  // For pagination
     }
}
