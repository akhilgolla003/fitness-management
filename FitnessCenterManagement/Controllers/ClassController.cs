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
    public class ClassController : Controller
    {
        private readonly FitnessCenterDbContext _context;

        public ClassController(FitnessCenterDbContext context)
        {
            _context = context;
        }

        // GET: Class
        public async Task<IActionResult> Index()
        {
              return _context.Classes != null ? 
                          View(await _context.Classes.ToListAsync()) :
                          Problem("Entity set 'FitnessCenterDbContext.Classes'  is null.");
        }

        // GET: Class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,ClassName,Schedule,TrainerID,MaxCapacity,RoomNumber")] Class @class)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use raw SQL for the insert
                    string insertSql = $"INSERT INTO Classes (ClassName, Schedule, TrainerID, MaxCapacity, RoomNumber) VALUES ('{@class.ClassName}', '{@class.Schedule}', {@class.TrainerID}, {@class.MaxCapacity}, '{@class.RoomNumber}')";

                    _context.Database.ExecuteSqlRaw(insertSql);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Check if the exception is caused by the trigger
                    if (IsTriggerError(ex))
                    {
                        ModelState.AddModelError(string.Empty, "Schedule conflicts with other classes. Choose a different schedule.");
                        return View(@class);
                    }
                    else
                    {
                        // Handle other database update exceptions or rethrow if necessary
                        return View("Error");
                    }
                }
            }
            return View(@class);
        }

        private bool IsTriggerError(DbUpdateException ex)
        {
            // Check if the exception is caused by the trigger
            return ex.InnerException is SqlException sqlException &&
                   sqlException.Number == 50000 &&
                   sqlException.Message.Contains("Schedule conflicts with other classes. Choose a different schedule.");
        }


        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,ClassName,Schedule,TrainerID,MaxCapacity,RoomNumber")] Class @class)
        {
            if (id != @class.ClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Use raw SQL for the update
                    string updateSql = $"UPDATE Classes SET ClassName = '{@class.ClassName}', Schedule = '{@class.Schedule}', TrainerID = {@class.TrainerID}, MaxCapacity = {@class.MaxCapacity}, RoomNumber = '{@class.RoomNumber}' WHERE ClassID = {@class.ClassID}";

                    _context.Database.ExecuteSqlRaw(updateSql);

                    // Alternatively, you can use parameters to avoid SQL injection
                    /*
                    string updateSql = "UPDATE Classes SET ClassName = @ClassName, Schedule = @Schedule, TrainerID = @TrainerID, MaxCapacity = @MaxCapacity, RoomNumber = @RoomNumber WHERE ClassID = @ClassID";

                    _context.Database.ExecuteSqlRaw(updateSql,
                        new SqlParameter("@ClassName", @class.ClassName),
                        new SqlParameter("@Schedule", @class.Schedule),
                        new SqlParameter("@TrainerID", @class.TrainerID),
                        new SqlParameter("@MaxCapacity", @class.MaxCapacity),
                        new SqlParameter("@RoomNumber", @class.RoomNumber),
                        new SqlParameter("@ClassID", @class.ClassID)
                    );
                    */

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.ClassID))
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
            return View(@class);
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classes == null)
            {
                return Problem("Entity set 'FitnessCenterDbContext.Classes'  is null.");
            }
            var @class = await _context.Classes.FindAsync(id);
            if (@class != null)
            {
                _context.Classes.Remove(@class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
          return (_context.Classes?.Any(e => e.ClassID == id)).GetValueOrDefault();
        }
    }
}
