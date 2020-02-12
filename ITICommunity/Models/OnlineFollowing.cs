using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITICommunity.Models
{
    public class OnlineFollowing
    {
        private readonly ITICommunityContext _context;

        public OnlineFollowing(ITICommunityContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<OnlineFollowing>>> GetFollowing(int id)
        {
            List<User> users = _context.User.ToList();
            List<Follow> follows = _context.Follow.ToList();
            List<OnlineUser> onlineUsers = _context.OnlineUser.ToList();
            var onlineFollowings =
                from o in onlineUsers.ToList()
                join u in users.ToList() on o.UserId equals u.Id
                join fol in follows.ToList() on u.Id equals fol.UserId
                where fol.FollowingId == o.UserId
                where o.UserId == id
                select new { UserId = u.Id, onlineFollowing = o.UserId, FollowingID = fol.FollowingId };
            return onlineFollowings as List<OnlineFollowing>;
        }
    }
}
