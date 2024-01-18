using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MAP_Ionescu_Serban_Andrei.Data;
using MAP_Ionescu_Serban_Andrei.Models;

namespace BasketWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly BasketballContext _context;

        public CoachesController(BasketballContext context)
        {
            _context = context;
        }

        // GET: api/Coaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoaches()
        {
            if (_context.Coaches == null)
            {
                return NotFound();
            }
            return await _context.Coaches.Include(i => i.PersonalStats).ToListAsync();
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coach>> GetCoach(int id)
        {
            if (_context.Coaches == null)
            {
                return NotFound();
            }
            var coach = await _context.Coaches.AsNoTracking().FirstOrDefaultAsync(c => c.CoachID == id);
            var personalStats = await _context.PersonalStats.AsNoTracking().FirstOrDefaultAsync(c => c.PersonalStatsID == coach.PersonalStatsID);

            coach.PersonalStats = personalStats;

            if (coach == null)
            {
                return NotFound();
            }

            return coach;
        }

        // PUT: api/Coaches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            // var city = await _context.Cities.FirstOrDefaultAsync(c => customer.CityID == c.ID);
            //  customer.City = city;
            if (id != coach.CoachID)
            {
                return BadRequest();
            }

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coaches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coach>> PostCoach(Coach coach)
        {
            //    var city = await _context.Cities.FirstOrDefaultAsync(c => customer.CityID==c.ID);
            //   customer.City = city;
            if (_context.Coaches == null)
            {
                return Problem("Entity set 'LibraryContext.Customers'  is null.");
            }
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = coach.CoachID }, coach);
        }

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            if (_context.Coaches == null)
            {
                return NotFound();
            }
            var customer = await _context.Coaches.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachExists(int id)
        {
            return (_context.Coaches?.Any(e => e.CoachID == id)).GetValueOrDefault();
        }
    }
}
