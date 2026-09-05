using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Donations
{
    public class MyDonationsModel : PageModel
    {
        private readonly IDonaationService _donaationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyDonationsModel(IDonaationService donaationService,UserManager<ApplicationUser> userManager)
        {
            _donaationService = donaationService;
            _userManager = userManager;
        }
        public List<Donaation> MyDonationsList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            MyDonationsList = await _donaationService.GetByDonorIdAsync(user.Id);
            return Page();
        }
    }
}
