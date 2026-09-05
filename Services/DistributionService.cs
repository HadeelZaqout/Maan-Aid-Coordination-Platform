using Donation.Data;
using Donation.Models;
using Microsoft.EntityFrameworkCore;

namespace Donation.Services
{
    public class DistributionService : IDistributionService
    {
        private readonly ApplicationDbContext _dbContext;
        public DistributionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Distribution> CreateAsync(Distribution distribution)
        {
           _dbContext.Distributions.Add(distribution);
           await _dbContext.SaveChangesAsync();
           return distribution;
        }

        public async Task<Distribution?> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.Distributions.FirstOrDefaultAsync(d => d.RequestId == requestId);
        }
    }
}
