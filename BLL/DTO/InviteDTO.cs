using System;

namespace BLL.DTO
{
    public sealed class InviteDTO
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public string AuthorUserName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public int  ReceiverId { get; set; }
        public string ReceiverUserName { get; set; }

        public string Time { get; set; }
        public ProjectRoles Role { get; set; }
    }
}
