using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var viewModel = new List<QuizViewModel>();
            foreach (var quiz in quizzes)
            {
                var topic = await _context.Topics.SingleOrDefaultAsync(m => m.Id == quiz.TopicId);
                var vm = new QuizViewModel()
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
            var vm = new QuizViewModel();
            vm.Topics = _context.Topics.ToList();
            
            return View(vm);
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Create(QuizViewModel model)
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

        [Authorize]
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

            var vm = new QuizViewModel()
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,Notes,TopicId")] QuizViewModel quizModel)
        {
            if (id != quizModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var quiz = await _context.Quizzes.SingleOrDefaultAsync(m => m.Id == id);
                    quiz.Question = quizModel.Question;
                    quiz.Answer = quizModel.Answer;
                    quiz.Notes = quizModel.Notes;
                    quiz.TopicId = quizModel.TopicId;
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quizModel.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(quizModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
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

            return View(quiz);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quizzes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }

    }


}