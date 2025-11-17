using AutoMapper;
using Entity.Business_Entity;
using Entity.General_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Entity --> DTO
            CreateMap<User, UserDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<Education, EducationDTO>();
            CreateMap<Experience, ExperienceDTO>();

            // DTO --> Entity
            CreateMap<UserDTO, User>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<EducationDTO, Education>();
            CreateMap<ExperienceDTO, Experience>();
        }
    }
}
