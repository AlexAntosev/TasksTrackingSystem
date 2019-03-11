using System.Collections.Generic;

namespace DAL.Entities
{
    public class Project : BaseEntity
    {
        /// <summary>
        /// Get and set project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set project tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Get and set task collection in project
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; }

        /// <summary>
        /// Get and set collection of users in project
        /// </summary>
        public virtual ICollection<User> Team { get; set; }
    }
}
