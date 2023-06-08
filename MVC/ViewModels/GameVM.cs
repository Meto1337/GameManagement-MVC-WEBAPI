using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ApplicationService.DTOs;
using Data.Entities;

namespace MVC.ViewModels
{
    public class GameVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието на играта е задължително.")]
        [StringLength(28, MinimumLength = 3, ErrorMessage = "Заглавието на играта трябва да бъде между 3 и 28 символа.")]
        [Display(Name = "Игра:")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "Описанието трябва да е под 300 символа.")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Категорията на играта е задължителна.")]
        [StringLength(28, MinimumLength = 3, ErrorMessage = "Категорията на играта трябва да бъде между 3 и 28 символа.")]
        [Display(Name = "Категория: ")]
        public string Category { get; set; }

        [Display(Name = "Дата на публикуване")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Последно обновено на")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Активна")]
        public bool IsActive { get; set; }

        [Url(ErrorMessage = "Полето трябва да съдържа валиден URL адрес.")]
        [Display(Name = "Линк към снимка")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Идентификаторът на разработчика е задължителен.")]
        public int Developer_Id { get; set; }

        [Required(ErrorMessage = "Идентификаторът на корпорацията е задължителен.")]
        public int Corporation_Id { get; set; }

        public GameVM()
        {

        }
        public GameVM(GameDTO gameDTO)
        {
            Id= gameDTO.Id;
            Title = gameDTO.Title;
            Description = gameDTO.Description;
            Category= gameDTO.Category;
            PublishDate = gameDTO.PublishDate;
            UpdatedOn = gameDTO.UpdatedOn;
            IsActive = gameDTO.IsActive;
            ImageUrl= gameDTO.ImageUrl;
            Developer_Id = gameDTO.Developer_Id;
            Corporation_Id = gameDTO.Corporation_Id;
        }
    }
}