using AutoMapper;
using Entity.Business_Entity;
using Entity.Common;
using Entity.General_Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class UserService 
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        // Dependency Injection (abstruction)
        public UserService(IUserRepo userRepo, IMapper mapper) 
        {
            _userRepo = userRepo;
            _mapper = mapper;

        }

        //-----------------------------------------------------------

        public List<UserDTO> GetAllusers()
        {
            var users = _userRepo.GetAll();
            return _mapper.Map<List<UserDTO>>(users);

        }

        public UserDTO GetUserById(int id) 
        {
            var user = _userRepo.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO CreateUser(UserDTO obj)
        {
            var userEntity = _mapper.Map<User>(obj); // Convert UserDTO obj --> Entity
            var created = _userRepo.Create(userEntity); // cause _userRepo(Repo) deals with actual entity obj
            return _mapper.Map<UserDTO>(created); // Convert Entity ---> UserDTO obj
        }

        public UserDTO UpdateUser(UserDTO obj)
        {
            var userEntity = _mapper.Map<User>(obj);
            var updated = _userRepo.Update(userEntity);
            return _mapper.Map<UserDTO>(updated);

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
        public UserDTO GetUserByUserName(string userName)
        {
            var ExsitsUser = _userRepo.GetUserByUserName(userName);
            if (ExsitsUser == null) return null;
            return _mapper.Map<UserDTO>(ExsitsUser);
        }



    }
}
