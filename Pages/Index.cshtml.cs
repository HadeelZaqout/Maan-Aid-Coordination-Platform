using Donation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Donation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDonationRequestService _requestService;
        private readonly IDonaationService _donaationService;

        public IndexModel(ILogger<IndexModel> logger,IDonationRequestService requestService,IDonaationService donaationService)
        {
            _logger = logger;
            _requestService = requestService;
            _donaationService = donaationService;
        }

        public int DeliveredNo { get; set; }
        public int MatchedNo { get; set; }
        public int DonorsNo { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var allRequests = await _requestService.GetAllAsync();
            DeliveredNo = allRequests.Count(s => s.Status == "Delivered");
            MatchedNo = allRequests.Count(s => s.Status == "Matched");
            var alldonors = await _donaationService.GetAllAsync();
            DonorsNo = alldonors.Select(d => d.DonorId).Distinct().Count();
            return Page();
        }
    }
}
