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
        //private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        // Dependency Injection (abstruction)
        public UserService(IMapper mapper, IUnitOfWork uow) 
        {
            //_userRepo = userRepo;
            _uow = uow;
            _mapper = mapper;

        }

        //-----------------------------------------------------------

        public List<UserDTO> GetAllusers()
        {
            var users = _uow.user.GetAll();
            //return _mapper.Map<List<UserDTO>>(users);
            return users.Select(u => u.ToMap<UserDTO>()).ToList();

        }

        public UserDTO GetUserById(int id) 
        {
            var user = _uow.user.GetById(id);
            //return _mapper.Map<UserDTO>(user);
            return user.ToMap<UserDTO>();
        }

        public UserDTO CreateUser(UserDTO obj)
        {
            //var userEntity = _mapper.Map<User>(obj); 
            var userEntity = obj.ToMap<User>();  
            var created = _uow.user.Create(userEntity);
            _uow.save();
          // return _mapper.Map<UserDTO>(created); 
            return created.ToMap<UserDTO>();   
        }

        public UserDTO UpdateUser(UserDTO obj)
        {
            //var userEntity = _mapper.Map<User>(obj);
            var userEntity = obj.ToMap<User>();
            var updated = _uow.user.Update(userEntity);
            _uow.save();
            //return _mapper.Map<UserDTO>(updated);
            return updated.ToMap<UserDTO>();

        }

        public bool DeleteUser(int id)
        {
            var Willbedeleted = _uow.user.Delete(id);
            if (Willbedeleted)
            {
                _uow.save();
                return true;
            }
            return false;

        }



        // For Show User Full Deatils 
        public UserFullDetailsVM GetUserFullDetails(int userId)
        {
            return _uow.user.GetUserFullDetails(userId);
        }

        // For Search Functionality 
        public List<UserDTO> SearchUserByUserName(string userName)
        {
            var user = _uow.user.SearchUserByUserName(userName);
            if (user == null) return null;
            //return _mapper.Map<List<UserDTO>>(user);
            return user.Select(u => u.ToMap<UserDTO>()).ToList();
        }

        // For Pagination
        public IQueryable<UserDTO> GetAllUserPagination()
        {
            var users = _uow.user.GetAllUserPagination(); // IQueryable<User>

            // In IQueryable we cannot use Autommaper extension methodlike : ToMap<UserDTO>()
            // ToMap<UserDTO>() , It's only good for in-memory object mapping
            //var usersDto = users    
            //    .AsQueryable()
            //    .Select(u => new UserDTO
            //    {
            //        UserId = u.UserId,
            //        UserName = u.UserName,
            //        Age = u.Age,s
            //        Email = u.Email,
            //        Bio = u.Bio
            //    });
            //return usersDto; 




            // IQueryable<UserDTO>, When IQueryable then use ProjectTO<>
            return users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider); 
        }




    }
}
