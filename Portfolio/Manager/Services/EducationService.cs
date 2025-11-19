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
    public class EducationService
    {
        private readonly IBaseRepo<Education> _educationRepo;
        private readonly IMapper _mapper;

        // Dependency Injection 
        public EducationService(IBaseRepo<Education> educationRepo, IMapper mapper)
        {
            _educationRepo = educationRepo;
            _mapper = mapper;
        }

        //-------------------------------------------------------------

        public List<EducationDTO> GetAllEducations()
        {
            var educations = _educationRepo.GetAll();
            return _mapper.Map<List<EducationDTO>>(educations);
        }

        public List<EducationDTO> GetEducationsByUserId(int userId) 
        {
            var educations = _educationRepo.Get_ByUserId(userId);
            return _mapper.Map<List<EducationDTO>>(educations);
        }

        public EducationDTO GetEducationById(int id)
        {
            var education = _educationRepo.GetById(id);
            return _mapper.Map<EducationDTO>(education);
        }

        public EducationDTO CreateEducation(EducationDTO obj)
        {
            var educationEntity = _mapper.Map<Education>(obj);
            var created = _educationRepo.Create(educationEntity);
            return _mapper.Map<EducationDTO>(created);
        }

        public EducationDTO UpdateEducation(EducationDTO obj)
        {
            var educationEntity = _mapper.Map<Education>(obj);
            var updated = _educationRepo.Update(educationEntity);
            return _mapper.Map<EducationDTO>(updated);
        }

        public bool DeleteEducation(int id)
        {
            return _educationRepo.Delete(id);
        }
    }
}
