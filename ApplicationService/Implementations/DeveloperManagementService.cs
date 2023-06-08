using ApplicationService.DTOs;
using Data.Entities;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Implementations
{
    public class DeveloperManagementService
    {
        public List<DeveloperDTO> Get()
        {
            List<DeveloperDTO> developerDTO = new List<DeveloperDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.DevRepository.Get())
                {
                    developerDTO.Add(new DeveloperDTO
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Age = item.Age,
                        PhoneNumber = item.PhoneNumber,
                        Specialization = item.Specialization,
                        YearsOfExperiance= item.YearsOfExperiance,
                        MonthSalary= item.MonthSalary,
                        Corporation_Id = item.Corporation_Id
                    });
                }
            }

            return developerDTO;
        }

        public DeveloperDTO GetById(int id)
        {
            DeveloperDTO developerDTO = new DeveloperDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Developer developer = unitOfWork.DevRepository.GetByID(id);
                List<Game> games = unitOfWork.GameRepository.Get().ToList();
                List<Game> assignedGames = new List<Game>();
                if (developer != null)
                {
                    foreach (var item in games)
                    {
                        if (item.Developer_Id == developer.Id)
                        {
                            assignedGames.Add(item);
                        }
                    }

                    developerDTO = new DeveloperDTO
                    {
                        Id = developer.Id,
                        FirstName = developer.FirstName,
                        LastName = developer.LastName,
                        Age = developer.Age,
                        PhoneNumber = developer.PhoneNumber,
                        Specialization = developer.Specialization,
                        YearsOfExperiance= developer.YearsOfExperiance,
                        MonthSalary= developer.MonthSalary,
                        Corporation_Id = developer.Corporation_Id,
                        Games = assignedGames

                    };

                }
            }
            return developerDTO;
        }


        public bool Update(DeveloperDTO developerDTO)
        {
            if (!CorpCheck(developerDTO.Corporation_Id))
            {
                return false;
            }

            try
            {

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Developer developer = unitOfWork.DevRepository.GetByID(developerDTO.Id);

                    if (developer == null)
                    {
                        return false;
                    }

                    developer.FirstName = developerDTO.FirstName;
                    developer.LastName = developerDTO.LastName;
                    developer.Age = developerDTO.Age;
                    developer.PhoneNumber = developerDTO.PhoneNumber;
                    developer.Specialization = developerDTO.Specialization;
                    developer.YearsOfExperiance = developerDTO.YearsOfExperiance;
                    developer.MonthSalary= developerDTO.MonthSalary;
                    developer.Corporation_Id = developerDTO.Corporation_Id;


                    unitOfWork.DevRepository.Update(developer);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save(DeveloperDTO developerDTO)
        {
            if (!CorpCheck(developerDTO.Corporation_Id))
            {
                return false;
            }

            Developer developer = new Developer
            {
                FirstName = developerDTO.FirstName,
                LastName = developerDTO.LastName,
                Age = developerDTO.Age,
                PhoneNumber = developerDTO.PhoneNumber,
                Specialization = developerDTO.Specialization,
                YearsOfExperiance = developerDTO.YearsOfExperiance,
                MonthSalary = developerDTO.MonthSalary,
                Corporation_Id = developerDTO.Corporation_Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.DevRepository.Insert(developer);
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                Console.WriteLine(developer);
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Developer developer = unitOfWork.DevRepository.GetByID(id);
                    unitOfWork.DevRepository.Delete(developer);
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CorpCheck(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Corporation corporation = unitOfWork.CorporationRepository.GetByID(id);
                    if (corporation == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            { return false; }
        }
    }
}
