using AutoMapper;
using Entity.Business_Entity;
using Entity.General_Entity;
using Manager.Utility;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class ProjectService
    {
        private readonly IBaseRepo<Project> _projectRepo;
        //private readonly IMapper _mapper;

        // Dependency Injection (abstruction)
        public ProjectService(IBaseRepo<Project> projectRepo)
        {
            _projectRepo = projectRepo;
           // _mapper = mapper;
        }

        //----------------------------------------------------------

        public List<ProjectDTO> GetAllProjects()
        {
            var projects = _projectRepo.GetAll();
           // return _mapper.Map<List<ProjectDTO>>(projects);
           return projects.Select(p => p.ToMap<ProjectDTO>()).ToList();
        }

        public List<ProjectDTO> GetProjectsByUserId(int userId)  
        {
            var projects = _projectRepo.Get_ByUserId().Where(x => x.UserId == userId); // Here return IQueryable Project
            //return _mapper.Map<List<ProjectDTO>>(projects) ;
            return projects.Select(p => p.ToMap<ProjectDTO>()).ToList() ;
        }

        public ProjectDTO GetProjectById(int id)
        {
            var project = _projectRepo.GetById(id);
           // return _mapper.Map<ProjectDTO>(project);
           return project.ToMap<ProjectDTO>();
        }

        public ProjectDTO CreateProject(ProjectDTO obj)
        {
            //var projectEntity = _mapper.Map<Project>(obj);
            var projectEntity = obj.ToMap<Project>();
            var created = _projectRepo.Create(projectEntity);
            //return _mapper.Map<ProjectDTO>(created);
            return created.ToMap<ProjectDTO>();
        }

        public ProjectDTO UpdateProject(ProjectDTO obj)
        {
            //var projectEntity = _mapper.Map<Project>(obj);
            var projectEntity = obj.ToMap<Project>();
            var updated = _projectRepo.Update(projectEntity);
            //return _mapper.Map<ProjectDTO>(updated);
            return updated.ToMap<ProjectDTO>();
        }

        public bool DeleteProject(int id)
        {
            return _projectRepo.Delete(id);
        }
    }
}
