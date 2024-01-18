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
                    ProfessionId = x.ProfessionId,
                    SMLink=x.ExpertsSMLinks.Select(q=>q.SMLinks.SMLink).ToList(),
                }).ToListAsync();
         
            return View(data);
        }
        public async Task<IActionResult> CreateExpert()
        {
            ViewBag.Profession = _db.Professions;
            ViewBag.SM = _db.SMLinks;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateExpert(ExpertCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Experts expert = new Experts
            {
                ImagePath = vm.ImagePath,
                ProfessionId = vm.ProfessionId,
                ExpertsSMLinks=vm.SMLinkIds.Select(x=>new ExpertsSMLinks
                {
                    SMLinksId=x
                }).ToList()
                
            };
            await _db.Experts.AddAsync(expert);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> UpdateExpert(int id)
        {
            ViewBag.Profession = _db.Professions;
            ViewBag.SM = _db.SMLinks;
            var data = await _db.Experts.Include(x => x.Profession).Include(d => d.ExpertsSMLinks).SingleOrDefaultAsync(d => d.Id == id);
            if (data == null) return NotFound();

            var vm = new ExpertUpdateVM
            {
                ImagePath = data.ImagePath,
                ProfessionName = data.Profession.Name,
                ProfessionId = data.ProfessionId,
                SMLinkIds = data.ExpertsSMLinks.Select(x => x.SMLinksId).ToList()
            };
                return View(vm);
        }
           
        
        [HttpPost]
        public async Task<IActionResult> UpdateExpert(int id, ExpertCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Experts.Include(x => x.Profession).Include(d => d.ExpertsSMLinks).SingleOrDefaultAsync(d => d.Id == id);
            if (data == null) return NotFound();
            data.ImagePath = vm.ImagePath;
            data.ProfessionId = vm.ProfessionId;
            if(vm.SMLinkIds!=null)
            {
                data.ExpertsSMLinks = vm.SMLinkIds.Select(t => new ExpertsSMLinks
                {
                    SMLinksId = t
                }).ToList();
            }
       

           
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
