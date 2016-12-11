using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Software.Data;
using Software.DomainModels;

namespace Software.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public QuizzesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quizzes = _context.Quizzes.ToList();
            var viewModel = new List<QuizzesViewModel>();
            foreach (var quiz in quizzes)
            {
               var vm = new QuizzesViewModel()
               {
                   Answer = quiz.Answer,
                   Id = quiz.Id,
                   Notes = quiz.Notes,
                   Question = quiz.Question,
                   Topic = quiz.Topic,
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
                try
                {
                    _context.Add(quiz);
                    var result =_context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }

            return View(model);
        }
    }
}