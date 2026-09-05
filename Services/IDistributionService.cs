using Donation.Models;

namespace Donation.Services
{
    public interface IDistributionService
    {
        Task<Distribution> CreateAsync(Distribution distribution);
        Task<Distribution?> GetByRequestIdAsync(int requestId);
    }
}
