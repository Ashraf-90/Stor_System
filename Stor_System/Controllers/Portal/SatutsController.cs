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
    public class SatutsController : Controller
    {
        private readonly TawerStorContext _context;

        public SatutsController(TawerStorContext context)
        {
            _context = context;
        }

        // GET: Satuts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Statuses_Role_DTL = (HttpContext.Session.GetString("Statuses_Role_DTL")).Split(",");
                if (Statuses_Role_DTL[0] == "L")
                {
                    return _context.Satuts != null ?
                         View(await _context.Satuts.ToListAsync()) :
                         Problem("Entity set 'TawerStorContext.Satuts'  is null.");
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

        // GET: Satuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Satuts == null)
            {
                return NotFound();
            }

            var satut = await _context.Satuts
                .FirstOrDefaultAsync(m => m.SatutsId == id);
            if (satut == null)
            {
                return NotFound();
            }

            return View(satut);
        }

        // GET: Satuts/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Statuses_Role_DTL = (HttpContext.Session.GetString("Statuses_Role_DTL")).Split(",");
                if (Statuses_Role_DTL[1] == " C")
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

        // POST: Satuts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SatutsId,SatutsName,Color,Usability")] Satut satut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(satut);
        }

        // GET: Satuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Statuses_Role_DTL = (HttpContext.Session.GetString("Statuses_Role_DTL")).Split(",");
                if (Statuses_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Satuts == null)
                    {
                        return NotFound();
                    }

                    var satut = await _context.Satuts.FindAsync(id);
                    if (satut == null)
                    {
                        return NotFound();
                    }
                    return View(satut);
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

        // POST: Satuts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SatutsId,SatutsName,Color,Usability")] Satut satut)
        {
            if (id != satut.SatutsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatutExists(satut.SatutsId))
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
            return View(satut);
        }

        // GET: Satuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Statuses_Role_DTL = (HttpContext.Session.GetString("Statuses_Role_DTL")).Split(",");
                if (Statuses_Role_DTL[3] == " D")
                {
                    if (id == null || _context.Satuts == null)
                    {
                        return NotFound();
                    }

                    var satut = await _context.Satuts
                        .FirstOrDefaultAsync(m => m.SatutsId == id);
                    if (satut == null)
                    {
                        return NotFound();
                    }

                    return View(satut);
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

        // POST: Satuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Satuts == null)
            {
                return Problem("Entity set 'TawerStorContext.Satuts'  is null.");
            }
            var satut = await _context.Satuts.FindAsync(id);
            if (satut != null)
            {
                _context.Satuts.Remove(satut);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatutExists(int id)
        {
          return (_context.Satuts?.Any(e => e.SatutsId == id)).GetValueOrDefault();
        }
    }
}
