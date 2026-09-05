namespace Donation.ViewModels
{
    public class EditDonationRequestViewModel
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public int FamilySize { get; set; }
        public int UrgencyLevel { get; set; }
    }
}
