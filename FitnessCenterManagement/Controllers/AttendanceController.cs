using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessCenterManagement.Models;
using System.Data.SqlClient;

namespace FitnessCenterManagement.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly FitnessCenterDbContext _context;

        public AttendanceController(FitnessCenterDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public async Task<IActionResult> Index()
        {
              return _context.Attendance != null ? 
                          View(await _context.Attendance.ToListAsync()) :
                          Problem("Entity set 'FitnessCenterDbContext.Attendance'  is null.");
        }

        // GET: Attendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .FirstOrDefaultAsync(m => m.AttendanceID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceID,MemberID,ClassID,Date,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use raw SQL for the insert
                    string insertSql = $"INSERT INTO Attendance (MemberID, ClassID, Date, Status) VALUES ({attendance.MemberID}, {attendance.ClassID}, '{attendance.Date}', '{attendance.Status}')";

                    _context.Database.ExecuteSqlRaw(insertSql);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Check if the exception is caused by the trigger
                    if (IsTriggerError(ex))
                    {
                        ModelState.AddModelError(string.Empty, "Class at full capacity. Cannot add more attendees.");
                        return View(attendance);
                    }
                    else
                    {
                        // Handle other database update exceptions or rethrow if necessary
                        throw;
                    }
                }

            }
            return View(attendance);
        }

        private bool IsTriggerError(DbUpdateException ex)
        {
            // Check if the exception message indicates a trigger-generated error
            return ex.InnerException is SqlException sqlException &&
                   sqlException.Number == 50000 &&
                   sqlException.Message.Contains("Class at full capacity. Cannot add more attendees.");
        }

        // GET: Attendance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceID,MemberID,ClassID,Date,Status")] Attendance attendance)
        {
            if (id != attendance.AttendanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceID))
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
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendance == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .FirstOrDefaultAsync(m => m.AttendanceID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendance == null)
            {
                return Problem("Entity set 'FitnessCenterDbContext.Attendance'  is null.");
            }
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendance.Remove(attendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
          return (_context.Attendance?.Any(e => e.AttendanceID == id)).GetValueOrDefault();
        }
    }
}
