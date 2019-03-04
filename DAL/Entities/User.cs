using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        /// <summary>
        /// Get and set user id
        /// </summary>
        public int Id { get; set; }

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
