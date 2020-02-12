using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITICommunity.Models;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEducationsController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public UserEducationsController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/UserEducations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEducation>>> GetUserEducation()
        {
            return await _context.UserEducation.ToListAsync();
        }

        // GET: api/UserEducations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEducation>> GetUserEducation(int id)
        {
            var userEducation = await _context.UserEducation.FindAsync(id);

            if (userEducation == null)
            {
                return NotFound();
            }

            return userEducation;
        }

        // PUT: api/UserEducations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEducation(int id, UserEducation userEducation)
        {
            if (id != userEducation.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEducation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEducationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserEducations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserEducation>> PostUserEducation(UserEducation userEducation)
        {
            _context.UserEducation.Add(userEducation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEducation", new { id = userEducation.Id }, userEducation);
        }

        // DELETE: api/UserEducations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserEducation>> DeleteUserEducation(int id)
        {
            var userEducation = await _context.UserEducation.FindAsync(id);
            if (userEducation == null)
            {
                return NotFound();
            }

            _context.UserEducation.Remove(userEducation);
            await _context.SaveChangesAsync();

            return userEducation;
        }

        private bool UserEducationExists(int id)
        {
            return _context.UserEducation.Any(e => e.Id == id);
        }
    }
}
