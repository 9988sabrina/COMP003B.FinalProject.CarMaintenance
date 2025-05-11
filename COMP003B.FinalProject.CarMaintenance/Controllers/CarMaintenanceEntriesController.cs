using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP003B.FinalProject.CarMaintenance.Data;
using COMP003B.FinalProject.CarMaintenance.Models;

namespace COMP003B.FinalProject.CarMaintenance.Controllers
{
    public class CarMaintenanceEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarMaintenanceEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarMaintenanceEntries
        public async Task<IActionResult> Index()
        {
            var entries = _context.CarMaintenanceEntries
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Mechanic)
                .Include(c => c.ServiceType);
            return View(await entries.ToListAsync());
        }

        // GET: CarMaintenanceEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var entry = await _context.CarMaintenanceEntries
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Mechanic)
                .Include(c => c.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entry == null) return NotFound();

            return View(entry);
        }

        // GET: CarMaintenanceEntries/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: CarMaintenanceEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceDate,Cost,Notes,CarId,CustomerId,MechanicId,ServiceTypeId")] CarMaintenanceEntry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(entry);
            return View(entry);
        }

        // GET: CarMaintenanceEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var entry = await _context.CarMaintenanceEntries.FindAsync(id);
            if (entry == null) return NotFound();

            PopulateDropdowns(entry);
            return View(entry);
        }

        // POST: CarMaintenanceEntries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceDate,Cost,Notes,CarId,CustomerId,MechanicId,ServiceTypeId")] CarMaintenanceEntry entry)
        {
            if (id != entry.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMaintenanceEntryExists(entry.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(entry);
            return View(entry);
        }

        // GET: CarMaintenanceEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var entry = await _context.CarMaintenanceEntries
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Mechanic)
                .Include(c => c.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entry == null) return NotFound();

            return View(entry);
        }

        // POST: CarMaintenanceEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.CarMaintenanceEntries.FindAsync(id);
            if (entry != null)
            {
                _context.CarMaintenanceEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Check if entry exists
        private bool CarMaintenanceEntryExists(int id)
        {
            return _context.CarMaintenanceEntries.Any(e => e.Id == id);
        }

        // Populate dropdowns
        private void PopulateDropdowns(CarMaintenanceEntry entry = null)
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Make", entry?.CarId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", entry?.CustomerId);
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "Name", entry?.MechanicId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Name", entry?.ServiceTypeId);
        }
    }
}
