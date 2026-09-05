using Donation.Models;
using Donation.Services;
using Donation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Donation.Pages.Requests
{
    public class EditModel : PageModel
    {
        private readonly IDonationRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IDonationRequestService requestService, UserManager<ApplicationUser> userManager)
        {
            _requestService = requestService;
            _userManager = userManager;
        }
        [BindProperty]
        public EditDonationRequestViewModel EditInput {  get; set; }
        public DonationRequest? DonationById { get; set; }
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
            if(DonationById.Status != "Submitted") return Forbid();
            EditInput = new EditDonationRequestViewModel();
            EditInput.Category = DonationById.Category;
            EditInput.Description = DonationById.Description;
            EditInput.UrgencyLevel = DonationById.UrgencyLevel;
            EditInput.FamilySize = DonationById.FamilySize;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Account/Login");
                }
                DonationById = await _requestService.GetByIdAsync(id);
                if (DonationById == null) return NotFound();
                if (DonationById.BeneficiaryId != user.Id) return NotFound();
                if (DonationById.Status != "Submitted") return Forbid();
                DonationById.Category = EditInput.Category;
                DonationById.FamilySize = EditInput.FamilySize;
                DonationById.UrgencyLevel = EditInput.UrgencyLevel;
                DonationById.Description = EditInput.Description;
                await _requestService.UpdateAsync(DonationById);
                return RedirectToPage("/Requests/Details", new { id = id });
            }
            return Page();

        }
    }
}

