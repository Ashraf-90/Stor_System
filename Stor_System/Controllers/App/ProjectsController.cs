using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;

namespace Stor_System.Controllers.App
{
    public class ProjectsController : Controller
    {
        private readonly TawerStorContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly int Int_Role_ID;

        public ProjectsController(TawerStorContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _httpContextAccessor = httpContextAccessor;
            var httpContext = _httpContextAccessor.HttpContext;

            Int_Role_ID = Convert.ToInt32(httpContext.Session.GetString("Role"));

        }
        //************************************************************* List *************************************************************
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[0] == "L")
                {
                    if (Request_Role_DTL[5] != " DI")
                    {
                        var AllUnSeenProject = await _context.Projects
                            .Include(PJ => PJ.EmployeeRequester)
                            .Include(PJ => PJ.EmployeeDeliver)
                            .Where(PJ => PJ.NotifivationSeen != "1")
                            .Where(PJ => PJ.ApproveStatus != "0")
                            .ToListAsync();
                        foreach (var project in AllUnSeenProject)
                        {
                            project.NotifivationSeen = "1";
                        }
                        await _context.SaveChangesAsync();
                    }



                    List<Project> PRJC = new List<Project>();

                    foreach (Project project in _context.Projects
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Product).ThenInclude(pe => pe.Brand)
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Product).ThenInclude(pe => pe.Category)
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Satuts)
                            .Where(m => m.Active == "1")

                            .OrderByDescending(PJ => PJ.ProjectId)
                            .ToList())
                    {
                        // Dictionary to store product names, model names, and their corresponding counts
                        var productModelCounts = project.ProjectEquipments
                            .GroupBy(pe => new { pe.ProDetail.Product.ProductName, pe.ProDetail.Product.ProductModel })
                            .ToDictionary(g => (g.Key.ProductName, g.Key.ProductModel), g => g.Count());

                        // Add project details to the PRJC list
                        PRJC.Add(new Project
                        {
                            ProjectId = project.ProjectId,
                            EmployeeRequesterId = project.EmployeeRequesterId,
                            EmployeeDeliverId = project.EmployeeDeliverId,
                            ProjectName = project.ProjectName,
                            ProjectLocation = project.ProjectLocation,
                            StartDate = project.StartDate,
                            EndDate = project.EndDate,

                            ProjectDescription = project.ProjectDescription,
                            ApproveStatus = project.ApproveStatus,
                            ProjectStatus = project.ProjectStatus,
                            NotifivationSeen = project.NotifivationSeen,
                            InternalExternal = project.InternalExternal,

                            CreateDate = project.CreateDate,
                            EmployeeDeliver = project.EmployeeDeliver,
                            EmployeeRequester = project.EmployeeRequester,

                            Quantity = project.ProjectEquipments.Count(),

                            // Assign the product and model counts to the new property
                            ProductModelCounts = productModelCounts
                        });
                    }

                    return View(PRJC);
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

        //************************************************************* List *************************************************************
        // GET: Projects
        public async Task<IActionResult> Archives()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[4] == " A")
                {
                    List<Project> PRJC = new List<Project>();
                    foreach (Project project in _context.Projects
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Product).ThenInclude(pe => pe.Brand)
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Product).ThenInclude(pe => pe.Category)
                            .Include(b => b.ProjectEquipments).ThenInclude(pe => pe.ProDetail).ThenInclude(pe => pe.Satuts)
                            .Where(m => m.Active == "0")
                            .ToList())
                    {
                        // Dictionary to store product names, model names, and their corresponding counts
                        var productModelCounts = project.ProjectEquipments
                            .GroupBy(pe => new { pe.ProDetail.Product.ProductName, pe.ProDetail.Product.ProductModel })
                            .ToDictionary(g => (g.Key.ProductName, g.Key.ProductModel), g => g.Count());

                        // Add project details to the PRJC list
                        PRJC.Add(new Project
                        {
                            ProjectId = project.ProjectId,
                            EmployeeRequesterId = project.EmployeeRequesterId,
                            EmployeeDeliverId = project.EmployeeDeliverId,
                            ProjectName = project.ProjectName,
                            ProjectLocation = project.ProjectLocation,
                            StartDate = project.StartDate,
                            EndDate = project.EndDate,

                            ProjectDescription = project.ProjectDescription,
                            ApproveStatus = project.ApproveStatus,
                            ProjectStatus = project.ProjectStatus,
                            NotifivationSeen = project.NotifivationSeen,

                            CreateDate = project.CreateDate,
                            EmployeeDeliver = project.EmployeeDeliver,
                            EmployeeRequester = project.EmployeeRequester,

                            Quantity = project.ProjectEquipments.Count(),

                            // Assign the product and model counts to the new property
                            ProductModelCounts = productModelCounts
                        });
                    }

                    return View(PRJC);
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

        //************************************************************* Details ***********************************************************
        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a single project based on the provided id and load all related data
            var project = await _context.Projects
                .Include(b => b.EmployeeDeliver)
                .Include(b => b.EmployeeRequester)
                .Include(b => b.ProjectEquipments)
                    .ThenInclude(pe => pe.ProDetail)
                    .ThenInclude(pe => pe.Product)
                    .ThenInclude(pe => pe.Brand)
                .Include(b => b.ProjectEquipments)
                    .ThenInclude(pe => pe.ProDetail)
                    .ThenInclude(pe => pe.Product)
                    .ThenInclude(pe => pe.Category)
                .Include(b => b.ProjectEquipments)
                    .ThenInclude(pe => pe.ProDetail)
                    .ThenInclude(pe => pe.Satuts)
                .Where(m => m.Active == "1" && m.ProjectId == id)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            // Dictionary to store product names, model names, and their corresponding counts
            var productModelCounts = project.ProjectEquipments
                .GroupBy(pe => new { pe.ProDetail.Product.ProductName, pe.ProDetail.Product.ProductModel })
                .ToDictionary(g => (g.Key.ProductName, g.Key.ProductModel), g => g.Count());

            // Prepare the project details for the view
            var projectDetails = new Project
            {
                ProjectId = project.ProjectId,
                EmployeeRequesterId = project.EmployeeRequesterId,
                EmployeeDeliverId = project.EmployeeDeliverId,
                ProjectName = project.ProjectName,
                ProjectLocation = project.ProjectLocation,
                StartDate = project.StartDate,
                EndDate = project.EndDate,

                ProjectDescription = project.ProjectDescription,
                ApproveStatus = project.ApproveStatus,
                ProjectStatus = project.ProjectStatus,
                NotifivationSeen = project.NotifivationSeen,

                CreateDate = project.CreateDate,
                EmployeeDeliver = project.EmployeeDeliver,
                EmployeeRequester = project.EmployeeRequester,

                Quantity = project.ProjectEquipments.Count(),

                // Assign the product and model counts to the new property
                ProductModelCounts = productModelCounts
            };

            return View(projectDetails);
        }

        //************************************************************* Create ***********************************************************
        // GET: Projects/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[1] == " C")
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

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ProjectName, string ProjectLocation, string StartDate, string EndDate, string ProjectDescription, string InternalExternalText)
        {
            try
            {
                DateTime Date_StartDate = Convert.ToDateTime(StartDate);
                DateTime Date_EndDate = Convert.ToDateTime(EndDate);

                int EployeeID = _context.UserAccounts.Where(e => e.RoleId == Int_Role_ID).Select(e => e.EmployeeId).FirstOrDefault();

                Project PRO = new Project()
                {
                    EmployeeRequesterId = EployeeID,
                    EmployeeDeliverId = EployeeID,
                    ProjectName = ProjectName,
                    ProjectLocation = ProjectLocation,
                    StartDate = Date_StartDate,
                    EndDate = Date_EndDate,
                    ProjectDescription = ProjectDescription,
                    ApproveStatus = "0",
                    ProjectStatus = "1",
                    InternalExternal = InternalExternalText,
                    NotifivationSeen = "0",
                    CreateDate = DateTime.Now.Date,
                    Active = "1",

                };
                _context.Add(PRO);
                _context.SaveChanges();

                int projectmaxid = (int)_context.Projects.Max(e => (int?)e.ProjectId);
                return RedirectToAction("ProjectEquipment", "Projects", new { id = projectmaxid });
            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }

        // GET: Project
        public async Task<IActionResult> ProjectEquipment(int id)
        {

            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[1] == " C")
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

                        var startEndDate = _context.Projects
                            .Where(prj => prj.ProjectId == id)
                            .Select(prj => new { prj.StartDate, prj.EndDate })
                            .FirstOrDefault();


                        DateTime? startDate = startEndDate.StartDate;
                        DateTime? endDate = startEndDate.EndDate;





                        int unavailableCount = _context.ProjectEquipments
                            .Where(pe => pe.ProDetail.ProductId == productsModel.ProductId &&
                                         pe.Project.StartDate <= endDate &&
                                         pe.Project.EndDate >= startDate)
                            .Where(pe => pe.Project.ProjectStatus != "4")
                            .Where(pe => pe.Project.InternalExternal == "2")

                            .GroupBy(pe => pe.ProDetail.ProductId)
                            .Select(g => g.Select(pe => pe.ProDetailId).Distinct().Count())
                            .FirstOrDefault();




                        int QStatus = productsModel.ProductsDetails.Where(m => m.Active == "1").Where(p => p.Satuts.Usability != "0").Count();


                        //int PJUsered = _context.ProjectEquipments
                        //    .Where(pe => pe.ProjectId == id)
                        //    .Where(pe => pe.ProDetail.ProductId == productsModel.ProductId )
                        //    .Count();


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
                            QStatus = QStatus - unavailableCount,

                            //PJUsered = PJUsered,    
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

        // POST: ProjectEquipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectEquipment(int id,string Produts_Id_List , string Produts_Qu_List)
        {
            try
            {
                if (Produts_Id_List != "")
                {
                    //int projectmaxid = (int)_context.Projects.Max(e => (int?)e.ProjectId);

                    string[] Arr_Produts_Id_List = Produts_Id_List.Split(',');
                    string[] Arr_Produts_Qu_List = Produts_Qu_List.Split(',');

                    string[] Arr_ProdutsD_Id_List;

                    if (Arr_Produts_Id_List.Length != 0)
                    {
                        for (int i = 0; i < Arr_Produts_Id_List.Length; i++)
                        {
                            var startEndDate = _context.Projects
                            .Where(prj => prj.ProjectId == id)
                            .Select(prj => new { prj.StartDate, prj.EndDate })
                            .FirstOrDefault();

                            DateTime? startDate = startEndDate.StartDate;
                            DateTime? endDate = startEndDate.EndDate;

                            var excludedIds = _context.ProjectEquipments
                                .Include(e => e.ProDetail)
                                .Where(pe => pe.ProDetail.ProductId == Convert.ToInt32(Arr_Produts_Id_List[i]) &&
                                 pe.Project.StartDate <= endDate &&
                                 pe.Project.EndDate >= startDate)
                                .Where(pe => pe.Project.ProjectStatus != "4")
                                .Where(pe => pe.Project.InternalExternal == "2")
                                .Where(e => e.ProDetail.ProductId == Convert.ToInt32(Arr_Produts_Id_List[i]))
                                .Select(e => e.ProDetailId)
                                .Distinct()
                                .ToList();

                            var model_Name = _context.ProductsDetails
                                .Where(p => p.Active == "1")
                                .Where(p => p.MovementId == 0)
                                .Where(p => p.Satuts.Usability == "1")
                                .Where(p => p.ProductId == Convert.ToInt32(Arr_Produts_Id_List[i]))
                                .Where(p => !excludedIds.Contains(p.ProDetailId)) // Exclude specific ProDetailId values retrieved from the database
                                .Take(Convert.ToInt32(Arr_Produts_Qu_List[i]))
                                .Select(p => p.ProDetailId)
                                .ToList();

                            for (int j = 0; j < model_Name.Count; j++)
                            {
                                ProjectEquipment PROEQ = new ProjectEquipment()
                                {
                                    ProjectId = id,
                                    ProDetailId = model_Name[j],
                                };
                                _context.Add(PROEQ);
                            }
                            _context.SaveChanges();

                       
                        }
                    }
                }

                return RedirectToAction("Index", "Projects");
            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }

        //************************************************************* Edit ***********************************************************
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[2] == " E")
                {
                    if (id == null || _context.Projects == null)
                    {
                        return NotFound();
                    }
                    ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
                    var project = await _context.Projects.FindAsync(id);
                    if (project == null)
                    {
                        return NotFound();
                    }
                    return View(project);
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

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,EmployeeRequesterId,EmployeeDeliverId,ProjectName,ProjectLocation,StartDate,EndDate,ProjectDescription,ApproveStatus,ProjectStatus, InternalExternal,Active")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }
           
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.ProjectId))
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



        // GET: Project
        public async Task<IActionResult> EditPJEQ(int id)
        {
            if (HttpContext.Session.GetString("Role") != "0")
            {
                string[] Request_Role_DTL = (HttpContext.Session.GetString("Request_Role_DTL")).Split(",");
                if (Request_Role_DTL[2] == " E")
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

                        var startEndDate = _context.Projects
                            .Where(prj => prj.ProjectId == id)
                            .Select(prj => new { prj.StartDate, prj.EndDate })
                            .FirstOrDefault();


                        DateTime? startDate = startEndDate.StartDate;
                        DateTime? endDate = startEndDate.EndDate;





                        int unavailableCount = _context.ProjectEquipments
                            .Where(pe => pe.ProDetail.ProductId == productsModel.ProductId &&
                                         pe.Project.StartDate <= endDate &&
                                         pe.Project.EndDate >= startDate)
                            .Where(pe => pe.Project.ProjectStatus != "4")
                            .Where(pe => pe.Project.InternalExternal == "2")

                            .GroupBy(pe => pe.ProDetail.ProductId)
                            .Select(g => g.Select(pe => pe.ProDetailId).Distinct().Count())
                            .FirstOrDefault();




                        int QStatus = productsModel.ProductsDetails.Where(m => m.Active == "1").Where(p => p.Satuts.Usability != "0").Count();


                        int PJUsered = _context.ProjectEquipments
                            .Where(pe => pe.ProjectId == id)
                            .Where(pe => pe.ProDetail.ProductId == productsModel.ProductId)
                            .Count();


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
                            QStatus = QStatus - unavailableCount,
                            PJUsered = PJUsered,
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

        // POST: ProjectEquipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPJEQ(int id, string Produts_Id_List, string Produts_Qu_List)
        {

            try
            {
                var OldEQ = _context.ProjectEquipments.Where(PE => PE.ProjectId == id);
                if (OldEQ.Any())
                {
                    _context.RemoveRange(OldEQ);
                    _context.SaveChanges();
                }


                Project PROJECT = _context.Projects.Find(id);
                PROJECT.ApproveStatus = "0";
                _context.Update(PROJECT);
                _context.SaveChanges();


                if (Produts_Id_List != "")
                {
                    //int projectmaxid = (int)_context.Projects.Max(e => (int?)e.ProjectId);

                    string[] Arr_Produts_Id_List = Produts_Id_List.Split(',');
                    string[] Arr_Produts_Qu_List = Produts_Qu_List.Split(',');

                    string[] Arr_ProdutsD_Id_List;

                    if (Arr_Produts_Id_List.Length != 0)
                    {
                        for (int i = 0; i < Arr_Produts_Id_List.Length; i++)
                        {

                            var excludedIds = _context.ProjectEquipments
                                .Include(e => e.ProDetail)
                                .Where(e => e.ProDetail.ProductId == Convert.ToInt32(Arr_Produts_Id_List[i]))
                                .Select(e => e.ProDetailId)
                                .Distinct()
                                .ToList();

                            var model_Name = _context.ProductsDetails
                                .Where(p => p.Active == "1")
                                .Where(p => p.MovementId == 0)
                                .Where(p => p.Satuts.Usability == "1")
                                .Where(p => p.ProductId == Convert.ToInt32(Arr_Produts_Id_List[i]))
                                .Where(p => !excludedIds.Contains(p.ProDetailId)) // Exclude specific ProDetailId values retrieved from the database
                                .Take(Convert.ToInt32(Arr_Produts_Qu_List[i]))
                                .Select(p => p.ProDetailId)
                                .ToList();

                            for (int j = 0; j < model_Name.Count; j++)
                            {
                                ProjectEquipment PROEQ = new ProjectEquipment()
                                {
                                    ProjectId = id,
                                    ProDetailId = model_Name[j],
                                };
                                _context.Add(PROEQ);
                            }
                            _context.SaveChanges();


                        }
                    }
                }

                return RedirectToAction("Index", "Projects");
            }
            catch (Exception ex) { return RedirectToAction("ErrorPage", "Home"); }
        }

        























        //************************************************************* Decisions ***********************************************************
        public IActionResult Accepted(int id)
        {
            Project PROJECT = _context.Projects.Find(id);
            PROJECT.ApproveStatus = "1";
            _context.Update(PROJECT);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Rejected(int id)
        {
            Project PROJECT = _context.Projects.Find(id);
            PROJECT.ApproveStatus = "2";
            _context.Update(PROJECT);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ComfirmDelete(int id)
        {
            Project PROJECT = _context.Projects.Find(id);
            PROJECT.Active = "0";
            _context.Update(PROJECT);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
