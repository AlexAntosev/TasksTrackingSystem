using System;

namespace DAL.Entities
{
    public class Invite : BaseEntity
    {
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? ReceiverId { get; set; }
        public virtual User Receiver { get; set; }

        public DateTime Time { get; set; }
        public int Role { get; set; }
    }
}
