using System;
using System.Web.UI.WebControls;
using uPubDash.Models;
using uPubDash.Persistence;

namespace uPubDash
{
    public class PublicationRequestService : IPublicationRequestService
    {
        private IPublicationRequestRepository PublicationRequestRepository { get; set; }

        public PublicationRequestService(IPublicationRequestRepository publicationRequestRepository)
        {
            PublicationRequestRepository = publicationRequestRepository;
        }

        public int Add(AddPublicationRequestDto addPublicationRequestDto)
        {
            if (addPublicationRequestDto == null)
                throw new ArgumentNullException("addPublicationRequestDto");

            var newPublicationRequest = new PublicationRequest();

            newPublicationRequest.DateTimeQueued = DateTime.Now;
            newPublicationRequest.NodeId = addPublicationRequestDto.NodeId;
            newPublicationRequest.SubmitterUserId = addPublicationRequestDto.UserId;
            newPublicationRequest.VersionId = addPublicationRequestDto.VersionId.ToString();

            var newPublicationRequestId = PublicationRequestRepository.Create(newPublicationRequest);

            return newPublicationRequestId;
        }
    }
}