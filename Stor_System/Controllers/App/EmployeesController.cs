using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;
using System;
using System.Collections.Generic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Cryptography;

namespace Stor_System.Controllers.App
{
    public class EmployeesController : Controller
    {
        private readonly TawerStorContext _context;
        private readonly IHostingEnvironment hosting;

        public EmployeesController(TawerStorContext context, IHostingEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        //************************************************************* Index *************************************************************
        // GET: Employees
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Employees_Role_DTL = (HttpContext.Session.GetString("Employees_Role_DTL")).Split(",");
                if (Employees_Role_DTL[0] == "L")
                {
                    List<Employee> EMPL = new List<Employee>();
                    foreach (Employee employee in _context.Employees.Include(b => b.Nationality).Include(b => b.Department).Where(m => m.Active == "1").ToList())
                    {
                        EMPL.Add(new Employee
                        {
                            EmployeeId = employee.EmployeeId,
                            Nationality = employee.Nationality,
                            Department = employee.Department,
                            EmployeeName = employee.EmployeeName,
                            BirthDay = ((DateTime.Now.Year) - Convert.ToInt32(Convert.ToDateTime(employee.BirthDay).Year)).ToString(),
                            Qid = employee.Qid,
                            Address = employee.Address,
                            Phone = employee.Phone,
                            Email = employee.Email,
                            ImagePath = employee.ImagePath,
                        });
                    }
                    //var tawerStorContext = _context.Employees.Where(e => e.Active=="1").Include(e => e.Department).Include(e => e.Nationality);
                    return View(EMPL);
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

        //************************************************************* Details *************************************************************
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Nationality)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        //************************************************************* Create  *************************************************************
        // GET: Employees/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Employees_Role_DTL = (HttpContext.Session.GetString("Employees_Role_DTL")).Split(",");
                if (Employees_Role_DTL[1] == " C")
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
                    ViewData["NationalityId"] = new SelectList(_context.Nationalities, "NationalityId", "NationalityName");
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

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEmployee employee,  string Nat_Id, string Dep_Id)
        {
            try
            {

                int int_Nat_Id = Convert.ToInt32(Nat_Id);
                int int_Dep_Id = Convert.ToInt32(Dep_Id);

                string FileProductIMG = string.Empty;

                if (employee.ImagePath != null)
                {
                    string ProductIMGUpload = Path.Combine(hosting.WebRootPath, "A_Stor_System/assets/images/Employee");
                    FileProductIMG = employee.ImagePath.FileName;
                    string ProductIMGFullPath = Path.Combine(ProductIMGUpload, FileProductIMG);
                    employee.ImagePath.CopyTo(new FileStream(ProductIMGFullPath, FileMode.Create));
                }


                Employee EMP = new Employee()
                {
                    NationalityId = int_Nat_Id ,
                    DepartmentId = int_Dep_Id,
                    EmployeeName = employee.EmployeeName,
                    BirthDay = employee.BirthDay,
                    Qid = employee.Qid,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    ImagePath = FileProductIMG,
                    Active = "1",
                };
                _context.Add(EMP);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }

        //************************************************************* Edit *************************************************************
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Employees_Role_DTL = (HttpContext.Session.GetString("Employees_Role_DTL")).Split(",");
                if (Employees_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Employees == null)
                    {
                        return NotFound();
                    }

                    var employee = await _context.Employees.FindAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
                    ViewData["NationalityId"] = new SelectList(_context.Nationalities, "NationalityId", "NationalityName", employee.NationalityId);
                    return View(employee);
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

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, CreateEmployee employee, string Nat_Id, string Dep_Id, string Delete_Status)
        {
            try
            {
                //********************************** Edit the Model  **********************************
                int int_Nat_Id = Convert.ToInt32(Nat_Id);
                int int_Dep_Id = Convert.ToInt32(Dep_Id);

                Employee EMPLOYEE = _context.Employees.Find(employee.EmployeeId);


                string FileProductIMG = string.Empty;

                if (employee.ImagePath != null)
                {
                    string ProductIMGUpload = Path.Combine(hosting.WebRootPath, "A_Stor_System/assets/images/Employee");
                    FileProductIMG = employee.ImagePath.FileName;
                    string ProductIMGFullPath = Path.Combine(ProductIMGUpload, FileProductIMG);
                    employee.ImagePath.CopyTo(new FileStream(ProductIMGFullPath, FileMode.Create));
                }


                EMPLOYEE.NationalityId = int_Nat_Id;
                EMPLOYEE.DepartmentId = int_Dep_Id;
                EMPLOYEE.EmployeeName = employee.EmployeeName;
                EMPLOYEE.BirthDay = employee.BirthDay;
                EMPLOYEE.Qid = employee.Qid;
                EMPLOYEE.Address = employee.Address;
                EMPLOYEE.Phone = employee.Phone;
                EMPLOYEE.Email = employee.Email;

                if (employee.ImagePath != null) { EMPLOYEE.ImagePath = FileProductIMG; }
                else if (employee.ImagePath == null && Delete_Status == "0") { EMPLOYEE.ImagePath = ""; }

                _context.Update(EMPLOYEE);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }

        //************************************************************* Delete *************************************************************
        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Employees_Role_DTL = (HttpContext.Session.GetString("Employees_Role_DTL")).Split(",");
                if (Employees_Role_DTL[3] == " D")
                {


                    if (id == null || _context.Employees == null)
                    {
                        return NotFound();
                    }

                    var employee = await _context.Employees
                        .Include(e => e.Department)
                        .Include(e => e.Nationality)
                        .FirstOrDefaultAsync(m => m.EmployeeId == id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    return View(employee);
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

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'TawerStorContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
