using System.Collections.Generic;
using uPubDash.Models;

namespace uPubDash.Services
{
    public interface IPublicationRequestService
    {
        List<PublicationRequestDto> GetRequests();
        PublicationRequestDto GetRequest(int publicationRequestId);
        int Submit(SubmitPublicationRequestDto submitPublicationRequestDto);
        void RemoveForDocument(int nodeId);
    }
}