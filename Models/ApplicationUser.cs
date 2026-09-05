using Microsoft.AspNetCore.Identity;

namespace Donation.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public List<DonationRequest> DonationRequests { get; set; }
        public List<Donaation> Donations { get; set; }
        public List<StatusHistory> StatusHistories { get; set; }
        public List<Distribution> Distributions { get; set; }
    }
}
