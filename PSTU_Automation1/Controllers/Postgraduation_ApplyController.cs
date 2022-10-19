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
    public class Postgraduation_ApplyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Postgraduation_ApplyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Postgraduation_Apply
        public async Task<IActionResult> Index()
        {
            return View(await _context.Postgraduation_Apply.ToListAsync());
        }

        // GET: Postgraduation_Apply/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduation_Apply = await _context.Postgraduation_Apply
                .FirstOrDefaultAsync(m => m.ID == id);
            if (postgraduation_Apply == null)
            {
                return NotFound();
            }

            return View(postgraduation_Apply);
        }

        // GET: Postgraduation_Apply/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postgraduation_Apply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Father_Name,Mother_Name,Gender,Nationality,Address,PostCode,Contact,Email,Department")] Postgraduation_Apply postgraduation_Apply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postgraduation_Apply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postgraduation_Apply);
        }

        // GET: Postgraduation_Apply/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduation_Apply = await _context.Postgraduation_Apply.FindAsync(id);
            if (postgraduation_Apply == null)
            {
                return NotFound();
            }
            return View(postgraduation_Apply);
        }

        // POST: Postgraduation_Apply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Father_Name,Mother_Name,Gender,Nationality,Address,PostCode,Contact,Email,Department")] Postgraduation_Apply postgraduation_Apply)
        {
            if (id != postgraduation_Apply.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postgraduation_Apply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Postgraduation_ApplyExists(postgraduation_Apply.ID))
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
            return View(postgraduation_Apply);
        }

        // GET: Postgraduation_Apply/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduation_Apply = await _context.Postgraduation_Apply
                .FirstOrDefaultAsync(m => m.ID == id);
            if (postgraduation_Apply == null)
            {
                return NotFound();
            }

            return View(postgraduation_Apply);
        }

        // POST: Postgraduation_Apply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postgraduation_Apply = await _context.Postgraduation_Apply.FindAsync(id);
            _context.Postgraduation_Apply.Remove(postgraduation_Apply);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Postgraduation_ApplyExists(int id)
        {
            return _context.Postgraduation_Apply.Any(e => e.ID == id);
        }
    }
}
