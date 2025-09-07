using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FULLSTACKFURY.EduSpace.API.BreakdownManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class ReportRepository(AppDbContext context) 
        : BaseRepository<Report>(context), IReportRepository
    {
        public async Task<Report> FindByIdAsync(int id)
        {
            return await Context.Set<Report>().FindAsync(id);
        }

        public async Task<IEnumerable<Report>> FindAllAsync()
        {
            return await Context.Set<Report>().ToListAsync();
        }

        public async Task<IEnumerable<Report>> FindAllByResourceIdAsync(int resourceId)
        {
            return await Context.Set<Report>().Where(f => f.ResourceId.Id == resourceId).ToListAsync();
        }
    }
}