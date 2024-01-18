using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MAP_Ionescu_Serban_Andrei.Data;
using MAP_Ionescu_Serban_Andrei.Models;
using MAP_Ionescu_Serban_Andrei.Models.LibraryViewModels;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace MAP_Ionescu_Serban_Andrei.Controllers
{
    [Authorize(Policy = "OnlyVeterans")]
    public class MatchesController : Controller
    {
        private readonly BasketballContext _context;

        public MatchesController(BasketballContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index(int? id, int? ballerID)
        {
            var viewModel = new MatchIndexData();
            viewModel.Matches = await _context.Matches
                .Include(i => i.Positions)
                    .ThenInclude(i => i.Baller)
                        .ThenInclude(i => i.GamePlanes)
                            .ThenInclude(i => i.Coach)
                                .Include(i => i.Positions)
                                    .ThenInclude(i => i.Baller)
                                        .ThenInclude(i => i.Team)
                .AsNoTracking()
                .OrderBy(i => i.oppositeTeam)
                .ToListAsync();

            if (id != null)
            {
                ViewData["MatchID"] = id.Value;
                Match match = viewModel.Matches.Where(
                       m => m.MatchID == id.Value).Single();
                viewModel.Ballers = match.Positions.Select(b => b.Baller);
            }
            if (ballerID != null)
            {
                ViewData["BallerID"] = ballerID.Value;
                viewModel.GamePlans = viewModel.Ballers.Where(
                        x => x.BallerID == ballerID).Single().GamePlanes;
            }
            return View(viewModel);
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchID,oppositeTeam,minutesPlayed,markedPoints")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(i => i.Positions).ThenInclude(i => i.Baller)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }
            PopulateMatchesPlayedData(match);
            return View(match);
        }

        private void PopulateMatchesPlayedData(Match match)
        {
            var allBallers = _context.Ballers;
            var matchesPlayed = new HashSet<int>(match.Positions.Select(c => c.BallerID));
            var viewModel = new List<PositionData>();
            foreach (var baller in allBallers)
            {
                viewModel.Add(new PositionData
                {
                    BallerID = baller.BallerID,
                    BallerName = baller.BallerName,
                    hasPlayed = matchesPlayed.Contains(baller.BallerID)
                });
            }
            ViewData["Ballers"] = viewModel;
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedBallers)
        {
            if (id == null)
            {
                return NotFound();
            }
            var matchToUpdate = await _context.Matches
            .Include(i => i.Positions)
                .ThenInclude(i => i.Baller)
                    .FirstOrDefaultAsync(m => m.MatchID == id);

            if (await TryUpdateModelAsync<Match>(
                matchToUpdate,
                "",
                i => i.oppositeTeam, i => i.minutesPlayed, i => i.markedPoints))
            {
                UpdatePlayedBallers(selectedBallers, matchToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePlayedBallers(selectedBallers, matchToUpdate);
            PopulateMatchesPlayedData(matchToUpdate);
            return View(matchToUpdate);
        }

        private void UpdatePlayedBallers(string[] selectedBallers, Match matchToUpdate)
        {
            if (selectedBallers == null)
            {
                matchToUpdate.Positions = new List<Position>();
                return;
            }
            var selectedBallersHS = new HashSet<string>(selectedBallers);
            var playedBallers = new HashSet<int>
            (matchToUpdate.Positions.Select(c => c.Baller.BallerID));
            foreach (var baller in _context.Ballers)
            {
                if (selectedBallersHS.Contains(baller.BallerID.ToString()))
                {
                    if (!playedBallers.Contains(baller.BallerID))
                    {
                        matchToUpdate.Positions.Add(new Position
                        {
                            MatchID =
                        matchToUpdate.MatchID,
                            BallerID = baller.BallerID,
                        });
                    }
                }
                else
                {
                    if (playedBallers.Contains(baller.BallerID))
                    {
                        Position ballerToRemove = matchToUpdate.Positions.FirstOrDefault(i
                        => i.BallerID == baller.BallerID);
                        _context.Remove(ballerToRemove);
                    }
                }
            }
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'BasketballContext.Coaches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
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
