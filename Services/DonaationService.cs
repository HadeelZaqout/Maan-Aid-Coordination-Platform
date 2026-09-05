using Donation.Data;
using Donation.Models;
using Microsoft.EntityFrameworkCore;

namespace Donation.Services
{
    public class DonaationService : IDonaationService
    {
        private readonly ApplicationDbContext _dbContext;

        public DonaationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Donaation>> GetAllAsync()
        {
            return await _dbContext.Donations.ToListAsync();
        }
        public async Task<Donaation> CreateAsync(Donaation donation)
        {
            _dbContext.Donations.Add(donation);
            await _dbContext.SaveChangesAsync();
            return donation;
        }

        public async Task<List<Donaation>> GetByDonorIdAsync(string donorId)
        {
            return await _dbContext.Donations.Where(d => d.DonorId ==donorId).ToListAsync();
        }

        public async Task<Donaation?> GetByIdAsync(int id)
        {
           return await _dbContext.Donations.FindAsync(id);        
        }

        public async Task<List<Donaation>> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.Donations.Where(r => r.RequestId == requestId).ToListAsync();
        }
    }
}
