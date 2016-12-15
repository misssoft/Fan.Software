using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Software.Data;
using Software.DomainModels;
using System.Security.Claims;
using Software.Dtos;

namespace Software.WebApi
{
    [Produces("application/json")]
    [Route("api/Assignments")]
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Join(AssignmentDto dto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var exists = _context.Assignments.Any(m => m.MemberId == userId && m.WorkId == dto.WorkId);

            if (exists)
            {
                return BadRequest("You are one of the members");
            }

            var newMember = new GroupAssignment()
            {
                WorkId = dto.WorkId,
                MemberId = userId 
            };

            _context.Assignments.Add(newMember);
            _context.SaveChanges();

            return Ok();
        }
    }

    
}