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
    public class ExperienceService
    {
        //private readonly IBaseRepo<Experience> _experienceRepo;
        //private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        // Dependency Injection
        public ExperienceService(IUnitOfWork uow)
        {
            //_experienceRepo = experienceRepo;
           // _mapper = mapper;
            _uow = uow;
        }

        //--------------------------------------------------------------

        public List<ExperienceDTO> GetAllExperiences()
        {
            var experiences = _uow.experience.GetAll();
            //return _mapper.Map<List<ExperienceDTO>>(experiences);
            return experiences.Select(ex => ex.ToMap<ExperienceDTO>()).ToList();
        }

        public List<ExperienceDTO> GetExperiencesByUserId(int userId) 
        {
            var experiences = _uow.experience.Get_ByUserId().Where(u => u.UserId == userId);
            //return _mapper.Map<List<ExperienceDTO>>(experiences) ;
            return experiences.Select( ex => ex.ToMap<ExperienceDTO>()).ToList();
        }

        public ExperienceDTO GetExperienceById(int id)
        {
            var experience = _uow.experience.GetById(id);
            //return _mapper.Map<ExperienceDTO>(experience);
            return experience.ToMap<ExperienceDTO>();
        }

        public ExperienceDTO CreateExperience(ExperienceDTO obj)
        {
            //var experienceEntity = _mapper.Map<Experience>(obj);
            var experienceEntity = obj.ToMap<Experience>();
            var created = _uow.experience.Create(experienceEntity);
            //_uow.save();
           // return _mapper.Map<ExperienceDTO>(created);
           return created.ToMap<ExperienceDTO>();
        }

        public ExperienceDTO UpdateExperience(ExperienceDTO obj)
        {
            //var experienceEntity = _mapper.Map<Experience>(obj);
            var experienceEntity = obj.ToMap<Experience>();
            var updated = _uow.experience.Update(experienceEntity);
            //_uow.save();
            //return _mapper.Map<ExperienceDTO>(updated);
            return updated.ToMap<ExperienceDTO>();
        }

        public bool DeleteExperience(int id)
        {
            var willBeDelete =  _uow.experience.Delete(id);
            if(willBeDelete)
            {
                //_uow.save();
                return true;
            }
            return false;
        }


    }
}
