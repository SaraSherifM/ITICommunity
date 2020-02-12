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
    public class OnlineUsersController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public OnlineUsersController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/OnlineUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnlineUser>>> GetOnlineUser()
        {
            return await _context.OnlineUser.ToListAsync();
        }

        // GET: api/OnlineUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OnlineUser>> GetOnlineUser(int id)
        {
            var onlineUser = await _context.OnlineUser.FindAsync(id);

            if (onlineUser == null)
            {
                return NotFound();
            }

            return onlineUser;
        }

        // PUT: api/OnlineUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOnlineUser(int id, OnlineUser onlineUser)
        {
            if (id != onlineUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(onlineUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnlineUserExists(id))
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

        // POST: api/OnlineUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OnlineUser>> PostOnlineUser(OnlineUser onlineUser)
        {
            _context.OnlineUser.Add(onlineUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOnlineUser", new { id = onlineUser.Id }, onlineUser);
        }

        // DELETE: api/OnlineUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OnlineUser>> DeleteOnlineUser(int id)
        {
            var onlineUser = await _context.OnlineUser.FindAsync(id);
            if (onlineUser == null)
            {
                return NotFound();
            }

            _context.OnlineUser.Remove(onlineUser);
            await _context.SaveChangesAsync();

            return onlineUser;
        }

        private bool OnlineUserExists(int id)
        {
            return _context.OnlineUser.Any(e => e.Id == id);
        }
        /// <summary>
        ///get online followers only
        /// </summary>
        // GET: api/OnlineUsers
        
        [HttpGet("GetOnlineFollowing/{Id}")]
        public async Task<ActionResult<IEnumerable<OnlineUser>>> GetOnlineFollowing(int id)
        {
            List<User> users = _context.User.ToList();
            List<Follow> follows = _context.Follow.ToList();
            List<OnlineUser> onlineUsers = _context.OnlineUser.ToList();
            var  onlineFollowings =
                from o in onlineUsers.ToList()
                join u in users.ToList() on o.UserId equals u.Id
                join fol in follows.ToList() on u.Id equals fol.UserId where fol.FollowingId == o.UserId where o.UserId == id
                select new {UserId=u.Id , onlineFollowing=o.UserId, FollowingID=fol.FollowingId };
            return onlineFollowings as List<OnlineUser>;
        }
    }
}
