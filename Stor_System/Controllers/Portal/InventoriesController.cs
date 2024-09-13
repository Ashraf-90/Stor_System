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
    public class InventoriesController : Controller
    {
        private readonly TawerStorContext _context;

        public InventoriesController(TawerStorContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Inventory_Role_DTL = (HttpContext.Session.GetString("Inventory_Role_DTL")).Split(",");
                if (Inventory_Role_DTL[0] == "L")
                {
                    return _context.Inventories != null ?
                                             View(await _context.Inventories.ToListAsync()) :
                                             Problem("Entity set 'TawerStorContext.Inventories'  is null.");
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

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Inventory_Role_DTL = (HttpContext.Session.GetString("Inventory_Role_DTL")).Split(",");
                if (Inventory_Role_DTL[1] == " C")
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

        // POST: Inventories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryId,SLocationId,InventoryName,InventoryLocation,GMapLink,InventoryPhone")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        public JsonResult CreateFromCreateModel( string InventoryName , string InventoryLocation, string GMapLink, string InventoryPhone)
        {
            Inventory INV = new Inventory()
            {
                InventoryName = InventoryName,
                InventoryLocation = InventoryLocation,
                GMapLink = GMapLink,
                InventoryPhone =  InventoryPhone,
            };
            _context.Add(INV);
            _context.SaveChanges();
            return Json(new { success = true, message = "Inventory created successfully!", inventoryId = INV.InventoryId, inventoryName = INV.InventoryName });
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Inventory_Role_DTL = (HttpContext.Session.GetString("Inventory_Role_DTL")).Split(",");
                if (Inventory_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Inventories == null)
                    {
                        return NotFound();
                    }

                    var inventory = await _context.Inventories.FindAsync(id);
                    if (inventory == null)
                    {
                        return NotFound();
                    }
                    return View(inventory);
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

        // POST: Inventories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,SLocationId,InventoryName,InventoryLocation,GMapLink,InventoryPhone")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Inventory_Role_DTL = (HttpContext.Session.GetString("Inventory_Role_DTL")).Split(",");
                if (Inventory_Role_DTL[3] == " D")
                {
                    if (id == null || _context.Inventories == null)
                    {
                        return NotFound();
                    }

                    var inventory = await _context.Inventories
                        .FirstOrDefaultAsync(m => m.InventoryId == id);
                    if (inventory == null)
                    {
                        return NotFound();
                    }

                    return View(inventory);
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

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'TawerStorContext.Inventories'  is null.");
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
          return (_context.Inventories?.Any(e => e.InventoryId == id)).GetValueOrDefault();
        }
    }
}
