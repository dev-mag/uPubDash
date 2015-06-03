using System;

namespace uPubDash.Models
{
    public class SubmitPublicationRequestDto
    {
        public int NodeId { get; set; }
        public int UserId { get; set; }
        public Guid VersionId { get; set; }
    }
}