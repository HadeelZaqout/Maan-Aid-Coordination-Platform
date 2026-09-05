using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Donations
{
    public class IndexModel : PageModel
    {
        private readonly IDonationRequestService _requestService;

        public IndexModel(IDonationRequestService requestService)
        {
            _requestService = requestService;
        }

        public List<DonationRequest> Requests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Requests = await _requestService.GetAvailableForDonationAsync();
            return Page();
        }
    }
}
