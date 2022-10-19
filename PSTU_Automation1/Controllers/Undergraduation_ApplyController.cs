using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PSTU_Automation1.Data;
using PSTU_Automation1.Models;

namespace PSTU_Automation1.Controllers
{
    public class Undergraduation_ApplyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Undergraduation_ApplyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Undergraduation_Apply
        public async Task<IActionResult> Index()
        {
            return View(await _context.Undergraduation_Apply.ToListAsync());
        }

        // GET: Undergraduation_Apply/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var undergraduation_Apply = await _context.Undergraduation_Apply
                .FirstOrDefaultAsync(m => m.ID == id);
            if (undergraduation_Apply == null)
            {
                return NotFound();
            }

            return View(undergraduation_Apply);
        }

        // GET: Undergraduation_Apply/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Undergraduation_Apply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Father_Name,Mother_Name,Gender,Nationality,Address,PostCode,Contact,Email,Unit")] Undergraduation_Apply undergraduation_Apply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(undergraduation_Apply);
                await _context.SaveChangesAsync();
                return RedirectToAction("ApplyPreview", "Reports", new { ID = undergraduation_Apply.ID });
            }
            return View(undergraduation_Apply);
        }
           
        

        // GET: Undergraduation_Apply/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var undergraduation_Apply = await _context.Undergraduation_Apply.FindAsync(id);
            if (undergraduation_Apply == null)
            {
                return NotFound();
            }
            return View(undergraduation_Apply);
        }

        // POST: Undergraduation_Apply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Father_Name,Mother_Name,Gender,Nationality,Address,PostCode,Contact,Email,Unit")] Undergraduation_Apply undergraduation_Apply)
        {
            if (id != undergraduation_Apply.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(undergraduation_Apply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Undergraduation_ApplyExists(undergraduation_Apply.ID))
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
            return View(undergraduation_Apply);
        }

        // GET: Undergraduation_Apply/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var undergraduation_Apply = await _context.Undergraduation_Apply
                .FirstOrDefaultAsync(m => m.ID == id);
            if (undergraduation_Apply == null)
            {
                return NotFound();
            }

            return View(undergraduation_Apply);
        }

        // POST: Undergraduation_Apply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var undergraduation_Apply = await _context.Undergraduation_Apply.FindAsync(id);
            _context.Undergraduation_Apply.Remove(undergraduation_Apply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Undergraduation_ApplyExists(int id)
        {
            return _context.Undergraduation_Apply.Any(e => e.ID == id);
        }
    }
}
