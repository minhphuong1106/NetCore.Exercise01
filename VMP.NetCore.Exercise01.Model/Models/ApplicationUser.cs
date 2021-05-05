using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace VMP.NetCore.Exercise01.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        //public virtual IEnumerable<Order> Orders { set; get; }
    }
}