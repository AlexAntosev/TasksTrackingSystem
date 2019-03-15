using System;

namespace Web_API.Models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Priority Priority { get; set; }
        public string CreatorName { get; set; }
        public string DeveloperName { get; set; }
        public DateTime Deadline { get; set; }
    }

    public enum Priority
    {
        Low,
        Middle,
        High,
        UltraHigh
    }
}