namespace BLL.DTO
{
    public class UserWithRoleDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserDTO User { get; set; }

        public ProjectRoles Role { get; set; }
    }

    public enum ProjectRoles
    {
        NoRole,
        Admin,
        Developer,
        Watcher
    }
}
