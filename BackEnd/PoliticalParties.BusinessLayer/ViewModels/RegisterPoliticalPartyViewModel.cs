using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliticalParties.BusinessLayer.ViewModels
{
    public class RegisterPoliticalPartyViewModel
    {
        [Key]
        public long PoliticalPartyId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Minimum 3 Maximum 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Minimum 3 Maximum 100 characters")]
        public string Founder { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
