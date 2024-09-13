using Stor_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using Microsoft.CodeAnalysis;

namespace Stor_System.Controllers.App
{
    public class LogIn : Controller
    {
        private readonly TawerStorContext _context;

        public LogIn(TawerStorContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Role", "0");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            var obj = _context.UserAccounts.Where(a => a.UserName.Equals(UserName) && a.Passwod.Equals(Password)).FirstOrDefault();
            if (obj != null)
            {
                HttpContext.Session.SetString("Role", obj.RoleId.ToString());
                int Role = Convert.ToInt32(HttpContext.Session.GetString("Role"));

                HttpContext.Session.SetString("UserID", obj.UserId.ToString());
                
                var RoleDTL = _context.Roles.SingleOrDefault(R => R.RoleId == Role);

                //********************************* App Sessions *********************************
                HttpContext.Session.SetString("Product_Role_DTL", RoleDTL.Products.ToString());
                HttpContext.Session.SetString("Request_Role_DTL", RoleDTL.Requests.ToString());
                HttpContext.Session.SetString("Employees_Role_DTL", RoleDTL.Employees.ToString());
                //********************************* Portal Sessions *********************************
                HttpContext.Session.SetString("Categories_Role_DTL", RoleDTL.Categories.ToString());
                HttpContext.Session.SetString("Barnds_Role_DTL", RoleDTL.Barnds.ToString());
                HttpContext.Session.SetString("Inventory_Role_DTL", RoleDTL.Inventory.ToString());
                HttpContext.Session.SetString("Statuses_Role_DTL", RoleDTL.Statuses.ToString());
                HttpContext.Session.SetString("Departments_Role_DTL", RoleDTL.Departments.ToString());
                HttpContext.Session.SetString("Nationalities_Role_DTL", RoleDTL.Nationalities.ToString());
                HttpContext.Session.SetString("Roles_Role_DTL", RoleDTL.Roles.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid UserName Or Password  !!!";
                return View("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            HttpContext.Session.SetString("Role", null);
            return View("index");
        }
    }
}
