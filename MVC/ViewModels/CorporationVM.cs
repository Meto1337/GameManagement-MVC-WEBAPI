using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Data.Entities;
using ApplicationService.DTOs;

namespace MVC.ViewModels
{
    public class CorporationVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Името на корпорацията е задължително.")]
        [StringLength(20, ErrorMessage = "Името на корпорацията не може да бъде по-дълго от 20 символа.")]
        [Display(Name = "Име на корпорацията")]
        public string CorporationName { get; set; }

        [Required(ErrorMessage = "Името на собственика на корпорацията е задължително.")]
        [StringLength(20, ErrorMessage = "Името на собственика на корпорацията не може да бъде по-дълго от 20 символа.")]
        [Display(Name = "Име на собственика на корпорацията")]
        public string CorporationOwnerName { get; set; }

        [Required(ErrorMessage = "Адресът е задължителен.")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Номерът на адреса е задължителен.")]
        [Display(Name = "Номер на адреса")]
        public int AddressNumber { get; set; }

        [Required(ErrorMessage = "Градът на корпорацията е задължителен.")]
        [StringLength(20, ErrorMessage = "Името на града не може да бъде по-дълго от 20 символа.")]
        [Display(Name = "Град")]
        public string City { get; set; }

        [Required(ErrorMessage = "Контактният номер на корпорацията е задължителен.")]
        [StringLength(10, ErrorMessage = "Контактният номер не може да бъде по-дълъг от 10 символа.")]
        [Display(Name = "Контактен номер")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Имейлът на корпорацията е задължителен.")]
        [StringLength(60, ErrorMessage = "Имейл адресът не може да бъде по-дълъг от 60 символа.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Дата на основаване на корпорацията е задължителна.")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на основаване")]
        public DateTime EstablishedYear { get; set; }

        public List<Data.Entities.Game> Games { get; set; }
        public List<Data.Entities.Developer> Developers { get; set; }
        public CorporationVM() { }
        public CorporationVM(CorporationDTO corporationDTO)
        {
            CorporationName = corporationDTO.CorporationName;
            CorporationOwnerName = corporationDTO.CorporationOwnerName;
            Address = corporationDTO.Address;
            AddressNumber = corporationDTO.AddressNumber;
            City = corporationDTO.City;
            ContactNumber = corporationDTO.ContactNumber;
            Email = corporationDTO.Email;
            EstablishedYear = corporationDTO.EstablishedYear;
            Games = corporationDTO.Games;
            Developers = corporationDTO.Developers;
        }
    }
}