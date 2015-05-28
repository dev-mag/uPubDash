using System;
using System.Collections.Generic;
using uPubDash.Models;
using uPubDash.Persistence;
using Umbraco.Core.Services;

namespace uPubDash.Services
{
    public class PublicationRequestService : IPublicationRequestService
    {
        private IUserService UserService { get; set; }
        private IContentService ContentService { get; set; }
        private IPublicationRequestRepository PublicationRequestRepository { get; set; }

        public PublicationRequestService(IPublicationRequestRepository publicationRequestRepository, IUserService userService, IContentService contentService)
        {
            UserService = userService;
            ContentService = contentService;
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

        public List<PublicationRequestDto> GetRequests()
        {
            var list = PublicationRequestRepository.Read();

            var publicationRequestDtos = new List<PublicationRequestDto>();

            foreach (var publicationRequest in list)
            {
                var publicationRequestDto = new PublicationRequestDto();

                publicationRequestDto.Id = publicationRequest.Id;
                publicationRequestDto.SubmittedBy = UserService.GetUserById(publicationRequest.SubmitterUserId).Name;
                publicationRequestDto.Name = ContentService.GetById(publicationRequest.NodeId).Name;
                publicationRequestDto.VersionId = new Guid(publicationRequest.VersionId);
                publicationRequestDto.DateSubmitted = publicationRequest.DateTimeQueued;

                publicationRequestDtos.Add(publicationRequestDto);
            }

            return publicationRequestDtos;
        }
    }
}