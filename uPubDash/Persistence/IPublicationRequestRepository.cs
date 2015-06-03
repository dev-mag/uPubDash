using System.Collections.Generic;

namespace uPubDash.Persistence
{
    public interface IPublicationRequestRepository
    {
        int Create(PublicationRequest publicationRequest);
        List<PublicationRequest> Read();
        PublicationRequest Read(int publicationRequestId);
        int Update(PublicationRequest publicationRequest);
        void Delete(int publicationRequestId);
    }
}