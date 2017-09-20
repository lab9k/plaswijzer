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
    public class ToiletsController : Controller
    {
        private readonly ToiletContext _context;

        public ToiletsController(ToiletContext context)
        {
            _context = context;
        }

        // GET: Toilets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Toilets.ToListAsync());
        }

        // GET: Toilets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilet = await _context.Toilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (toilet == null)
            {
                return NotFound();
            }

            return View(toilet);
        }

        // GET: Toilets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Toilets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat,Type")] Toilet toilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toilet);
        }

        // GET: Toilets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilet = await _context.Toilets.SingleOrDefaultAsync(m => m.ID == id);
            if (toilet == null)
            {
                return NotFound();
            }
            return View(toilet);
        }

        // POST: Toilets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Situering,Open7op7,Openuren,Gratis,Type_locat,Lon,Lat,Type")] Toilet toilet)
        {
            if (id != toilet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToiletExists(toilet.ID))
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
            return View(toilet);
        }

        // GET: Toilets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilet = await _context.Toilets
                .SingleOrDefaultAsync(m => m.ID == id);
            if (toilet == null)
            {
                return NotFound();
            }

            return View(toilet);
        }

        // POST: Toilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var toilet = await _context.Toilets.SingleOrDefaultAsync(m => m.ID == id);
            _context.Toilets.Remove(toilet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToiletExists(string id)
        {
            return _context.Toilets.Any(e => e.ID == id);
        }
    }
}
