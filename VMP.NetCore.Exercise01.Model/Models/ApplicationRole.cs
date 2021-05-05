using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VMP.NetCore.Exercise01.Model.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        [StringLength(250)]
        public string Description { set; get; }
    }
}