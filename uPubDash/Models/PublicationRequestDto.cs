using System;

namespace uPubDash.Models
{
    public class PublicationRequestDto
    {
        public int Id { get; set; }
        public int NodeId { get; set; }
        public string Name { get; set; }
        public Guid VersionId { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}