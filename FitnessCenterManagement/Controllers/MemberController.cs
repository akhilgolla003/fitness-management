using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessCenterManagement.Models;

namespace FitnessCenterManagement.Controllers
{
    public class MemberController : Controller
    {
        private readonly FitnessCenterDbContext _context;

        public MemberController(FitnessCenterDbContext context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            return _context.Members != null ?
                        View(await _context.Members.ToListAsync()) :
                        Problem("Entity set 'FitnessCenterDbContext.Members'  is null.");
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,FirstName,LastName,MembershipType,ExpirationDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,FirstName,LastName,MembershipType,ExpirationDate")] Member member)
        {
            if (id != member.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberID))
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
            return View(member);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Members == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Members == null)
            {
                return Problem("Entity set 'FitnessCenterDbContext.Members'  is null.");
            }
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return (_context.Members?.Any(e => e.MemberID == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> MemberAttendance()
        {
            var joinResults = from member in _context.Members
                              join attendance in _context.Attendance on member.MemberID equals attendance.MemberID
                              select new MemberAttendanceViewModel
                              {
                                  MemberName = $"{member.FirstName} {member.LastName}",
                                  AttendanceDate = attendance.Date,
                                  Status = attendance.Status,
                                  MembershipType = member.MembershipType
                              };

            var viewModelList = await joinResults.ToListAsync();

            return View("MemberAttendance", viewModelList);
        }

        public async Task<IActionResult> AttendanceClass()
        {
            var joinResults = from attendance in _context.Attendance
                              join fitnessClass in _context.Classes on attendance.ClassID equals fitnessClass.ClassID
                              join member in _context.Members on attendance.MemberID equals member.MemberID
                              select new AttendanceClassViewModel
                              {
                                  AttendanceID = attendance.AttendanceID,
                                  MemberName = $"{member.FirstName} {member.LastName}", 
                                  ClassName = fitnessClass.ClassName,
                                  AttendanceDate = attendance.Date,
                                  Status = attendance.Status
                              };

            var viewModelList = await joinResults.ToListAsync();

            return View("AttendanceClass", viewModelList);
        }


    }
}
