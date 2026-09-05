using Donation.Models;

namespace Donation.Services
{
    public interface IDonationRequestService
    {
        Task<DonationRequest> CreateAsync(DonationRequest request);
        Task<DonationRequest?> GetByIdAsync(int id);
        Task<List<DonationRequest>> GetAllAsync();
        Task<DonationRequest?> UpdateAsync(DonationRequest request);
        Task<bool> DeleteAsync(int id);
        Task<List<DonationRequest>> GetByBeneficiaryIdAsync(string beneficiaryId);
        Task<List<DonationRequest>> GetAvailableForDonationAsync();

    }
}
