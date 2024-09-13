using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;

namespace Stor_System.Controllers.App
{
    public class UserAccountsController : Controller
    {
        private readonly TawerStorContext _context;

        public UserAccountsController(TawerStorContext context)
        {
            _context = context;
        }

        //************************************************************* List *************************************************************
        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            var tawerStorContext = _context.UserAccounts.Include(u => u.Employee).Include(u => u.Role);
            return View(await tawerStorContext.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        //************************************************************* Create *************************************************************
        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: UserAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount userAccount, string Emp_Id, string Role_Id)  {
            try
            {

                int int_Emp_Id = Convert.ToInt32(Emp_Id);
                int int_Role_Id = Convert.ToInt32(Role_Id);

                UserAccount USC = new UserAccount()
                {
                    EmployeeId = int_Emp_Id,
                    RoleId = int_Role_Id,
                    UserName = userAccount.UserName,
                    Passwod =  userAccount.Passwod,
                    Active = "1",
                };
                _context.Add(USC);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }


        //************************************************************* Edit *************************************************************
        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", userAccount.EmployeeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", userAccount.RoleId);
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserAccount userAccount, string Emp_Id, string Role_Id)  {
            try
            {
                int int_Emp_Id = Convert.ToInt32(Emp_Id);
                int int_Role_Id = Convert.ToInt32(Role_Id);

                UserAccount USERACC = _context.UserAccounts.Find(userAccount.UserId);

                USERACC.EmployeeId = int_Emp_Id;
                USERACC.RoleId = int_Role_Id;
                USERACC.UserName = userAccount.UserName;
                USERACC.Passwod = userAccount.Passwod;

                _context.Update(USERACC);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }


        //************************************************************* Delete *************************************************************
        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAccounts == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAccounts == null)
            {
                return Problem("Entity set 'TawerStorContext.UserAccounts'  is null.");
            }
            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount != null)
            {
                _context.UserAccounts.Remove(userAccount);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
          return (_context.UserAccounts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
