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
    public class GehandToiletsController : Controller
    {
        private readonly ToiletContext _context;

        public GehandToiletsController(ToiletContext context)
        {
            _context = context;
        }

        // GET: GehandToilets
        public async Task<IActionResult> Index()
        {
            return View(await _context.GehandToilets.ToListAsync());
        }

        // GET: GehandToilets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gehandToilet = await _context.GehandToilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (gehandToilet == null)
            {
                return NotFound();
            }

            return View(gehandToilet);
        }

        // GET: GehandToilets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GehandToilets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat")] GehandToilet gehandToilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gehandToilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gehandToilet);
        }

        // GET: GehandToilets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gehandToilet = await _context.GehandToilets.SingleOrDefaultAsync(m => m.ID == id);
            if (gehandToilet == null)
            {
                return NotFound();
            }
            return View(gehandToilet);
        }

        // POST: GehandToilets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat")] GehandToilet gehandToilet)
        {
            if (id != gehandToilet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gehandToilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GehandToiletExists(gehandToilet.ID))
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
            return View(gehandToilet);
        }

        // GET: GehandToilets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gehandToilet = await _context.GehandToilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (gehandToilet == null)
            {
                return NotFound();
            }

            return View(gehandToilet);
        }

        // POST: GehandToilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var gehandToilet = await _context.GehandToilets.SingleOrDefaultAsync(m => m.ID == id);
            _context.GehandToilets.Remove(gehandToilet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GehandToiletExists(string id)
        {
            return _context.GehandToilets.Any(e => e.ID == id);
        }
    }
}
