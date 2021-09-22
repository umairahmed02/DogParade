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
    public class WalkingGroupsController : Controller
    {
        private readonly DogParadeDatabaseContext _context;

        public WalkingGroupsController(DogParadeDatabaseContext context)
        {
            _context = context;
        }

        // GET: WalkingGroups
        public async Task<IActionResult> Index()
        {
            var dogParadeDatabaseContext = _context.WalkingGroups.Include(w => w.Dogs1).Include(w => w.WalkerNavigation);
            return View(await dogParadeDatabaseContext.ToListAsync());
        }

        // GET: WalkingGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroups
                .Include(w => w.Dogs1)
                .Include(w => w.WalkerNavigation)
                .FirstOrDefaultAsync(m => m.Gid == id);
            if (walkingGroup == null)
            {
                return NotFound();
            }

            return View(walkingGroup);
        }

        // GET: WalkingGroups/Create
        public IActionResult Create()
        {
            ViewData["Dogs"] = new SelectList(_context.Dogs, "Did", "Breed");
            ViewData["Walker"] = new SelectList(_context.Walkers, "Wid", "Name");
            return View();
        }

        // POST: WalkingGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Gid,Walker,Dogs,Time,DurationMins,MeetupLocation")] WalkingGroup walkingGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walkingGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Dogs"] = new SelectList(_context.Dogs, "Did", "Breed", walkingGroup.Dogs);
            ViewData["Walker"] = new SelectList(_context.Walkers, "Wid", "Name", walkingGroup.Walker);
            return View(walkingGroup);
        }

        // GET: WalkingGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroups.FindAsync(id);
            if (walkingGroup == null)
            {
                return NotFound();
            }
            ViewData["Dogs"] = new SelectList(_context.Dogs, "Did", "Breed", walkingGroup.Dogs);
            ViewData["Walker"] = new SelectList(_context.Walkers, "Wid", "Name", walkingGroup.Walker);
            return View(walkingGroup);
        }

        // POST: WalkingGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Gid,Walker,Dogs,Time,DurationMins,MeetupLocation")] WalkingGroup walkingGroup)
        {
            if (id != walkingGroup.Gid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walkingGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkingGroupExists(walkingGroup.Gid))
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
            ViewData["Dogs"] = new SelectList(_context.Dogs, "Did", "Breed", walkingGroup.Dogs);
            ViewData["Walker"] = new SelectList(_context.Walkers, "Wid", "Name", walkingGroup.Walker);
            return View(walkingGroup);
        }

        // GET: WalkingGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroups
                .Include(w => w.Dogs1)
                .Include(w => w.WalkerNavigation)
                .FirstOrDefaultAsync(m => m.Gid == id);
            if (walkingGroup == null)
            {
                return NotFound();
            }

            return View(walkingGroup);
        }

        // POST: WalkingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walkingGroup = await _context.WalkingGroups.FindAsync(id);
            _context.WalkingGroups.Remove(walkingGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkingGroupExists(int id)
        {
            return _context.WalkingGroups.Any(e => e.Gid == id);
        }
    }
}
