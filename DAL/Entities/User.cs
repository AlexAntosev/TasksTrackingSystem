using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Get and set user name like key for application user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get and set foreign key for user profile by id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Get and set user profile by foreign key
        /// </summary>
        public virtual UserProfile Profile { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public virtual ICollection<Project> Projects { get; set; }
    }
}
