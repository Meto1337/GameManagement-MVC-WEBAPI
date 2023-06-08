using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Entities
{
    public class Game
    {
        [Key]
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
        public Developer Developer { get; set; }
        public int Corporation_Id { get; set; }
        public Corporation Corporation { get; set; }
    }
}
