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
    public class ExperienceService
    {
        private readonly IBaseRepo<Experience> _experienceRepo;
        private readonly IMapper _mapper;

        // Dependency Injection
        public ExperienceService(IBaseRepo<Experience> experienceRepo, IMapper mapper)
        {
            _experienceRepo = experienceRepo;
            _mapper = mapper;
        }

        //--------------------------------------------------------------

        public List<ExperienceDTO> GetAllExperiences()
        {
            var experiences = _experienceRepo.GetAll();
            return _mapper.Map<List<ExperienceDTO>>(experiences);
        }

        public List<ExperienceDTO> GetExperiencesByUserId(int userId) 
        {
            var experiences = _experienceRepo.Get_ByUserId(userId);
            return _mapper.Map<List<ExperienceDTO>>(experiences) ;
        }

        public ExperienceDTO GetExperienceById(int id)
        {
            var experience = _experienceRepo.GetById(id);
            return _mapper.Map<ExperienceDTO>(experience);
        }

        public ExperienceDTO CreateExperience(ExperienceDTO obj)
        {
            var experienceEntity = _mapper.Map<Experience>(obj);
            var created = _experienceRepo.Create(experienceEntity);
            return _mapper.Map<ExperienceDTO>(created);
        }

        public ExperienceDTO UpdateExperience(ExperienceDTO obj)
        {
            var experienceEntity = _mapper.Map<Experience>(obj);
            var updated = _experienceRepo.Update(experienceEntity);
            return _mapper.Map<ExperienceDTO>(updated);
        }

        public bool DeleteExperience(int id)
        {
            return _experienceRepo.Delete(id);
        }


    }
}
