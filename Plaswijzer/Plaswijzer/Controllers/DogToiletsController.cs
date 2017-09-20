using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plaswijzer.Data;
using Plaswijzer.Model;

namespace Plaswijzer.Controllers
{
    public class DogToiletsController : Controller
    {
        private readonly ToiletContext _context;

        public DogToiletsController(ToiletContext context)
        {
            _context = context;
        }

        // GET: DogToilets
        public async Task<IActionResult> Index()
        {
            return View(await _context.DogToilets.ToListAsync());
        }

        // GET: DogToilets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogToilet = await _context.DogToilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dogToilet == null)
            {
                return NotFound();
            }

            return View(dogToilet);
        }

        // GET: DogToilets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DogToilets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Situering,Lon,Lat")] DogToilet dogToilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dogToilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dogToilet);
        }

        // GET: DogToilets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogToilet = await _context.DogToilets.SingleOrDefaultAsync(m => m.ID == id);
            if (dogToilet == null)
            {
                return NotFound();
            }
            return View(dogToilet);
        }

        // POST: DogToilets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Situering,Lon,Lat")] DogToilet dogToilet)
        {
            if (id != dogToilet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dogToilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogToiletExists(dogToilet.ID))
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
            return View(dogToilet);
        }

        // GET: DogToilets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogToilet = await _context.DogToilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dogToilet == null)
            {
                return NotFound();
            }

            return View(dogToilet);
        }

        // POST: DogToilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dogToilet = await _context.DogToilets.SingleOrDefaultAsync(m => m.ID == id);
            _context.DogToilets.Remove(dogToilet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogToiletExists(string id)
        {
            return _context.DogToilets.Any(e => e.ID == id);
        }
    }
}
