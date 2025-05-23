using Microsoft.AspNetCore.Identity;

namespace WashWise.Web.Models
{
    public class UserRoleViewModel
    {
        public IdentityUser User { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}