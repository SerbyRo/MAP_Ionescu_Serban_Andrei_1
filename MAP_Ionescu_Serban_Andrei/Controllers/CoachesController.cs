using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MAP_Ionescu_Serban_Andrei.Data;
using MAP_Ionescu_Serban_Andrei.Models;
using Newtonsoft.Json;
using System.Text;

namespace MAP_Ionescu_Serban_Andrei.Controllers
{
    public class CoachesController : Controller
    {
        private readonly BasketballContext _context;
        private string baseUrl = "http://localhost:5042/api/Coaches";

        public CoachesController(BasketballContext context)
        {
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var customers = JsonConvert.DeserializeObject<List<Coach>>(await
                response.Content.
                ReadAsStringAsync());
                return View(customers);
            }
            return NotFound();
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await _context.Coaches.AsNoTracking().Include(b => b.PersonalStats)
                    .FirstOrDefaultAsync(m => m.CoachID== id);
                return View(customer);
            }
            return NotFound();
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["PersonalStatsID"] = new SelectList(_context.PersonalStats, "PersonalStatsID", "Description");
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CoachID,PersonalStatsID,CoachName,CoachCountry,debutYear")] Coach coach)
        {
            ViewData["PersonalStatsID"] = new SelectList(_context.PersonalStats, "PersonalStatsID", "Description");
            if (!ModelState.IsValid) return View(coach);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(coach);
                var response = await client.PostAsync(baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ex.Message}");
            }
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["PersonalStatsID"] = new SelectList(_context.PersonalStats, "PersonalStatsID", "Description");
            if (id == null || _context.Coaches == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CoachID,PersonalStatsID,CoachName,CoachCountry,debutYear")] Coach coach)
        {
            if (id != coach.CoachID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.CoachID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalStatsID"] = new SelectList(_context.PersonalStats, "PersonalStatsID", "Description", coach.PersonalStatsID);
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coaches == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .Include(c => c.PersonalStats)
                .FirstOrDefaultAsync(m => m.CoachID == id);
            if (coach == null)
            {
                return NotFound();
            }

            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coaches == null)
            {
                return Problem("Entity set 'BasketballContext.Coaches'  is null.");
            }
            var coach = await _context.Coaches.FindAsync(id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoachExists(int id)
        {
          return (_context.Coaches?.Any(e => e.CoachID == id)).GetValueOrDefault();
        }
    }
}
