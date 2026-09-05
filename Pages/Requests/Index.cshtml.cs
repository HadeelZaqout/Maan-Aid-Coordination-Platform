using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDonationRequestService _service;

        public IndexModel(UserManager<ApplicationUser> userManager,IDonationRequestService service)
        {
            _userManager = userManager;
            _service = service;
        }

        public List<DonationRequest> Mydonations { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
           
            Mydonations = await _service.GetByBeneficiaryIdAsync(user.Id);
            return Page();
        }
    }
}
