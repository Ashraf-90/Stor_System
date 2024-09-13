using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stor_System.Models;


namespace Stor_System
{
    public class ProductService
    {
        private readonly TawerStorContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int Int_Role_ID;
        private readonly int Int_User_ID;


        public ProductService(TawerStorContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            _httpContextAccessor = httpContextAccessor;
            var httpContext = _httpContextAccessor.HttpContext;

            Int_Role_ID = Convert.ToInt32(httpContext.Session.GetString("Role"));
            Int_User_ID = Convert.ToInt32(httpContext.Session.GetString("UserID"));
        }

        public  List<Project> GetAllProducts()
        {
           int NewCreatedProjects = _context.Projects.Where(PJ => PJ.Active == "1").Where(PJ => PJ.ApproveStatus == "0")
                                    .OrderByDescending(m => m.ProjectId).Count();


            return _context.Projects
                .Where(PJ => PJ.Active == "1")
                .OrderByDescending(m => m.ProjectId)
                .Take(NewCreatedProjects)
                .ToList();
        }



        public List<Project> REQProducts()
        {

            int NewCreatedProjects = _context.Projects
                                        .Where(PJ => PJ.Active == "1")
                                        .Where(PJ => PJ.ApproveStatus != "0")
                                        .Where(PJ => PJ.NotifivationSeen != "1")
                                        .OrderByDescending(m => m.ProjectId).Count();


            return _context.Projects
                .Where(PJ => PJ.Active == "1")
                .Where(PJ => PJ.ApproveStatus != "0")
                .Where(PJ => PJ.NotifivationSeen != "1")
                .OrderByDescending(m => m.ProjectId)
                .Take(NewCreatedProjects)
                .ToList();
        }



        public List<UserAccount> INFOUserAccount()
        {
            return  _context.UserAccounts.Include(U => U.Role).Include(U => U.Employee).Where(U => U.UserId == Int_User_ID).ToList();
        }
    }
}
