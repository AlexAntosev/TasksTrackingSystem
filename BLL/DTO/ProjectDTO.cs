using System.Collections.Generic;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        /// <summary>
        /// Get and set project id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get and set project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get and set project tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Get and set tasks collection in project
        /// </summary>
        public IEnumerable<TaskDTO> Tasks { get; set; }

        /// <summary>
        /// Get and set users tean in project
        /// </summary>
        public IEnumerable<UserDTO> Team { get; set; }
    }
}
