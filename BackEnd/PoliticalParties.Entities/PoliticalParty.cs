using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliticalParties.Entities
{
    public class PoliticalParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PoliticalPartyId { get; set; }
        public string Name { get; set; }
        public string Founder { get; set; }
        public bool IsDeleted { get; set; }
    }
}
