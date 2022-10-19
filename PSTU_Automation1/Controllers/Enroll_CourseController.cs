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
    public class Enroll_CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Enroll_CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enroll_Course
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enroll_Course.ToListAsync());
        }

        // GET: Enroll_Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll_Course = await _context.Enroll_Course
                .FirstOrDefaultAsync(m => m.EnrolId == id);
            if (enroll_Course == null)
            {
                return NotFound();
            }

            return View(enroll_Course);
        }

        // GET: Enroll_Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enroll_Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrolId,StudentId,InstructorName,CourseTitle,CourseCode,CourseCradit,Department")] Enroll_Course enroll_Course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enroll_Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enroll_Course);
        }

        // GET: Enroll_Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll_Course = await _context.Enroll_Course.FindAsync(id);
            if (enroll_Course == null)
            {
                return NotFound();
            }
            return View(enroll_Course);
        }

        // POST: Enroll_Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrolId,StudentId,InstructorName,CourseTitle,CourseCode,CourseCradit,Department")] Enroll_Course enroll_Course)
        {
            if (id != enroll_Course.EnrolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enroll_Course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Enroll_CourseExists(enroll_Course.EnrolId))
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
            return View(enroll_Course);
        }

        // GET: Enroll_Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enroll_Course = await _context.Enroll_Course
                .FirstOrDefaultAsync(m => m.EnrolId == id);
            if (enroll_Course == null)
            {
                return NotFound();
            }

            return View(enroll_Course);
        }

        // POST: Enroll_Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enroll_Course = await _context.Enroll_Course.FindAsync(id);
            _context.Enroll_Course.Remove(enroll_Course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Enroll_CourseExists(int id)
        {
            return _context.Enroll_Course.Any(e => e.EnrolId == id);
        }
    }
}
