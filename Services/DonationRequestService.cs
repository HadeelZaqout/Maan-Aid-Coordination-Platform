using Donation.Data;
using Donation.Models;
using Microsoft.EntityFrameworkCore;

namespace Donation.Services
{
    public class DonationRequestService : IDonationRequestService
    {
        private readonly ApplicationDbContext _dbContext;

        public DonationRequestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DonationRequest> CreateAsync(DonationRequest request)
        {
            _dbContext.DonationsRequests.Add(request);
            await  _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DonationRequest? del = await GetByIdAsync(id);
            if(del != null)
            {
                _dbContext.DonationsRequests.Remove(del);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<DonationRequest>> GetAllAsync()
        {
            return await _dbContext.DonationsRequests.ToListAsync();
        }

        public async Task<List<DonationRequest>> GetByBeneficiaryIdAsync(string beneficiaryId)
        {
            return await _dbContext.DonationsRequests.Where(r =>r.BeneficiaryId==beneficiaryId).ToListAsync();
        }

        public async Task<List<DonationRequest>> GetAvailableForDonationAsync()
        {
            return await _dbContext.DonationsRequests.Where(s => s.Status== "Verified").ToListAsync();
        }

        public async Task<DonationRequest?> GetByIdAsync(int id)
        {
            return await _dbContext.DonationsRequests.FindAsync(id);
        }

        public async Task<DonationRequest?> UpdateAsync(DonationRequest request)
        {
           DonationRequest? req = await GetByIdAsync(request.Id);
           if(req != null)
            {
               req.Category = request.Category;
               req.Status = request.Status;
               req.CreatedAt = request.CreatedAt;
               req.UrgencyLevel = request.UrgencyLevel;
               req.FamilySize = request.FamilySize;
               req.Description = request.Description;
                await _dbContext.SaveChangesAsync();
                return req;
            }
           return null; 
        }

       
    }
}
