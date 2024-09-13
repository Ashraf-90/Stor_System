using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stor_System.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Stor_System.Controllers
{
    public class TestController : Controller
    {
        private readonly TawerStorContext _context;

        public TestController(TawerStorContext context)
        {
            _context = context;
        }





        public async Task<IActionResult> GetUnavailableProducts(DateTime startDate, DateTime endDate)
        {
            var excludedIds = _context.ProjectEquipments
                                .Include(e => e.ProDetail)
                                .Where(e => e.ProDetail.ProductId == 41)
                                .Select(e => e.ProDetailId)
                                .Distinct()
                                .ToList();

            return Content(string.Join(",", excludedIds));
        }




        private bool IsDateRangeWithin(DateTime? fromDatabase, DateTime? toDatabase, DateTime fromCheck, DateTime toCheck)
        {
            // Check if the entire check range is within the database range
            return fromDatabase <= fromCheck && toCheck <= toDatabase;
        }



    }
}
