using System.Collections.Generic;

namespace BibliotecaMVC.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }

    public class RoleCheckbox
    {
        public string RoleName { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }

    public class EditUserRolesViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<RoleCheckbox> Roles { get; set; } = new List<RoleCheckbox>();
    }
}