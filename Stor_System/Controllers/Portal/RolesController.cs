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
    public class RolesController : Controller
    {
        private readonly TawerStorContext _context;

        public RolesController(TawerStorContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Roles_Role_DTL = (HttpContext.Session.GetString("Roles_Role_DTL")).Split(",");
                if (Roles_Role_DTL[0] == "L")
                {
                    return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'TawerStorContext.Roles'  is null.");
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

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Roles_Role_DTL = (HttpContext.Session.GetString("Roles_Role_DTL")).Split(",");
                if (Roles_Role_DTL[1] == " C")
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

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Products,Requests,Employees,Categories,Barnds,Inventory,Statuses,Departments,Nationalities,Users_Accounts,Roles")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Roles_Role_DTL = (HttpContext.Session.GetString("Roles_Role_DTL")).Split(",");
                if (Roles_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Roles == null)
                    {
                        return NotFound();
                    }

                    var role = await _context.Roles.FindAsync(id);
                    if (role == null)
                    {
                        return NotFound();
                    }
                    return View(role);
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

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName, Products,Requests,Employees,Categories,Barnds,Inventory,Statuses,Departments,Nationalities,Users_Accounts,Roles")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Roles_Role_DTL = (HttpContext.Session.GetString("Roles_Role_DTL")).Split(",");
                if (Roles_Role_DTL[3] == " D")
                {
                    if (id == null || _context.Roles == null)
                    {
                        return NotFound();
                    }

                    var role = await _context.Roles
                        .FirstOrDefaultAsync(m => m.RoleId == id);
                    if (role == null)
                    {
                        return NotFound();
                    }

                    return View(role);
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

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'TawerStorContext.Roles'  is null.");
            }
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
          return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
