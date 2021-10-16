using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DogParade.Models;

namespace DogParade.Controllers
{
    public class WalkersController : Controller
    {
        private readonly DogParadeDatabaseContext _context;

        public WalkersController(DogParadeDatabaseContext context)
        {
            _context = context;
        }

        // GET: Walkers
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewData["AgeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "age_desc" : "age_asc";
            ViewData["CurrentFilter"] = searchString;
            var walker = from w in _context.Walkers
                       select w;
            if (!String.IsNullOrEmpty(searchString))
            {
                walker = walker.Where(w => w.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    walker = walker.OrderByDescending(w => w.Name);
                    break;
                case "name_asc":
                    walker = walker.OrderBy(w => w.Name);
                    break;
                case "age_desc":
                    walker = walker.OrderByDescending(w => w.Age);
                    break;
                case "age_asc":
                    walker = walker.OrderBy(w => w.Age);
                    break;
                default:
                    walker = walker.OrderBy(w => w.Wid);
                    break;

            }
            return View(await walker.AsNoTracking().ToListAsync());
        }

        // GET: Walkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walker = await _context.Walkers
                .Include(w => w.GroupNavigation)
                .FirstOrDefaultAsync(m => m.Wid == id);
            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // GET: Walkers/Create
        public IActionResult Create()
        {
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation");
            return View();
        }

        // POST: Walkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Wid,Name,Age,Group")] Walker walker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", walker.Group);
            return View(walker);
        }

        // GET: Walkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walker = await _context.Walkers.FindAsync(id);
            if (walker == null)
            {
                return NotFound();
            }
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", walker.Group);
            return View(walker);
        }

        // POST: Walkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Wid,Name,Age,Group")] Walker walker)
        {
            if (id != walker.Wid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkerExists(walker.Wid))
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
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", walker.Group);
            return View(walker);
        }

        // GET: Walkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walker = await _context.Walkers
                .Include(w => w.GroupNavigation)
                .FirstOrDefaultAsync(m => m.Wid == id);
            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // POST: Walkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walker = await _context.Walkers.FindAsync(id);
            _context.Walkers.Remove(walker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkerExists(int id)
        {
            return _context.Walkers.Any(e => e.Wid == id);
        }
    }
}
