using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MplusAspCore.Data;
using MplusAspCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace MplusAspCore.Controllers
{
    public class WowClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WowClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WowClasses
        public async Task<IActionResult> Index()
        {
              return _context.WowClass != null ? 
                          View(await _context.WowClass.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.WowClass'  is null.");
        }
        // GET: WowClasses/search
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.WowClass != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.WowClass'  is null.");
        }

        // PoST: WowClasses/showResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return _context.WowClass != null ?
                        View("Index", await _context.WowClass.Where(j => j.classDescription.Contains(SearchPhrase)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.WowClass'  is null.");
        }

        // GET: WowClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WowClass == null)
            {
                return NotFound();
            }

            var wowClass = await _context.WowClass
                .FirstOrDefaultAsync(m => m.id == id);
            if (wowClass == null)
            {
                return NotFound();
            }

            return View(wowClass);
        }

        // GET: WowClasses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WowClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,className,classDescription")] WowClass wowClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wowClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wowClass);
        }

        // GET: WowClasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WowClass == null)
            {
                return NotFound();
            }

            var wowClass = await _context.WowClass.FindAsync(id);
            if (wowClass == null)
            {
                return NotFound();
            }
            return View(wowClass);
        }

        // POST: WowClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,className,classDescription")] WowClass wowClass)
        {
            if (id != wowClass.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wowClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WowClassExists(wowClass.id))
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
            return View(wowClass);
        }

        // GET: WowClasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WowClass == null)
            {
                return NotFound();
            }

            var wowClass = await _context.WowClass
                .FirstOrDefaultAsync(m => m.id == id);
            if (wowClass == null)
            {
                return NotFound();
            }

            return View(wowClass);
        }

        // POST: WowClasses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WowClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WowClass'  is null.");
            }
            var wowClass = await _context.WowClass.FindAsync(id);
            if (wowClass != null)
            {
                _context.WowClass.Remove(wowClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WowClassExists(int id)
        {
          return (_context.WowClass?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
