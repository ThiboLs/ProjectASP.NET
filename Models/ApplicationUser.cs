using Microsoft.AspNetCore.Identity;
using System;

namespace ProjectASP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
    }
}
