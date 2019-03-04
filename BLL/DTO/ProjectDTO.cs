using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public IEnumerable<TaskDTO> Tasks { get; set; }
        public IEnumerable<UserDTO> Team { get; set; }
    }
}
