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
    public class NationalitiesController : Controller
    {
        private readonly TawerStorContext _context;

        public NationalitiesController(TawerStorContext context)
        {
            _context = context;
        }

        // GET: Nationalities
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Nationalities_Role_DTL = (HttpContext.Session.GetString("Nationalities_Role_DTL")).Split(",");
                if (Nationalities_Role_DTL[0] == "L")
                {
                    return _context.Nationalities != null ?
                          View(await _context.Nationalities.ToListAsync()) :
                          Problem("Entity set 'TawerStorContext.Nationalities'  is null.");
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

        // GET: Nationalities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nationalities == null)
            {
                return NotFound();
            }

            var nationality = await _context.Nationalities
                .FirstOrDefaultAsync(m => m.NationalityId == id);
            if (nationality == null)
            {
                return NotFound();
            }

            return View(nationality);
        }

        // GET: Nationalities/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Nationalities_Role_DTL = (HttpContext.Session.GetString("Nationalities_Role_DTL")).Split(",");
                if (Nationalities_Role_DTL[1] == " C")
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

        // POST: Nationalities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NationalityId,NationalityName")] Nationality nationality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nationality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nationality);
        }

        // GET: Nationalities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Nationalities_Role_DTL = (HttpContext.Session.GetString("Nationalities_Role_DTL")).Split(",");
                if (Nationalities_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Nationalities == null)
                    {
                        return NotFound();
                    }

                    var nationality = await _context.Nationalities.FindAsync(id);
                    if (nationality == null)
                    {
                        return NotFound();
                    }
                    return View(nationality);
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

        // POST: Nationalities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NationalityId,NationalityName")] Nationality nationality)
        {
            if (id != nationality.NationalityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nationality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalityExists(nationality.NationalityId))
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
            return View(nationality);
        }

        // GET: Nationalities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Nationalities_Role_DTL = (HttpContext.Session.GetString("Nationalities_Role_DTL")).Split(",");
                if (Nationalities_Role_DTL[3] == " D")
                {
                    if (id == null || _context.Nationalities == null)
                    {
                        return NotFound();
                    }

                    var nationality = await _context.Nationalities
                        .FirstOrDefaultAsync(m => m.NationalityId == id);
                    if (nationality == null)
                    {
                        return NotFound();
                    }

                    return View(nationality);
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

        // POST: Nationalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nationalities == null)
            {
                return Problem("Entity set 'TawerStorContext.Nationalities'  is null.");
            }
            var nationality = await _context.Nationalities.FindAsync(id);
            if (nationality != null)
            {
                _context.Nationalities.Remove(nationality);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationalityExists(int id)
        {
          return (_context.Nationalities?.Any(e => e.NationalityId == id)).GetValueOrDefault();
        }
    }
}
