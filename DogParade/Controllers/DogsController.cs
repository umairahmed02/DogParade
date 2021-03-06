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
    public class DogsController : Controller
    {
        private readonly DogParadeDatabaseContext _context;

        public DogsController(DogParadeDatabaseContext context)
        {
            _context = context;
        }

        // GET: Dogs
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewData["BreedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "breed_desc" : "breed_asc";
            ViewData["AgeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "age_desc" : "age_asc";
            ViewData["CurrentFilter"] = searchString;

            var dogs = from d in _context.Dogs
                        .Include(d => d.GroupNavigation)
                        select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    dogs = dogs.OrderByDescending(d => d.Name);
                    break;
                case "name_asc":
                    dogs = dogs.OrderBy(d => d.Name);
                    break;
                case "breed_desc":
                    dogs = dogs.OrderByDescending(d => d.Breed);
                    break;
                case "breed_asc":
                    dogs = dogs.OrderBy(d => d.Breed);
                    break;
                case "age_desc":
                    dogs = dogs.OrderByDescending(d => d.Age);
                    break;
                case "age_asc":
                    dogs = dogs.OrderBy(d => d.Age);
                    break;
                default:
                    dogs = dogs.OrderBy(d => d.Did);
                    break;
            }
            return View(await dogs.AsNoTracking().ToListAsync());
        }

            // GET: Dogs/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(d => d.GroupNavigation)
                .FirstOrDefaultAsync(m => m.Did == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: Dogs/Create
        public IActionResult Create()
        {
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation");
            return View();
        }

        // POST: Dogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Did,Name,Breed,Age,Notes,Group")] Dog dog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", dog.Group);
            return View(dog);
        }

        // GET: Dogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", dog.Group);
            return View(dog);
        }

        // POST: Dogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Did,Name,Breed,Age,Notes,Group")] Dog dog)
        {
            if (id != dog.Did)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogExists(dog.Did))
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
            ViewData["Group"] = new SelectList(_context.WalkingGroups, "Gid", "MeetupLocation", dog.Group);
            return View(dog);
        }

        // GET: Dogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(d => d.GroupNavigation)
                .FirstOrDefaultAsync(m => m.Did == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: Dogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogExists(int id)
        {
            return _context.Dogs.Any(e => e.Did == id);
        }
    }
}
