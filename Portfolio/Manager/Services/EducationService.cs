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
    public class EducationService
    {
        //private readonly IBaseRepo<Education> _educationRepo;
       // private readonly IMapper _mapper;

        private readonly IUnitOfWork _uow;

        // Dependency Injection 
        public EducationService(IUnitOfWork uow)
        {
            //_educationRepo = educationRepo;
            //_mapper = mapper;
            _uow = uow;
        }

        //-------------------------------------------------------------

        public List<EducationDTO> GetAllEducations()
        {
            var educations = _uow.education.GetAll();
            //return _mapper.Map<List<EducationDTO>>(educations);
            return educations.Select(edu => edu.ToMap<EducationDTO>()).ToList();
        }

        public List<EducationDTO> GetEducationsByUserId(int userId) 
        {
            var educations = _uow.education.Get_ByUserId().Where(u => u.UserId == userId);
            //return _mapper.Map<List<EducationDTO>>(educations);
            return educations.Select(edu => edu.ToMap<EducationDTO>()).ToList();
        }

        public EducationDTO GetEducationById(int id)
        {
            var education = _uow.education.GetById(id);
            //return _mapper.Map<EducationDTO>(education);
            return education.ToMap<EducationDTO>();
        }

        public EducationDTO CreateEducation(EducationDTO obj)
        {
            //var educationEntity = _mapper.Map<Education>(obj);
            var educationEntity = obj.ToMap<Education>();
            var created = _uow.education.Create(educationEntity); // this actually repo
            //_uow.save();
            //return _mapper.Map<EducationDTO>(created);
            return created.ToMap<EducationDTO>();
        }

        public EducationDTO UpdateEducation(EducationDTO obj)
        {
            //var educationEntity = _mapper.Map<Education>(obj);
            var educationEntity = obj.ToMap<Education>();
            var updated = _uow.education.Update(educationEntity);
            //_uow.save();
            //return _mapper.Map<EducationDTO>(updated);
            return updated.ToMap<EducationDTO>();
        }

        public bool DeleteEducation(int id)
        {
            var willBeDelete = _uow.education.Delete(id);
            if (willBeDelete)
            {
               // _uow.save();
                return true;
            }
            return false;
        }
    }
}
