using Microsoft.AspNetCore.Mvc;

namespace jerseyShoppingCartMvcUI.Controllers;
public class ReportsController : Controller
{
    private readonly IReportRepository _reportRepository;
    public ReportsController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    // GET: ReportsController
    public async Task<ActionResult> TopFiveSellingjerseys(DateTime? sDate = null, DateTime? eDate = null)
    {
        try
        {
            // by default, get last 7 days record
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime endDate = eDate ?? DateTime.UtcNow;
            var topFiveSellingjerseys = await _reportRepository.GetTopNSellingjerseysByDate(startDate, endDate);
            var vm = new TopNSoldjerseysVm(startDate, endDate, topFiveSellingjerseys);
            return View(vm);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Something went wrong";
            return RedirectToAction("Index", "Home");
        }
    }
}