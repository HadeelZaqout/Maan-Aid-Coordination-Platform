using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donation.Models
{
    public class DonationRequest
    {
        [Key]
        public int Id { get; set; }
        
        public string BeneficiaryId { get; set; }
        public string Category {  get; set; }
        public string Description { get; set; }
        public int FamilySize { get; set; }
        public int UrgencyLevel { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("BeneficiaryId")]
        public ApplicationUser Beneficiary { get; set; }  // navigation property

        public List<Donaation> Donations { get; set; }
        public List<StatusHistory> StatusHistory { get; set; }

    }
}
