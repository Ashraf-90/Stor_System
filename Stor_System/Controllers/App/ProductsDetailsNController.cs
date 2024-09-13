using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stor_System.Models;

namespace Stor_System.Controllers.App
{
    public class ProductsDetailsNController : Controller
    {
        private readonly TawerStorContext _context;

        public ProductsDetailsNController(TawerStorContext context)
        {
            _context = context;
        }
        //*************************************************** List For All ***************************************************
        // GET: ProductsDetailsN
        public async Task<IActionResult> IndexAll()
        {

            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[0] == "L")
                {
                    dynamic mymodel = new ExpandoObject();

                    // Fetch ProductsDetails along with the current associated Project
                    mymodel.ProductsDetails = _context.ProductsDetails
                        .Where(PD => PD.Active == "1")
                        .Include(p => p.Inventory)
                        .Include(p => p.Satuts)
                        .Include(p => p.Product)
                        .Select(pd => new
                        {
                            ProductsDetail = pd,
                            CurrentProject = pd.ProjectEquipments
                                .Where(pe => pe.Project.StartDate <= DateTime.Now && pe.Project.EndDate >= DateTime.Now)
                                .Where(pe=>pe.Project.ProjectStatus=="3")
                                .Select(pe => new
                                {
                                    pe.Project.ProjectName,
                                    pe.Project.StartDate,
                                    pe.Project.EndDate
                                })
                                .FirstOrDefault(),
                            ProjectHistories = pd.ProjectEquipments
                                        .Select(pe => new
                                        {
                                            pe.Project.ProjectName,
                                            pe.Project.StartDate,
                                            pe.Project.EndDate,
                                            pe.Project.ProjectStatus
                                        }).ToList()
                        }).OrderByDescending(PD =>PD.ProductsDetail.Product.ProductId )
                        .ToList();

                    mymodel.Status = _context.Satuts.ToList();
                    return View(mymodel);

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

        //*************************************************** List For One Model ***************************************************
        // GET: ProductsDetailsN
        public async Task<IActionResult> Index(int id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[0] == "L")
                {
                    dynamic mymodel = new ExpandoObject();
                    mymodel.ProductsDetails = _context.ProductsDetails
                        .Where(PD => PD.Active == "1")
                        .Where(PD => PD.ProductId == id)
                        .Include(p => p.Inventory)
                        .Include(p => p.Satuts)
                        .Include(p => p.Product).Select(pd => new
                        {
                            ProductsDetail = pd,
                            CurrentProject = pd.ProjectEquipments
                                .Where(pe => pe.Project.StartDate <= DateTime.Now && pe.Project.EndDate >= DateTime.Now)
                                .Where(pe => pe.Project.ProjectStatus == "3")
                                .Select(pe => new
                                {
                                    pe.Project.ProjectName,
                                    pe.Project.StartDate,
                                    pe.Project.EndDate
                                })
                                .FirstOrDefault(),
                            ProjectHistories = pd.ProjectEquipments
                                        .Select(pe => new
                                        {
                                            pe.Project.ProjectName,
                                            pe.Project.StartDate,
                                            pe.Project.EndDate,
                                            pe.Project.ProjectStatus
                                        }).ToList()
                        })
                        .ToList();


                    mymodel.Status = _context.Satuts.ToList();

                    var Model_Name = _context.ProductsModels.Where(e => e.ProductId == id).Select(e => e.ProductName).FirstOrDefault();
                    ViewBag.Model_Name = Model_Name;

                    var Model_Id = id;
                    ViewBag.Model_Id = Model_Id;

                    return View(mymodel);
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

        //*************************************************** List Archive *******************************************
        public async Task<IActionResult> Archive()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[4] == " A")
                {
                    dynamic mymodel = new ExpandoObject();
                    mymodel.ProductsDetails = _context.ProductsDetails.Where(PD => PD.Active == "0").Include(p => p.Inventory).Include(p => p.Satuts).Include(p => p.Product).ToList();

                    return View(mymodel);
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

        //*************************************************** Details ***************************************************
        // GET: ProductsDetailsN/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductsDetails == null)
            {
                return NotFound();
            }

            var productsDetail = await _context.ProductsDetails
                .Include(p => p.Inventory)
                .Include(p => p.Product)
                .Include(p => p.Satuts)
                .FirstOrDefaultAsync(m => m.ProDetailId == id);
            if (productsDetail == null)
            {
                return NotFound();
            }

            return View(productsDetail);
        }
     
        //*************************************************** Edit ***************************************************
        // GET: ProductsDetailsN/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[2] == " E")
                {
                    if (id == null || _context.ProductsDetails == null)
                    {
                        return NotFound();
                    }

                    var Model_Name = _context.ProductsDetails.Where(e => e.ProDetailId == id).Select(e => e.Product.ProductName).FirstOrDefault();
                    ViewBag.Model_Name = Model_Name;



                    var productsDetail = await _context.ProductsDetails.FindAsync(id);
                    if (productsDetail == null)
                    {
                        return NotFound();
                    }
                    ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName", productsDetail.InventoryId);
                    ViewData["ProductId"] = new SelectList(_context.ProductsModels, "ProductId", "ProductName", productsDetail.ProductId);
                    ViewData["SatutsId"] = new SelectList(_context.Satuts, "SatutsId", "SatutsName", productsDetail.SatutsId);
                    return View(productsDetail);
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

        // POST: ProductsDetailsN/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int MID ,int id, [Bind("ProDetailId,ProductId,InventoryId,SatutsId,MovementId,MovementLocation,ProDetailBarcode,ProDetailDescription,Active")] ProductsDetail productsDetail)
        {

            if (id != productsDetail.ProDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(productsDetail);

                    var Model_ID = _context.ProductsDetails.Where(e => e.ProDetailId == id).Select(e => e.ProductId).FirstOrDefault();
                     MID = Convert.ToInt32(Model_ID);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsDetailExists(productsDetail.ProDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/ProductsDetailsN/Index/" + MID);
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName", productsDetail.InventoryId);
            ViewData["ProductId"] = new SelectList(_context.ProductsModels, "ProductId", "ProductName", productsDetail.ProductId);
            ViewData["SatutsId"] = new SelectList(_context.Satuts, "SatutsId", "SatutsName", productsDetail.SatutsId);
            return Redirect("/ProductsDetailsN/Index/" + MID);
        }

        //*************************************************** Delete ***************************************************
        // GET: ProductsDetailsN/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[3] == " D")
                {

                    if (id == null || _context.ProductsDetails == null)
                    {
                        return NotFound();
                    }

                    var Model_Name = _context.ProductsDetails.Where(e => e.ProDetailId == id).Select(e => e.Product.ProductName).FirstOrDefault();
                    ViewBag.Model_Name = Model_Name;

                    var productsDetail = await _context.ProductsDetails
                        .Include(p => p.Inventory)
                        .Include(p => p.Product)
                        .Include(p => p.Satuts)
                        .FirstOrDefaultAsync(m => m.ProDetailId == id);
                    if (productsDetail == null)
                    {
                        return NotFound();
                    }

                    return View(productsDetail);
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

        // POST: ProductsDetailsN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ProductsDetail productsDetail , int id , int MID)
        {

            ProductsDetail PROD = _context.ProductsDetails.Find(productsDetail.ProDetailId);
            PROD.Active = "0";

            _context.Update(PROD);
            _context.SaveChanges();


            var Model_ID = _context.ProductsDetails.Where(e => e.ProDetailId == id).Select(e => e.ProductId).FirstOrDefault();
            MID = Convert.ToInt32(Model_ID);

            return Redirect("/ProductsDetailsN/Index/" + MID);
        }

        private bool ProductsDetailExists(int id)
        {
          return (_context.ProductsDetails?.Any(e => e.ProDetailId == id)).GetValueOrDefault();
        }
    }
}
