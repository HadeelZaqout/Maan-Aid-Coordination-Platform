using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Donation.Models
{
    public class StatusHistory
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedByUserId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Notes { get; set; }
        [ForeignKey("RequestId")]
        public DonationRequest Request { get; set; }
        [ForeignKey("ChangedByUserId")]
        public ApplicationUser ChangedByUser { get; set; }
    }
}
