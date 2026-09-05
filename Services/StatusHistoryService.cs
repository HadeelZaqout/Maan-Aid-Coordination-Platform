using Donation.Data;
using Donation.Models;
using Microsoft.EntityFrameworkCore;

namespace Donation.Services
{
    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public StatusHistoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StatusHistory> CreateAsync(StatusHistory history)
        {
            await _dbContext.StatusHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync();
            return history;
        }

        public async Task<List<StatusHistory>> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.StatusHistories.Where(s=>s.RequestId == requestId).ToListAsync();
        }
    }
}
