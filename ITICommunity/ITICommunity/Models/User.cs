using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            FollowFollowing = new HashSet<Follow>();
            FollowUser = new HashSet<Follow>();
            Like = new HashSet<Like>();
            Post = new HashSet<Post>();
            UserContacts = new HashSet<UserContacts>();
            UserEducation = new HashSet<UserEducation>();
            UserWorkExperience = new HashSet<UserWorkExperience>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string NationalId { get; set; }
        public string UserName { get; set; }
        public int? IntakeId { get; set; }
        public int? TrackId { get; set; }
        public string About { get; set; }
        public byte[] Cvfile { get; set; }
        public string ProfilePic { get; set; }
        public string JobTitle { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? UserTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int? BranchId { get; set; }
        public string BgPic { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Intake Intake { get; set; }
        public virtual Track Track { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Follow> FollowFollowing { get; set; }
        public virtual ICollection<Follow> FollowUser { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<UserContacts> UserContacts { get; set; }
        public virtual ICollection<UserEducation> UserEducation { get; set; }
        public virtual ICollection<UserWorkExperience> UserWorkExperience { get; set; }
    }
}
