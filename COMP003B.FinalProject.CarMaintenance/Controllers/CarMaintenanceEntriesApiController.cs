using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP003B.FinalProject.CarMaintenance.Data;
using COMP003B.FinalProject.CarMaintenance.Models;

namespace COMP003B.FinalProject.CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMaintenanceEntriesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarMaintenanceEntriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarMaintenanceEntriesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarMaintenanceEntry>>> GetCarMaintenanceEntries()
        {
            return await _context.CarMaintenanceEntries.ToListAsync();
        }

        // GET: api/CarMaintenanceEntriesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarMaintenanceEntry>> GetCarMaintenanceEntry(int id)
        {
            var carMaintenanceEntry = await _context.CarMaintenanceEntries.FindAsync(id);

            if (carMaintenanceEntry == null)
            {
                return NotFound();
            }

            return carMaintenanceEntry;
        }

        // PUT: api/CarMaintenanceEntriesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarMaintenanceEntry(int id, CarMaintenanceEntry carMaintenanceEntry)
        {
            if (id != carMaintenanceEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(carMaintenanceEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarMaintenanceEntryExists(id))
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

        // POST: api/CarMaintenanceEntriesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarMaintenanceEntry>> PostCarMaintenanceEntry(CarMaintenanceEntry carMaintenanceEntry)
        {
            _context.CarMaintenanceEntries.Add(carMaintenanceEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarMaintenanceEntry", new { id = carMaintenanceEntry.Id }, carMaintenanceEntry);
        }

        // DELETE: api/CarMaintenanceEntriesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarMaintenanceEntry(int id)
        {
            var carMaintenanceEntry = await _context.CarMaintenanceEntries.FindAsync(id);
            if (carMaintenanceEntry == null)
            {
                return NotFound();
            }

            _context.CarMaintenanceEntries.Remove(carMaintenanceEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarMaintenanceEntryExists(int id)
        {
            return _context.CarMaintenanceEntries.Any(e => e.Id == id);
        }
    }
}
