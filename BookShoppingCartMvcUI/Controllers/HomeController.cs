using jerseyShoppingCartMvcUI.Models;
using jerseyShoppingCartMvcUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace jerseyShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int categoryId=0)
        {
            IEnumerable<jersey> jerseys = await _homeRepository.Getjerseys(sterm, categoryId);
            IEnumerable<category> categorys = await _homeRepository.categorys();
            jerseyDisplayModel jerseyModel = new jerseyDisplayModel
            {
              jerseys=jerseys,
              categorys=categorys,
              STerm=sterm,
              categoryId=categoryId
            };
            return View(jerseyModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}