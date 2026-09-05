using Donation.Models;
using Donation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages.Coordinator
{
    [Authorize(Roles = "Coordinator")]
    public class ChangeStatusModel : PageModel
    {
        private readonly IDonationRequestService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStatusHistoryService _status;

        public ChangeStatusModel(IDonationRequestService service,UserManager<ApplicationUser> userManager,IStatusHistoryService status)
        {
            _service = service;
            _userManager = userManager;
            _status = status;
        }

        public DonationRequest? CurrentRequest { get; set; }
        public string? NextStatus { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            CurrentRequest = await _service.GetByIdAsync(id);
            if (CurrentRequest == null) return NotFound();
            NextStatus = GetNextStatus(CurrentRequest.Status);
            if (NextStatus == null) {return NotFound();}
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user== null) return RedirectToPage("/Account/Login");
            CurrentRequest = await _service.GetByIdAsync(id);
            if (CurrentRequest == null) return NotFound();
            NextStatus = GetNextStatus(CurrentRequest.Status);
            if (NextStatus == null) { return NotFound(); }
            string oldStatus = CurrentRequest.Status;
            CurrentRequest.Status = NextStatus;
            await  _service.UpdateAsync(CurrentRequest);

            var history = new StatusHistory
            {
                RequestId = id,
                OldStatus = oldStatus,
                NewStatus = NextStatus,
                ChangedByUserId = user.Id,
                ChangedAt = DateTime.UtcNow,
                Notes = ""
            };
            await _status.CreateAsync(history);
            return Page();
        }
        private string? GetNextStatus(string currentStatus) 
        {
            switch (currentStatus)
            {
                case "Submitted":
                    return "Verified";
                case "Verified":
                    return "Matched";
                case "Matched":
                    return "Delivered";
                case "Delivered":
                    return null;
                default:
                    return null;
            }
        
        }

    }

}

