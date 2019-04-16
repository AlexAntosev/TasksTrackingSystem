using System.Collections.Generic;

namespace DAL.Entities
{
    public class UserWithRole : BaseEntity
    {
        public int? UserId { get; set; }
        public User User { get; set; }

        public int? Role { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public UserWithRole()
        {
            Projects = new HashSet<Project>();
        }
    }
}
