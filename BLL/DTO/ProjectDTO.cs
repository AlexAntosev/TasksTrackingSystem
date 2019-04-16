using System.Collections.Generic;

namespace BLL.DTO
{
    public sealed class ProjectDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Tag { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// Get and set the foreign key for the user that created the project.
        /// </summary>
        public int? LeadId { get; set; }

        /// <summary>
        /// Get and set tasks in the project.
        /// </summary>
        public IEnumerable<TaskDTO> Tasks { get; set; }

        /// <summary>
        /// Get and set users in the project.
        /// </summary>
        public IEnumerable<UserWithRoleDTO> Team { get; set; }

        /// <summary>
        /// Get and set invites in the project.
        /// </summary>
        public IEnumerable<InviteDTO> Invites { get; set; }
    }
}
