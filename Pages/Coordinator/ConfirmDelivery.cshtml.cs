using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Coordinator
{
    [Authorize(Roles = "Coordinator")]
    public class ConfirmDeliveryModel : PageModel
    {
        private readonly IDonationRequestService _requestService;
        private readonly IDistributionService _distributionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmDeliveryModel(IDonationRequestService requestService,IDistributionService distributionService,UserManager<ApplicationUser> userManager)
        {
            _requestService = requestService;
            _distributionService = distributionService;
            _userManager = userManager;
        }

        [BindProperty]
        public string ConfirmationNotes {  get; set; }
        public DonationRequest? CurrentRequest { get; set; }
        public async  Task<IActionResult> OnGetAsync(int id)
        {
            CurrentRequest = await _requestService.GetByIdAsync(id);
            if (CurrentRequest == null) return NotFound();

            if (CurrentRequest.Status != "Delivered") return NotFound();

            var existingDistribution = await _distributionService.GetByRequestIdAsync(id);
            if (existingDistribution != null) return NotFound(); 

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            CurrentRequest = await _requestService.GetByIdAsync(id);
            if (CurrentRequest == null) return NotFound();
            if (CurrentRequest.Status != "Delivered") return NotFound();

            var existingDistribution = await _distributionService.GetByRequestIdAsync(id);
            if (existingDistribution != null) return NotFound();

            var newDistribution = new Distribution
            {
                RequestId = id,
                CoordinatorId = user.Id,
                DeliveredAt = DateTime.UtcNow,
                ConfirmationNotes = ConfirmationNotes
            };

            await _distributionService.CreateAsync(newDistribution);
            return RedirectToPage("/Requests/Details", new { id = id }); 
        }

    }
}
