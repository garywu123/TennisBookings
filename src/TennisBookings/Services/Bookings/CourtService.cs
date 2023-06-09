namespace TennisBookings.Services.Bookings;

public class CourtService : ICourtService
{
	private readonly TennisBookingsDbContext _dbContext;
	private readonly IAuditor<CourtService> _auditor;

	public CourtService(TennisBookingsDbContext dbContext, IAuditor<CourtService> auditor)
	{
		_dbContext = dbContext;
		_auditor = auditor;
	}

	public async Task<IEnumerable<Court>> GetOutdoorCourts() =>
		await GetQueryableCourts().Where(c => c.Type == CourtType.Outdoor).ToListAsync();

	public async Task<HashSet<int>> GetCourtIds()
	{
		_auditor.RecordAction("Test");

		var ids = await GetQueryableCourts().Select(c => c.Id).OrderBy(c => c).ToListAsync();
		return ids.ToHashSet();
	}

	private IQueryable<Court> GetQueryableCourts() => _dbContext.Courts!.AsNoTracking();
}
