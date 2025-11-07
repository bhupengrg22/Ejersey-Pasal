using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace jerseyShoppingCartMvcUI.Repositories;

[Authorize(Roles = nameof(Roles.Admin))]
public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;
    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TopNSoldjerseyModel>> GetTopNSellingjerseysByDate(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        var topFiveSoldjerseys = await _context.Database.SqlQueryRaw<TopNSoldjerseyModel>("exec Usp_GetTopNSellingjerseysByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return topFiveSoldjerseys;
    }

}

public interface IReportRepository
{
    Task<IEnumerable<TopNSoldjerseyModel>> GetTopNSellingjerseysByDate(DateTime startDate, DateTime endDate);
}