using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITICommunity.DTOs
{
    public class UserForRegisterDto
    {
        [EmailAddress]
        [Required]
        public string emailaddress { get; set; }
        [Required]
        public string password { get; set; }
    }
}
