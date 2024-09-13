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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;


namespace Stor_System.Controllers.App
{
    public class ProductsModelsController : Controller
    {
        private readonly TawerStorContext _context;
        private readonly IHostingEnvironment hosting;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly int Int_Role_ID;

        public ProductsModelsController(TawerStorContext context, IHostingEnvironment hosting , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.hosting = hosting;

            //_httpContextAccessor = httpContextAccessor;
            //var httpContext = _httpContextAccessor.HttpContext;
            //Int_Role_ID = Convert.ToInt32(httpContext.Session.GetString("Role"));


        }
        //************************************************************* List *************************************************************
        // GET: ProductsModels
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[0]=="L")
                {
                    ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                    ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName");

                    List<ProductsModel> ProMod = new List<ProductsModel>();
                    foreach (ProductsModel productsModel in _context.ProductsModels
                            .Include(b => b.Category)
                            .Include(b => b.Brand)
                            .Include(b => b.ProductsDetails).ThenInclude(b => b.Satuts)
                            .Where(m => m.Active == "1")
                            .OrderByDescending(m => m.ProductId)
                            .ToList())
                    {
                        // Get the Quantity for the current ProductsModel
                        int Quantity = productsModel.ProductsDetails.Where(m => m.Active == "1").Count();
                        int QStatus = productsModel.ProductsDetails.Where(m => m.Active == "1").Where(p => p.Satuts.Usability != "0").Where(p => p.MovementId == 0).Count();


                        ProMod.Add(new ProductsModel
                        {
                            ProductId = productsModel.ProductId,

                            ProductModel = productsModel.ProductModel,
                            Category = productsModel.Category,
                            Brand = productsModel.Brand,
                            CategoryId = productsModel.CategoryId,
                            BrandId = productsModel.BrandId,
                            ProductName = productsModel.ProductName,
                            ImagePath = productsModel.ImagePath,
                            ProductsDetails = _context.ProductsDetails.Where(o => o.ProductId == productsModel.ProductId).Where(o => o.Active == "1").ToList(),
                            Quantity = Quantity, // Adding the building count to the model
                            QStatus = QStatus,
                        });

                    }
                    return View(ProMod);
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
        // GET: ProductsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductsModels == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModels
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        //************************************************************* Create *************************************************************
        // GET: ProductsModels/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[1] == " C")
                {
                    ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
                    ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName");
                    ViewData["SatutsId"] = new SelectList(_context.Satuts, "SatutsId", "SatutsName");
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

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreaeProduct_Model poduct,ProductsModel PproMod , 
                                    List<string> Barcodes, List<string> Statuses, List<string> Descriptions, List<string> Inventorys,
                                    string Bra_Id, string Cat_Id)
        {
            try
            {
                //********************************** Add the Model  **********************************
               
                    int int_Bra_Id = Convert.ToInt32(Bra_Id);
                    int int_Cat_Id = Convert.ToInt32(Cat_Id);

                    string FileProductIMG = string.Empty;

                    int ModelId = 0;
                    int ModelCount = _context.ProductsModels.Count();
                    if (ModelCount != 0) { ModelId = (int)_context.ProductsModels.Max(e => (int?)e.ProductId) + 1; }
                    else { ModelId = 1; }

                    if (poduct.ImagePath != null)
                    {
                        string ProductIMGUpload = Path.Combine(hosting.WebRootPath, "A_Stor_System/assets/images/Products");
                        FileProductIMG = poduct.ImagePath.FileName;
                        string ProductIMGFullPath = Path.Combine(ProductIMGUpload, FileProductIMG);
                        poduct.ImagePath.CopyTo(new FileStream(ProductIMGFullPath, FileMode.Create));
                    }


                    ProductsModel PRO = new ProductsModel()
                    {
                        ProductModel = poduct.ProductModel,
                        CategoryId = int_Cat_Id,
                        BrandId = int_Bra_Id,
                        ProductName = poduct.ProductName,
                        ImagePath = FileProductIMG,
                        Active = "1",
                    };
                    _context.Add(PRO);
                    _context.SaveChanges();

                    //********************************** Add the Model Details **********************************

                    int maxId = (int)_context.ProductsModels.Max(e => (int?)e.ProductId);

                    if (Barcodes != null && Statuses != null && Inventorys != null)
                    {
                        for (int i = 0; i < Barcodes.Count; i++)
                        {
                            var proDetail = new ProductsDetail()
                            {
                                ProDetailBarcode = Barcodes[i],
                                SatutsId = Convert.ToInt32(Statuses[i]),
                                InventoryId = Convert.ToInt32(Inventorys[i]),
                                ProDetailDescription = Descriptions[i],
                                ProductId = maxId,
                                MovementId = 0,
                                Active = "1"
                            };
                            _context.Add(proDetail);
                        }
                        _context.SaveChanges();
                    }

                    return RedirectToAction(nameof(Index));
                

            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }



        //************************************************************* Edit *************************************************************
        // GET: ProductsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Product_Role_DTL = (HttpContext.Session.GetString("Product_Role_DTL")).Split(",");
                if (Product_Role_DTL[2] == " E")
                {


                    if (id == null || _context.ProductsModels == null)
                    {
                        return NotFound();
                    }

                    var productsModel = await _context.ProductsModels.FindAsync(id);
                    if (productsModel == null)
                    {
                        return NotFound();
                    }
                    ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", productsModel.BrandId);
                    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", productsModel.CategoryId);

                    ViewData["SatutsId"] = new SelectList(_context.Satuts, "SatutsId", "SatutsName");
                    ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "InventoryName");
                    return View(productsModel);
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

        // POST: ProductsModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreaeProduct_Model poduct, int id ,
                                     List<string> Barcodes, List<string> Statuses, List<string> Descriptions, List<string> Inventorys,
                                    string Bra_Id, string Cat_Id,  string Delete_Status)
        {
            try
            {
                //********************************** Edit the Model  **********************************
                int int_Bra_Id = Convert.ToInt32(Bra_Id);
                int int_Cat_Id = Convert.ToInt32(Cat_Id);

                ProductsModel PROMOD = _context.ProductsModels.Find(poduct.ProductId);


                string FileProductIMG = string.Empty;

                if (poduct.ImagePath != null)
                {
                    string ProductIMGUpload = Path.Combine(hosting.WebRootPath, "A_Stor_System/assets/images/Products");
                    FileProductIMG = poduct.ImagePath.FileName;
                    string ProductIMGFullPath = Path.Combine(ProductIMGUpload, FileProductIMG);
                    poduct.ImagePath.CopyTo(new FileStream(ProductIMGFullPath, FileMode.Create));
                }


                PROMOD.ProductModel = poduct.ProductModel;
                PROMOD.CategoryId = int_Cat_Id;
                PROMOD.BrandId = int_Bra_Id;
                PROMOD.ProductName = poduct.ProductName;

                if (poduct.ImagePath != null) { PROMOD.ImagePath = FileProductIMG; }
                else if(poduct.ImagePath == null && Delete_Status=="0") { PROMOD.ImagePath = ""; }

                _context.Update(PROMOD);
                _context.SaveChanges();


                //********************************** Add the Model Details **********************************

                if (Barcodes != null && Statuses != null && Inventorys != null)
                {
                    for (int i = 0; i < Barcodes.Count; i++)
                    {
                        var proDetail = new ProductsDetail()
                        {
                            ProDetailBarcode = Barcodes[i],
                            SatutsId = Convert.ToInt32(Statuses[i]),
                            InventoryId = Convert.ToInt32(Inventorys[i]),
                            ProDetailDescription = Descriptions[i],
                            ProductId = id,
                            MovementId = 0,
                            Active = "1"
                        };
                        _context.Add(proDetail);
                    }
                    _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }


        //************************************************************* Delete *************************************************************
        // GET: ProductsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductsModels == null)
            {
                return NotFound();
            }

            var productsModel = await _context.ProductsModels
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productsModel == null)
            {
                return NotFound();
            }

            return View(productsModel);
        }

        // POST: ProductsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductsModels == null)
            {
                return Problem("Entity set 'TawerStorContext.ProductsModels'  is null.");
            }
            var productsModel = await _context.ProductsModels.FindAsync(id);
            if (productsModel != null)
            {
                _context.ProductsModels.Remove(productsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsModelExists(int id)
        {
          return (_context.ProductsModels?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
