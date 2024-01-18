using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Admin.ViewModels;
using WebApplication1.Areas.Admin.ViewModels.ExpertVMs;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        ExamDay4DBContext _db { get; }

        public HomeController(ExamDay4DBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            var data =await _db.Experts
                .Include(d => d.Profession)
                .Select(x => new ExpertsListItemVM
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    IsDeleted = x.IsDeleted,
                    ProfessionName = x.Profession.Name,
                    SMLink=x.ExpertsSMLinks.Select(q=>q.SMLinks.SMLink).ToList(),
                }).ToListAsync();
         
            return View(data);
        }
    }
}
