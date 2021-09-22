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
        public async Task<IActionResult> Index()
        {
            var dogParadeDatabaseContext = _context.Walkers.Include(w => w.GroupNavigation);
            return View(await dogParadeDatabaseContext.ToListAsync());
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
