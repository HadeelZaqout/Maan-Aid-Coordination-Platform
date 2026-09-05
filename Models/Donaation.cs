using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donation.Models
{
    public class Donaation
    {
        [Key]
        public int Id { get; set; }
       
        public string DonorId { get; set; }
       
        public int RequestId { get; set; }
        public int Quantity { get; set; }
        public DateTime DonatedAt { get; set; }
        [ForeignKey("DonorId")]
        public ApplicationUser Donor { get; set; }
        [ForeignKey("RequestId")]
        public DonationRequest DonationRequest { get; set; }

    }
}
