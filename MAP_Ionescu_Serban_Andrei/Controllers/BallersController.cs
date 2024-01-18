using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MAP_Ionescu_Serban_Andrei.Data;
using MAP_Ionescu_Serban_Andrei.Models;
using Microsoft.AspNetCore.Authorization;

namespace MAP_Ionescu_Serban_Andrei.Controllers
{
    [Authorize(Roles ="Employee")]
    public class BallersController : Controller
    {
        private readonly BasketballContext _context;

        public BallersController(BasketballContext context)
        {
            _context = context;
        }

        // GET: Ballers
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TShirtNumberSortParm"] = sortOrder == "TShirtNumber" ? "tshirt_desc" : "TShirtNumber";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var ballers = _context.Ballers.Include(b => b.Team).AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                ballers = ballers.Where(b => b.BallerName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ballers = ballers.OrderByDescending(b => b.BallerName);
                    break;
                case "TShirtNumber":
                    ballers = ballers.OrderBy(b => b.TShirtNumber);
                    break;
                case "tshirt_desc":
                    ballers = ballers.OrderByDescending(b => b.TShirtNumber);
                    break;
                default:
                    ballers = ballers.OrderBy(b => b.BallerName);
                    break;
            }
            int pageSize = 2;
        //    var basketballContext = _context.Ballers.Include(b => b.Team).AsNoTracking();
            return View(await PaginatedList<Baller>.CreateAsync(ballers, pageNumber ?? 1, pageSize));
        }

        // GET: Ballers/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ballers == null)
            {
                return NotFound();
            }

            var baller = await _context.Ballers
                .Include(g => g.GamePlanes)
                .ThenInclude(c => c.Coach)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BallerID == id);
            if (baller == null)
            {
                return NotFound();
            }

            return View(baller);
        }

        // GET: Ballers/Create
        public IActionResult Create()
        {
            ViewData["TeamID"] = new SelectList(_context.Teams, "TeamID", "TeamID");
            return View();
        }

        // POST: Ballers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BallerName,TeamID,TShirtNumber")] Baller baller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(baller);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /*ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
"Try again, and if the problem persists ");
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "TeamID", "TeamID", baller.TeamID);
            return View(baller);
        }

        // GET: Ballers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ballers == null)
            {
                return NotFound();
            }

            var baller = await _context.Ballers.FindAsync(id);
            if (baller == null)
            {
                return NotFound();
            }
            ViewData["TeamID"] = new SelectList(_context.Teams, "TeamID", "TeamID", baller.TeamID);
            return View(baller);
        }

        // POST: Ballers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ballerToUpdate = await _context.Ballers.FirstOrDefaultAsync(b => b.BallerID == id);
            if (await TryUpdateModelAsync<Baller>(
                   ballerToUpdate,
                   "",
                   b => b.Team, b => b.BallerName, ballerToUpdate => ballerToUpdate.TShirtNumber))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /*ex*/) {
                    ModelState.AddModelError("", "Unable to save changes." + "Try again, and if the problem persists");
                }
            }
            return View(ballerToUpdate);
            
        }

        // GET: Ballers/Delete/5
        public async Task<IActionResult> Delete(int? id,bool? saveChangesError = false)
        {
            if (id == null || _context.Ballers == null)
            {
                return NotFound();
            }

            var baller = await _context.Ballers
                .Include(b => b.Team)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BallerID == id);
            if (baller == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(baller);
        }

        // POST: Ballers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ballers == null)
            {
                return Problem("Entity set 'BasketballContext.Ballers'  is null.");
            }
            var baller = await _context.Ballers.FindAsync(id);
            if (baller == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Ballers.Remove(baller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /*ex*/)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool BallerExists(int id)
        {
          return (_context.Ballers?.Any(e => e.BallerID == id)).GetValueOrDefault();
        }
    }
}
