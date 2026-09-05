using Donation.Data;
using Donation.Models;
using Donation.Services;
using Donation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Donations
{
    public class CreateModel : PageModel
    {
        private readonly IDonaationService _dservice;
        private readonly IDonationRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IDonaationService dservice, IDonationRequestService requestService, UserManager<ApplicationUser> userManager)
        {
            _dservice = dservice;
            _requestService = requestService;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateDonationViewModel DonationInput { get; set; }

        public DonationRequest? donationRequest { get; set; }
        public async Task<IActionResult> OnGetAsync(int requestId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            donationRequest = await _requestService.GetByIdAsync(requestId);
            if (donationRequest == null) { return NotFound(); }
            if (donationRequest.Status != "Verified") { return NotFound(); }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int requestId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Account/Login");
                }
                donationRequest = await _requestService.GetByIdAsync(requestId);
                if (donationRequest == null) { return NotFound(); }
                if (donationRequest.Status != "Verified") { return NotFound(); }
                var newDonation = new Donaation();
                newDonation.DonorId = user.Id;
                newDonation.RequestId = requestId;
                newDonation.Quantity = DonationInput.Quantity;
                newDonation.DonatedAt = DateTime.UtcNow;

                await _dservice.CreateAsync(newDonation);
                return RedirectToPage("/Donations/Index");
            }
            return Page();
        }
        
    }
}
