using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uPubDash.Persistence
{
    [TableName("uPubDash")]
    [PrimaryKey("Id")]
    [ExplicitColumns]
    public class PublicationRequest
    {
        [Column("Id")]
        [PrimaryKeyColumn(Name = "PK_PublicationRequest", AutoIncrement = true, Clustered = true)]
        public int Id { get; set; }

        [Column("NodeId")]
        public int NodeId { get; set; }

        [Column("VersionId")]
        public string VersionId { get; set; }

        [Column("SubmitterUserId")]
        public int SubmitterUserId { get; set; }

        [Column("DateTimeSubmitted")]
        public DateTime DateTimeQueued { get; set; }
    }
}