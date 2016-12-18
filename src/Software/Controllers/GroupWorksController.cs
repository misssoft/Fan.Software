using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Software.Data;
using Software.DomainModels;

namespace Software.Controllers
{
    [Authorize]
    public class GroupWorksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupWorksController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GroupWorks
        public async Task<IActionResult> Index()
        {
            var works = await _context.Works.ToListAsync();
           
            var worksVm = TransformtoVM(works);

            var vm = new GroupWorkViewModel()
            {
                Works = worksVm,
                Heading = "All Works",
                ManagingAccount = false
            };
            return View(vm);
        }

        

        // GET: GroupWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupWork = await _context.Works.SingleOrDefaultAsync(m => m.Id == id);
            if (groupWork == null)
            {
                return NotFound();
            }

            return View(groupWork);
        }

        // GET: GroupWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,Tasks")] GroupWork groupWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(groupWork);
        }

        // GET: GroupWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupWork = await _context.Works.SingleOrDefaultAsync(m => m.Id == id);
            if (groupWork == null)
            {
                return NotFound();
            }
            return View(groupWork);
        }

        // POST: GroupWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,Tasks")] GroupWork groupWork)
        {
            if (id != groupWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupWorkExists(groupWork.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(groupWork);
        }

        // GET: GroupWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupWork = await _context.Works.SingleOrDefaultAsync(m => m.Id == id);
            if (groupWork == null)
            {
                return NotFound();
            }

            return View(groupWork);
        }

        // POST: GroupWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupWork = await _context.Works.SingleOrDefaultAsync(m => m.Id == id);
            _context.Works.Remove(groupWork);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult Members()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var works = _context.Assignments.Where(a=>a.MemberId == userId).Select(w=>w.Work).ToList();
            var worksvm = TransformtoVM(works);
           
            var vm = new GroupWorkViewModel()
            {
                Works = worksvm,
                Heading = "My Group Work",
                ManagingAccount = true
            };

            return View("_GroupWorks",vm);
        }

        private List<WorkViewModel> TransformtoVM(List<GroupWork> works)
        {
            var worksVm = new List<WorkViewModel>();

            foreach (var work in works)
            {
                var workvm = new WorkViewModel()
                {
                    GroupWork = work,
                    CanJoin = CanJoin(work.Id)
                };
                worksVm.Add(workvm);
            }
            return worksVm;
        }

        private bool GroupWorkExists(int id)
        {
            return _context.Works.Any(e => e.Id == id);
        }

        private bool CanJoin(int workId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return !_context.Assignments.Any(x => x.MemberId == userId && x.WorkId == workId);
        }
    }
}
