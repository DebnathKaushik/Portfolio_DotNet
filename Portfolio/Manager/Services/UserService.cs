using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entity.Business_Entity;
using Entity.Common;
using Entity.General_Entity;
using Manager.Utility;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class UserService 
    {
        private readonly IUserRepo _userRepo;
       // private readonly IMapper _mapper;

        // Dependency Injection (abstruction)
        public UserService(IUserRepo userRepo) 
        {
            _userRepo = userRepo;
           // _mapper = mapper;

        }

        //-----------------------------------------------------------

        public List<UserDTO> GetAllusers()
        {
            var users = _userRepo.GetAll();
            //return _mapper.Map<List<UserDTO>>(users);
            return users.Select(u => u.ToMap<UserDTO>()).ToList();

        }

        public UserDTO GetUserById(int id) 
        {
            var user = _userRepo.GetById(id);
            //return _mapper.Map<UserDTO>(user);
            return user.ToMap<UserDTO>();
        }

        public UserDTO CreateUser(UserDTO obj)
        {
            //var userEntity = _mapper.Map<User>(obj); 
            var userEntity = obj.ToMap<User>();  // Convert UserDTO obj --> Entity
            var created = _userRepo.Create(userEntity); // cause _userRepo(Repo) deals with actual entity obj
          // return _mapper.Map<UserDTO>(created); 
            return created.ToMap<UserDTO>();   // Convert Entity ---> UserDTO obj
        }

        public UserDTO UpdateUser(UserDTO obj)
        {
            //var userEntity = _mapper.Map<User>(obj);
            var userEntity = obj.ToMap<User>();
            var updated = _userRepo.Update(userEntity);
            //return _mapper.Map<UserDTO>(updated);
            return updated.ToMap<UserDTO>();

        }

        public bool DeleteUser(int id)
        {
            return _userRepo.Delete(id);
        }



        // For Show User Full Deatils 
        public UserFullDetailsVM GetUserFullDetails(int userId)
        {
            return _userRepo.GetUserFullDetails(userId);
        }

        // For Search Functionality 
        public List<UserDTO> SearchUserByUserName(string userName)
        {
            var user = _userRepo.SearchUserByUserName(userName);
            if (user == null) return null;
            //return _mapper.Map<List<UserDTO>>(user);
            return user.Select(u => u.ToMap<UserDTO>()).ToList();
        }

        // For Pagination
        public IQueryable<UserDTO> GetAllUserPagination()
        {
            var users = _userRepo.GetAllUserPagination(); // IQueryable<User>

            var usersDto = users
                .AsQueryable()
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Age = u.Age,
                    Email = u.Email,
                    Bio = u.Bio
                });

            return usersDto; // IQueryable<UserDTO>
        }




    }
}
