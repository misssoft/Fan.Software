using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Software.Data;
using Software.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Software.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public QuizzesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var quizzes = _context.Quizzes.ToList();
            var viewModel = new List<QuizzesViewModel>();
            foreach (var quiz in quizzes)
            {
                var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == quiz.TopicId);
                var vm = new QuizzesViewModel()
               {
                   Answer = quiz.Answer,
                   Id = quiz.Id,
                   Notes = quiz.Notes,
                   Question = quiz.Question,
                   Topic = topic,
                   TopicId = quiz.TopicId,
               };
                viewModel.Add(vm);
            }
            return View(viewModel);
        }

        public IActionResult Create()
        {
            var vm = new QuizzesViewModel();
            vm.Topics = _context.Topics.ToList();
            
            return View(vm);
        }
        
        [HttpPost]
        public IActionResult Create(QuizzesViewModel model)
        {
            var quiz = new Quiz()
            {
                Answer   = model.Answer,
                Notes = model.Notes,
                Question = model.Question,
                TopicId = model.TopicId
            };

           if (ModelState.IsValid)
            {
                _context.Add(quiz);
                    var result =_context.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            return View(model);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.SingleOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }
            if (quiz.Topic == null && quiz.TopicId > 0)
            {
                var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == quiz.TopicId);
                quiz.Topic = topic;
            }
            return View(quiz);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.SingleOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == quiz.TopicId);
            quiz.Topic = topic;

            var vm = new QuizzesViewModel()
            {
                Answer = quiz.Answer,
                Id = quiz.Id,
                Notes = quiz.Notes,
                Question = quiz.Question,
                Topic = quiz.Topic,
                TopicId = quiz.TopicId,
            };
            vm.Topics = _context.Topics.ToList();
            return View(vm);
        }

    }
}