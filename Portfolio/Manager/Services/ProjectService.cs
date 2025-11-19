using AutoMapper;
using Entity.Business_Entity;
using Entity.General_Entity;
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
        private readonly IMapper _mapper;

        // Dependency Injection (abstruction)
        public ProjectService(IBaseRepo<Project> projectRepo, IMapper mapper)
        {
            _projectRepo = projectRepo;
            _mapper = mapper;
        }

        //----------------------------------------------------------

        public List<ProjectDTO> GetAllProjects()
        {
            var projects = _projectRepo.GetAll();
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public List<ProjectDTO> GetProjectsByUserId(int userId)  
        {
            var projects = _projectRepo.Get_ByUserId(userId);
            return _mapper.Map<List<ProjectDTO>>(projects) ;
        }

        public ProjectDTO GetProjectById(int id)
        {
            var project = _projectRepo.GetById(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public ProjectDTO CreateProject(ProjectDTO obj)
        {
            var projectEntity = _mapper.Map<Project>(obj);
            var created = _projectRepo.Create(projectEntity);
            return _mapper.Map<ProjectDTO>(created);
        }

        public ProjectDTO UpdateProject(ProjectDTO obj)
        {
            var projectEntity = _mapper.Map<Project>(obj);
            var updated = _projectRepo.Update(projectEntity);
            return _mapper.Map<ProjectDTO>(updated);
        }

        public bool DeleteProject(int id)
        {
            return _projectRepo.Delete(id);
        }
    }
}
