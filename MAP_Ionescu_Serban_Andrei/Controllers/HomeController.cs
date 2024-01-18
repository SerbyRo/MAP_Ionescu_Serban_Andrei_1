using MAP_Ionescu_Serban_Andrei.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MAP_Ionescu_Serban_Andrei.Data;
using MAP_Ionescu_Serban_Andrei.Models;
using MAP_Ionescu_Serban_Andrei.Models.LibraryViewModels;

namespace MAP_Ionescu_Serban_Andrei.Controllers
{
    public class HomeController : Controller
    {
        private readonly BasketballContext _context;

        public HomeController(BasketballContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> Statistics()
        {
            IQueryable<BallerGroup> data =
            from baller in _context.Ballers
            group baller by baller.TShirtNumber into dateGroup
            select new BallerGroup()
            {
                maxTShirtNumber = dateGroup.Key,
                BallerCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}