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
    public class UrinoirsController : Controller
    {
        private readonly ToiletContext _context;

        public UrinoirsController(ToiletContext context)
        {
            _context = context;
        }

        // GET: Urinoirs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Urinoirs.ToListAsync());
        }

        // GET: Urinoirs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urinoir = await _context.Urinoirs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (urinoir == null)
            {
                return NotFound();
            }

            return View(urinoir);
        }

        // GET: Urinoirs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Urinoirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat")] Urinoir urinoir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urinoir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urinoir);
        }

        // GET: Urinoirs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urinoir = await _context.Urinoirs.SingleOrDefaultAsync(m => m.ID == id);
            if (urinoir == null)
            {
                return NotFound();
            }
            return View(urinoir);
        }

        // POST: Urinoirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat")] Urinoir urinoir)
        {
            if (id != urinoir.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urinoir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrinoirExists(urinoir.ID))
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
            return View(urinoir);
        }

        // GET: Urinoirs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urinoir = await _context.Urinoirs
                .SingleOrDefaultAsync(m => m.ID == id);
            if (urinoir == null)
            {
                return NotFound();
            }

            return View(urinoir);
        }

        // POST: Urinoirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var urinoir = await _context.Urinoirs.SingleOrDefaultAsync(m => m.ID == id);
            _context.Urinoirs.Remove(urinoir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrinoirExists(string id)
        {
            return _context.Urinoirs.Any(e => e.ID == id);
        }
    }
}
