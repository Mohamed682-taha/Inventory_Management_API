namespace Shared
{
    public class UserWithRoleDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
