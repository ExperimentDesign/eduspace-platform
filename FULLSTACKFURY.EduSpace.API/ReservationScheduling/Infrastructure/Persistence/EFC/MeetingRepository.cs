using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Infrastructure.Persistence.EFC;

public class MeetingRepository(AppDbContext context)
        : BaseRepository<Meeting>(context), IMeetingRepository
{
        public async Task<IEnumerable<Meeting>> FindAllByAdminIdAsync(int adminId)
        {
                return await Context.Set<Meeting>().Where(f =>f.AdministratorId.AdministratorIdentifier == adminId).ToListAsync();
        }
}