using System.Collections.Generic;
using DAL.Entities;

namespace BLL.DTO
{
    public sealed class UserDTO
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
        /// Get and set user profile by foreign key
        /// </summary>
        public UserProfileDTO Profile { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public IEnumerable<ProjectDTO> Projects { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
