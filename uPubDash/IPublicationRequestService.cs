using uPubDash.Models;

namespace uPubDash
{
    public interface IPublicationRequestService
    {
        int Add(AddPublicationRequestDto addPublicationRequestDto);
    }
}