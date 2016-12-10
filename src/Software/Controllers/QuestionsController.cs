using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Software.Data;
using Software.DomainModels;
using Software.Models;

namespace Software.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var questions = _context.Questions.ToList();
            return View(questions);
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var question = new Question()
            {
                DateTime = DateTime.Now.ToLocalTime(),
                Questioner = user

            };
            return View(question);
        }
    }
}