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
        /// Get and set unique username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get and set user first name 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get and set user last name 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get and set user position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Get and set collection of projects
        /// </summary>
        public IEnumerable<ProjectDTO> Projects { get; set; }

        public string AuthenticationUserId { get; set; }
    }
}
