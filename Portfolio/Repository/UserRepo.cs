using Azure;
using Entity;
using Entity.Business_Entity;
using Entity.General_Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DB_Context _db;    
        private readonly DbSet<User> _table;

        // Dependency Injection
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

        public UserFullDetailsVM GetUserFullDetails(int userId)
        {
            // Ado .Net connection here ( here is Multiple Model sets , that's why Ado.net )

            using (var connection = _db.Database.GetDbConnection())
            {
                connection.Open();

                using(var cmd =  connection.CreateCommand()) 
                {
                    cmd.CommandText = "GetUserFullDetails"; // Stored Procedure Name (SP)
                    cmd.CommandType = CommandType.StoredProcedure;


                    var param = cmd.CreateParameter();
                    param.ParameterName = "@UserId";    // Map with SP Parameter
                    param.Value = userId;
                    cmd.Parameters.Add(param);

                    var vm = new UserFullDetailsVM();   

                    using( var reader = cmd.ExecuteReader()) 
                    {


                        // 1st -> User
                        if (reader.Read()) 
                        {
                            vm.User = new UserDTO
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Bio = reader.GetString(reader.GetOrdinal("Bio"))
                            };

                        }

                        // 2nd -> Projects
                        reader.NextResult();
                        vm.Projects = new List<ProjectDTO>();
                        while(reader.Read()) 
                        {
                            vm.Projects.Add(new ProjectDTO
                            {
                                ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                                ProjectTitle = reader.GetString(reader.GetOrdinal("ProjectTitle")),
                                Description = reader.GetString(reader.GetOrdinal("Description"))
                            });
                        }

                        // 3rd -> EDUCATIONS
                        reader.NextResult();
                        vm.Educations = new List<EducationDTO>();
                        while (reader.Read())
                        {
                            vm.Educations.Add(new EducationDTO
                            {
                                EducationId = reader.GetInt32(reader.GetOrdinal("EducationId")),
                                Institution = reader.GetString(reader.GetOrdinal("Institution")),
                                Degree = reader.GetString(reader.GetOrdinal("Degree")),
                                Year = reader.GetString(reader.GetOrdinal("Year"))
                            });
                        }


                        // 4th -> EXPERIENCES
                        reader.NextResult();
                        vm.Experiences = new List<ExperienceDTO>();
                        while (reader.Read())
                        {
                            vm.Experiences.Add(new ExperienceDTO
                            {
                                ExperienceId = reader.GetInt32(reader.GetOrdinal("ExperienceId")),
                                CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                                Position = reader.GetString(reader.GetOrdinal("Position"))
                                
                            });
                        }


                    }

                    return vm;


                }
            }
            
        }

       
        
        // this is for Search Functionality 
        public List<User> SearchUserByUserName(string userName)
        {
            return _db.Users
                .FromSqlRaw("EXEC GetUserByUserName @UserName = {0}", userName)
                .ToList();
        }
    }
}
