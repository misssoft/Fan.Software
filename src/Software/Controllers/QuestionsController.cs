using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Software.Data;
using Software.DomainModels;

namespace Software.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var questions = _context.Questions.ToList();
            return View(questions);
        }

        public IActionResult Create()
        {
            var question = new Question()
            {
                DateTime = DateTime.Now
            };
            return View(question);
        }
    }
}