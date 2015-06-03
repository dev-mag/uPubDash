using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;
using uPubDash.Models;
using uPubDash.Persistence;

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

        public int Submit(SubmitPublicationRequestDto submitPublicationRequestDto)
        {
            if(submitPublicationRequestDto == null)
                throw new ArgumentNullException("submitPublicationRequestDto");

            var publicationRequest = FindByNodeId(submitPublicationRequestDto.NodeId);

            if(publicationRequest == null)
            {
                publicationRequest = new PublicationRequest
                {
                    NodeId = submitPublicationRequestDto.NodeId, 
                    DateTimeQueued = DateTime.Now, 
                    SubmitterUserId = submitPublicationRequestDto.UserId, 
                    VersionId = submitPublicationRequestDto.VersionId.ToString()
                };

                return PublicationRequestRepository.Create(publicationRequest);
            }

            publicationRequest.DateTimeQueued = DateTime.Now;
            publicationRequest.VersionId = submitPublicationRequestDto.VersionId.ToString();
            publicationRequest.SubmitterUserId = submitPublicationRequestDto.UserId;

            return PublicationRequestRepository.Update(publicationRequest);
        }

        public void RemoveForDocument(int nodeId)
        {
            var publicationRequest = FindByNodeId(nodeId);

            if (publicationRequest != null)
            {
                PublicationRequestRepository.Delete(publicationRequest.Id);
            }
        }

        public List<PublicationRequestDto> GetRequests()
        {
            var list = PublicationRequestRepository.Read();

            var publicationRequestDtos = new List<PublicationRequestDto>();

            foreach(var publicationRequest in list)
            {
                var publicationRequestDto = new PublicationRequestDto
                {
                    Id = publicationRequest.Id, 
                    SubmittedBy = UserService.GetUserById(publicationRequest.SubmitterUserId).Name, 
                    NodeId = publicationRequest.NodeId, 
                    Name = ContentService.GetById(publicationRequest.NodeId).Name, 
                    VersionId = new Guid(publicationRequest.VersionId), 
                    DateSubmitted = publicationRequest.DateTimeQueued
                };

                publicationRequestDtos.Add(publicationRequestDto);
            }

            return publicationRequestDtos;
        }

        public PublicationRequestDto GetRequest(int publicationRequestId)
        {
            var request = PublicationRequestRepository.Read(publicationRequestId);

            var publicationRequestDto = new PublicationRequestDto()
            {
                Id = request.Id,
                SubmittedBy = UserService.GetUserById(request.SubmitterUserId).Name,
                Name = ContentService.GetById(request.NodeId).Name,
                VersionId = new Guid(request.VersionId),
                DateSubmitted = request.DateTimeQueued
            };

            return publicationRequestDto;
        }

        private PublicationRequest FindByNodeId(int nodeId)
        {
            var list = PublicationRequestRepository.Read();
            return list.FirstOrDefault(request => request.NodeId == nodeId);
        }
    }
}