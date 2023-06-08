using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace ApplicationService.DTOs
{
    public class CorporationDTO
    {
       
        public int Id { get; set; }

       
        [StringLength(20)]
        public string CorporationName { get; set; }

        [StringLength(20)]
        public string CorporationOwnerName { get; set; }

        public string Address { get; set; }
        public int AddressNumber { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(10)]
        public string ContactNumber { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime EstablishedYear { get; set; }
        public List<Game> Games { get; set; }
        public List<Developer> Developers { get; set; }
        
        public bool Validate()
        {
            return !String.IsNullOrEmpty(CorporationName);
        }
    }
}
