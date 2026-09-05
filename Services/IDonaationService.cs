using Donation.Models;

namespace Donation.Services
{
    public interface IDonaationService
    {
        Task<Donaation> CreateAsync(Donaation donation);
        Task<Donaation?> GetByIdAsync(int id);
        Task<List<Donaation>> GetByDonorIdAsync(string donorId);      
        Task<List<Donaation>> GetByRequestIdAsync(int requestId); 
        Task<List<Donaation>> GetAllAsync();
    }
}
