using Donation.Models;

namespace Donation.Services
{
    public interface IStatusHistoryService
    {
        Task<StatusHistory> CreateAsync(StatusHistory history);
        Task<List<StatusHistory>> GetByRequestIdAsync(int requestId);
    }
}
