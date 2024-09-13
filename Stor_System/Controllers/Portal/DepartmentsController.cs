using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;

namespace Stor_System.Controllers.Portal
{
    public class DepartmentsController : Controller
    {
        private readonly TawerStorContext _context;

        public DepartmentsController(TawerStorContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Departments_Role_DTL = (HttpContext.Session.GetString("Departments_Role_DTL")).Split(",");
                if (Departments_Role_DTL[0] == "L")
                {
                    return _context.Departments != null ?
                          View(await _context.Departments.ToListAsync()) :
                          Problem("Entity set 'TawerStorContext.Departments'  is null.");
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

            
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Departments_Role_DTL = (HttpContext.Session.GetString("Departments_Role_DTL")).Split(",");
                if (Departments_Role_DTL[1] == " C")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

           
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Departments_Role_DTL = (HttpContext.Session.GetString("Departments_Role_DTL")).Split(",");
                if (Departments_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Departments == null)
                    {
                        return NotFound();
                    }

                    var department = await _context.Departments.FindAsync(id);
                    if (department == null)
                    {
                        return NotFound();
                    }
                    return View(department);
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

            
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Departments_Role_DTL = (HttpContext.Session.GetString("Departments_Role_DTL")).Split(",");
                if (Departments_Role_DTL[3] == " D")
                {
                    if (id == null || _context.Departments == null)
                    {
                        return NotFound();
                    }

                    var department = await _context.Departments
                        .FirstOrDefaultAsync(m => m.DepartmentId == id);
                    if (department == null)
                    {
                        return NotFound();
                    }

                    return View(department);
                }
                else
                {
                    return RedirectToAction("Index", "LogIn");
                }
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

            
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'TawerStorContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Departments?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }
    }
}
