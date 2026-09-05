using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donation.Models
{
    public class Distribution
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string CoordinatorId { get; set; }
        public DateTime DeliveredAt { get; set; }
        public string ConfirmationNotes { get; set; }

        [ForeignKey("RequestId")]
        public DonationRequest Request { get; set; }
        [ForeignKey("CoordinatorId")]
        public ApplicationUser Coordinator { get; set; }
    }
}
