using Donation.Data;
using Donation.Models;
using Donation.Services;
using Donation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly IDonationRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public CreateDonationRequestViewModel NewRequest { get; set; }
        public CreateModel(IDonationRequestService requestService, UserManager<ApplicationUser> userManager)
        {
            _requestService = requestService;
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user == null)
                {
                    return RedirectToPage("/Account/Login");
                }
                var newDonationRequest = new DonationRequest();
                newDonationRequest.BeneficiaryId = user.Id;
                newDonationRequest.CreatedAt = DateTime.UtcNow;
                newDonationRequest.Status = "Submitted";
                newDonationRequest.Category= NewRequest.Category;
                newDonationRequest.FamilySize = NewRequest.FamilySize;
                newDonationRequest.UrgencyLevel= NewRequest.UrgencyLevel;
                newDonationRequest.Description = NewRequest.Description;
                await _requestService.CreateAsync(newDonationRequest);
                return RedirectToPage("/Index");
            }
            return Page();

        }
    }
}
