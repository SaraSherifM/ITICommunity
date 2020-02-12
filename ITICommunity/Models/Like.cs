﻿using System;
using System.Collections.Generic;

namespace ITICommunity.Models
{
    public partial class Like
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
