using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Stor_System.Controllers.App
{
    public class HomeController : Controller
    {
        private readonly TawerStorContext _context;

        public HomeController(TawerStorContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? month, int? year)
        {

            int NewCreatedProjects = _context.Projects.Where(PJ => PJ.Active == "1").Where(PJ => PJ.ApproveStatus == "0")
                                    .OrderByDescending(m => m.ProjectId).Count();
            ViewBag.NewCreatedProjects = NewCreatedProjects;


            var projects = _context.Projects.Where(p=>p.Active=="1").Select(p => new
            {
                p.ProjectId,
                p.ProjectName,
                p.ProjectStatus,
                Start_Date = p.StartDate,  // Format date for JavaScript
                End_Date = p.EndDate       // Format date for JavaScript
            }).ToList();

            // Pass the data to the view using ViewBag
            ViewBag.Projects = JsonConvert.SerializeObject(projects);

            var ProjectXX = _context.Projects.Where(PJ=>PJ.Active=="1").Where(PJ => PJ.ApproveStatus == "1").OrderByDescending(m => m.ProjectId).ToList();

            return View(ProjectXX);
        }

        public IActionResult Privacy()
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}