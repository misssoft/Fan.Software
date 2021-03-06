using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Software.Data;
using Software.DomainModels;

namespace Software.Controllers
{
    [Authorize]
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public TopicsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Topics
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.SummarySort = string.IsNullOrEmpty(sortOrder) ? "summary_desc" : "";
            ViewBag.IntroSort = string.IsNullOrEmpty(sortOrder) ? "intro_desc" : "intro_asc";

            var topics = await GetAllTopics();
            
            switch (sortOrder)
            {
                case "summary_desc":
                    topics = topics.OrderByDescending(t => t.Summary);
                    break;
                case "intro_asc":
                    topics = topics.OrderBy(t => t.Intro);
                    break;
                case "intro_desc":
                    topics = topics.OrderByDescending(t => t.Intro);
                    break;
                default:
                    topics = topics.OrderBy(t => t.Summary);
                    break;
                
            }
            
            return View(topics);
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            var newTopic = new Topic()
            {
                TimeStamp = DateTime.Now.ToLocalTime()
            };
            return View(newTopic);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intro,Rate,Summary,TimeStamp")] Topic topic)
        {
            topic.TimeStamp = DateTime.Now.ToLocalTime();
            if (ModelState.IsValid)
            {
                _context.Add(topic);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intro,Rate,Summary,TimeStamp")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.Id))
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
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == id);
            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }

        private async Task<IEnumerable<Topic>> GetAllTopics()
        {
            var topics = _context.Topics.ToList();
            return await Task.FromResult(topics);
        }
    }
}
