using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly IDonationRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDonaationService _donaation;
        private readonly IStatusHistoryService _historyService;

        public DonationRequest? DonationById { get; set; }

        public List<Donaation> Donors { get; set; }
        public DetailsModel(IDonationRequestService requestService, UserManager<ApplicationUser> userManager,IDonaationService donaation,IStatusHistoryService historyService)
        {
            _requestService = requestService;
            _userManager = userManager;
            _donaation = donaation;
            _historyService = historyService;
        }

        public List<StatusHistory> History { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            DonationById = await _requestService.GetByIdAsync(id);
            if (DonationById == null) return NotFound();
            if (DonationById.BeneficiaryId != user.Id) return NotFound();
            Donors = await _donaation.GetByRequestIdAsync(id);
            History = await _historyService.GetByRequestIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            var donation = await _requestService.GetByIdAsync(id);
            if (donation == null) return NotFound();
            if (donation.BeneficiaryId != user.Id) return NotFound();
            if (donation.Status != "Submitted") return Forbid();

            await _requestService.DeleteAsync(id);
            return RedirectToPage("/Requests/Index");
        }
    }
}
