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
    public class DeveloperVM
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Името е задължително.")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Възрастта е задължителна.")]
        [Range(1, 150, ErrorMessage = "Възрастта трябва да е между 1 и 150.")]
        [Display(Name = "Възраст")]
        public int Age { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Специализацията е задължителна.")]
        [Display(Name = "Специализация")]
        public string Specialization { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Годините опит са задължителни.")]
        [Display(Name = "Години опит")]
        public int? YearsOfExperiance { get; set; }

        [Required(ErrorMessage = "Месечната заплата е задължителна.")]
        [Display(Name = "Месечна заплата")]
        public double? MonthSalary { get; set; }

        [Required(ErrorMessage = "Идентификаторът на корпорацията е задължителен.")]
        [Display(Name = "Корпорация")]
        public int Corporation_Id { get; set; }

        public List<Data.Entities.Game> Games { get; set; }

        public DeveloperVM() { }

        public DeveloperVM(DeveloperDTO developerDTO)
        {
            Id = developerDTO.Id;
            FirstName = developerDTO.FirstName;
            LastName = developerDTO.LastName;
            Age = developerDTO.Age;
            Specialization = developerDTO.Specialization;
            PhoneNumber = developerDTO.PhoneNumber;
            YearsOfExperiance = developerDTO.YearsOfExperiance;
            MonthSalary = developerDTO.MonthSalary;
            Corporation_Id =  developerDTO.Corporation_Id;

            Games = developerDTO.Games;
            
        }


    }
}