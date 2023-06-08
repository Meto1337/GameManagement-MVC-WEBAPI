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
    public class CorporationManagementService
    {
        public List<CorporationDTO> Get()
        {
            List<CorporationDTO> corporationDTO = new List<CorporationDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.CorporationRepository.Get())
                {
                    corporationDTO.Add(new CorporationDTO
                    {
                        Id = item.Id,
                        CorporationName = item.CorporationName,
                        CorporationOwnerName= item.CorporationOwnerName,
                        Address = item.Address,
                        AddressNumber = item.AddressNumber,
                        City = item.City,
                        ContactNumber = item.ContactNumber,
                        Email = item.Email,
                        EstablishedYear= item.EstablishedYear,
                    });
                }
            }
            return corporationDTO;

        }

        public CorporationDTO GetById(int id)
        {

            CorporationDTO corporationDTO = new CorporationDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Corporation corporation = unitOfWork.CorporationRepository.GetByID(id);
                List<Game> games = unitOfWork.GameRepository.Get().ToList();
                List<Game> assignedGames = new List<Game>();
                List<Developer> developers = unitOfWork.DevRepository.Get().ToList();
                List<Developer> assignedDevs = new List<Developer>();
                if (corporation != null)
                {

                    foreach (var item in games)
                    {
                        if (item.Corporation_Id == corporation.Id)
                        {
                            assignedGames.Add(item);
                        }
                    }
                    foreach (var item in developers)
                    {
                        if (item.Corporation_Id == corporation.Id)
                        {
                            assignedDevs.Add(item);
                        }
                    }
                    corporationDTO = new CorporationDTO
                    {
                        Id = corporation.Id,
                        CorporationName = corporation.CorporationName,
                        CorporationOwnerName = corporation.CorporationOwnerName,
                        Address = corporation.Address,
                        AddressNumber = corporation.AddressNumber,
                        City = corporation.City,
                        ContactNumber = corporation.ContactNumber,
                        Email = corporation.Email,
                        EstablishedYear = corporation.EstablishedYear,
                        Games = assignedGames,
                        Developers= assignedDevs,
                    };
                }

            }
            return corporationDTO;

        }

        public bool Update(CorporationDTO corporationDTO)
        {
            try
            {

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Corporation corporation = unitOfWork.CorporationRepository.GetByID(corporationDTO.Id);

                    if (corporation == null)
                    {
                        return false;
                    }

                    
                    corporation.CorporationName = corporationDTO.CorporationName;
                    corporation.CorporationOwnerName= corporationDTO.CorporationOwnerName; 
                    corporation.Address = corporationDTO.Address;
                    corporation.AddressNumber = corporationDTO.AddressNumber;
                    corporation.City = corporationDTO.City;
                    corporation.ContactNumber = corporationDTO.ContactNumber;
                    corporation.Email = corporationDTO.Email;
                    


                    unitOfWork.CorporationRepository.Update(corporation);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save(CorporationDTO corporationDTO)
        {

            Corporation corporation = new Corporation
            {
                CorporationName = corporationDTO.CorporationName,
                CorporationOwnerName = corporationDTO.CorporationOwnerName,
                Address = corporationDTO.Address,
                AddressNumber = corporationDTO.AddressNumber,
                City = corporationDTO.City,
                ContactNumber = corporationDTO.ContactNumber,
                Email = corporationDTO.Email,
                EstablishedYear = corporationDTO.EstablishedYear,
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    unitOfWork.CorporationRepository.Insert(corporation);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Corporation corporation = unitOfWork.CorporationRepository.GetByID(id);
                    unitOfWork.CorporationRepository.Delete(corporation);
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
