using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; } 
        [StringLength(28)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
        [StringLength(28)]
        public string Category { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat()]
        public DateTime PublishDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat()]
        public DateTime? UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public int Developer_Id { get; set; }
        public DeveloperDTO Developer { get; set; }
        public int Corporation_Id { get; set; }
        public CorporationDTO Corporation { get; set; }
        public List<Corporation> Corporations { get; set; }
        public List<Developer> Developers { get; set; }
        public bool Validate()
        {
            return !String.IsNullOrEmpty(Title);
        }
    }
}
