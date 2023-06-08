using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Corporation
    {
        [Key]
        public int Id { get; set; }

     
        [StringLength(20)]
        public string CorporationName { get; set; }

        [StringLength(20)]
        public string CorporationOwnerName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int AddressNumber { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [StringLength(10)]
        public string ContactNumber { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
        [Required()]
        [DataType(DataType.Date)]
        public DateTime EstablishedYear { get; set; }

    }
}
