using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Core.Data;
using WebApplication_Core.Models;

namespace WebApplication_Core.Controllers
{
    public class StaffController : Controller
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("LastName,FirstName,StaffNo,Designation,Address")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View(staff);
        }

        public IActionResult List()
        {
            var staff = _context.Staff.ToList();
            return View(staff);
        }

        public IActionResult Edit(int id)
        {
            var staff = _context.Staff.FirstOrDefault(e => e.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,LastName,FirstName,StaffNo,Designation,Address")] Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!_context.Staff.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Log the exception details
                        Console.WriteLine(ex);
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(staff);
        }


        public IActionResult Delete(int id)
        {
            var staff = _context.Staff.FirstOrDefault(e => e.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var staff = _context.Staff.FirstOrDefault(e => e.Id == id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
        }
    }
}
