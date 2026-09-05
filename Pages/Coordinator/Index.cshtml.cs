using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Coordinator
{
    [Authorize(Roles = "Coordinator")]
    public class IndexModel : PageModel
    {
        private readonly IDonationRequestService _donationRequest;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDistributionService _distributionService;

        public IndexModel(IDonationRequestService donationRequest,UserManager<ApplicationUser> userManager,IDistributionService distributionService)
        {
            _donationRequest = donationRequest;
            _userManager = userManager;
            _distributionService = distributionService;
        }
        public List<DonationRequest> AllRequests { get; set; }
        public List<int> DeliveredWithDistribution { get; set; } = new List<int>();
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            AllRequests = await _donationRequest.GetAllAsync();
            foreach (var request in AllRequests)
            {
                if(request.Status == "Delivered") 
                {
                    var sd = await _distributionService.GetByRequestIdAsync(request.Id);
                    if (sd != null) { DeliveredWithDistribution.Add(request.Id); }                
                }
            }
            return Page();
        }
    }
}
