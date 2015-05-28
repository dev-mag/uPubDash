using System.Collections.Generic;
using uPubDash.Models;

namespace uPubDash.Services
{
    public interface IPublicationRequestService
    {
        int Add(AddPublicationRequestDto addPublicationRequestDto);
        List<PublicationRequestDto> GetRequests();
    }
}