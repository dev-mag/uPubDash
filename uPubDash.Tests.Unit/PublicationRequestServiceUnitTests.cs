﻿using System;
using System.Collections.Generic;
using System.Data;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using uPubDash.Models;
using uPubDash.Persistence;
using uPubDash.Services;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services;

namespace uPubDash.Tests.Unit
{
    [TestFixture]
    public class PublicationRequestServiceUnitTests
    {
        [Test]
        public void SubmitIsCalled_GivenValidModel_IdIsReturnedAsExpected()
        {
            //Given
            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1);
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(new List<PublicationRequest>());

            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new SubmitPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);
            var result = publicationRequestService.Submit(validModel);

            //Then
            result.Should().Be(1);
        }

        [Test]
        public void SubmitIsCalled_GivenValidModelAndErroringRepository_RepositoryExceptionIsRaised()
        {
            //Given
            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Throws(new DataException());
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(new List<PublicationRequest>());

            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new SubmitPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            Action action = () => publicationRequestService.Submit(validModel);

            //Then
            action.ShouldThrow<DataException>();
        }

        [Test]
        public void SubmitIsCalled_GivenNullModel_RepositoryIsCalledAsExpected()
        {
            //Given
            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1);

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            Action action = () => publicationRequestService.Submit(null);

            //Then
            action.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SubmitIsCalled_GivenValidNewRequest_RepositoryCreateIsInvoked()
        {
            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1).Verifiable();
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(new List<PublicationRequest>());
            publicationRequestRepositoryMock.Setup(x => x.Update(It.IsAny<PublicationRequest>())).Returns(1).Verifiable();
            

            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new SubmitPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);
            var result = publicationRequestService.Submit(validModel);

            publicationRequestRepositoryMock.Verify(x => x.Create(It.IsAny<PublicationRequest>()), Times.Once);
            publicationRequestRepositoryMock.Verify(x => x.Update(It.IsAny<PublicationRequest>()), Times.Never);
        }

        [Test]
        public void SubmitIsCalled_GivenExistingDocumentPublicationRequested_RepositoryUpdateIsInvoked()
        {
            const int testPubReq1Id = 1;
            const int testPubReq1NodeId = 2;
            const string testPubReq1NodeName = "testPubReq1NodeName";
            const int testPubReq1SubmitterUserId = 3;
            const string testPubReq1SubmittedBy = "testPubReq1SubmittedBy";
            var testPubReq1VersionId = Guid.NewGuid().ToString();

            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var user1Mock = new Mock<IUser>();
            user1Mock.Setup(x => x.Name).Returns(testPubReq1SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq1SubmitterUserId))).Returns(user1Mock.Object);


            var node1Mock = new Mock<IContent>();
            node1Mock.Setup(x => x.Name).Returns(testPubReq1NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq1NodeId))).Returns(node1Mock.Object);

            var testPubReq1 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq1Id,
                NodeId = testPubReq1NodeId,
                SubmitterUserId = testPubReq1SubmitterUserId,
                VersionId = testPubReq1VersionId
            };

            var publicationRequestList = new List<PublicationRequest>() { testPubReq1 };

            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new SubmitPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1).Verifiable();
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(publicationRequestList);
            publicationRequestRepositoryMock.Setup(x => x.Update(It.IsAny<PublicationRequest>())).Returns(1).Verifiable();

            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);
            var result = publicationRequestService.Submit(validModel);

            publicationRequestRepositoryMock.Verify(x => x.Create(It.IsAny<PublicationRequest>()), Times.Never);
            publicationRequestRepositoryMock.Verify(x => x.Update(It.IsAny<PublicationRequest>()), Times.Once);
        }

        [Test]
        public void GetRequestsIsCalled_GivenValidResponseFromRepository_ListReturnedAsExpected()
        {
            //Given
            const int testPubReq1Id = 1;
            const int testPubReq1NodeId = 2;
            const string testPubReq1NodeName = "testPubReq1NodeName";
            const int testPubReq1SubmitterUserId = 3;
            const string testPubReq1SubmittedBy = "testPubReq1SubmittedBy";
            var testPubReq1VersionId = Guid.NewGuid().ToString();

            const int testPubReq2Id = 4;
            const int testPubReq2NodeId = 5;
            const string testPubReq2NodeName = "testPubReq2NodeName";
            const int testPubReq2SubmitterUserId = 6;
            const string testPubReq2SubmittedBy = "testPubReq2SubmittedBy";
            var testPubReq2VersionId = Guid.NewGuid().ToString();

            var userServiceMock = new Mock<IUserService>();

            var user1Mock = new Mock<IUser>();
            user1Mock.Setup(x => x.Name).Returns(testPubReq1SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq1SubmitterUserId))).Returns(user1Mock.Object);

            var user2Mock = new Mock<IUser>();
            user2Mock.Setup(x => x.Name).Returns(testPubReq2SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq2SubmitterUserId))).Returns(user2Mock.Object);

            var contentServiceMock = new Mock<IContentService>();

            var node1Mock = new Mock<IContent>();
            node1Mock.Setup(x => x.Name).Returns(testPubReq1NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq1NodeId))).Returns(node1Mock.Object);

            var node2Mock = new Mock<IContent>();
            node2Mock.Setup(x => x.Name).Returns(testPubReq2NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq2NodeId))).Returns(node2Mock.Object);

            var testPubReq1 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq1Id,
                NodeId = testPubReq1NodeId,
                SubmitterUserId = testPubReq1SubmitterUserId,
                VersionId = testPubReq1VersionId
            };

            var testPubReq2 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq2Id,
                NodeId = testPubReq2NodeId,
                SubmitterUserId = testPubReq2SubmitterUserId,
                VersionId = testPubReq2VersionId
            };

            var publicationRequestList = new List<PublicationRequest>() { testPubReq1, testPubReq2 };

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(publicationRequestList);

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            var result = publicationRequestService.GetRequests();

            //Then
            result.Count.Should().Be(2);

            result[0].Id.Should().Be(testPubReq1Id);
            result[0].Name.Should().Be(testPubReq1NodeName);
            result[0].SubmittedBy.Should().Be(testPubReq1SubmittedBy);
            result[0].VersionId.Should().Be(testPubReq1VersionId);

            result[1].Id.Should().Be(testPubReq2Id);
            result[1].Name.Should().Be(testPubReq2NodeName);
            result[1].SubmittedBy.Should().Be(testPubReq2SubmittedBy);
            result[1].VersionId.Should().Be(testPubReq2VersionId);
        }

        [Test]
        public void GetRequestIsCalled_GivenValidResponseFromRepository_RequestReturnedAsExpected()
        {
            //Given
            const int testPubReq1Id = 1;
            const int testPubReq1NodeId = 2;
            const string testPubReq1NodeName = "testPubReq1NodeName";
            const int testPubReq1SubmitterUserId = 3;
            const string testPubReq1SubmittedBy = "testPubReq1SubmittedBy";
            var testPubReq1VersionId = Guid.NewGuid().ToString();

            const int testPubReq2Id = 4;
            const int testPubReq2NodeId = 5;
            const string testPubReq2NodeName = "testPubReq2NodeName";
            const int testPubReq2SubmitterUserId = 6;
            const string testPubReq2SubmittedBy = "testPubReq2SubmittedBy";
            var testPubReq2VersionId = Guid.NewGuid().ToString();

            var userServiceMock = new Mock<IUserService>();

            var user1Mock = new Mock<IUser>();
            user1Mock.Setup(x => x.Name).Returns(testPubReq1SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq1SubmitterUserId))).Returns(user1Mock.Object);

            var user2Mock = new Mock<IUser>();
            user2Mock.Setup(x => x.Name).Returns(testPubReq2SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq2SubmitterUserId))).Returns(user2Mock.Object);

            var contentServiceMock = new Mock<IContentService>();

            var node1Mock = new Mock<IContent>();
            node1Mock.Setup(x => x.Name).Returns(testPubReq1NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq1NodeId))).Returns(node1Mock.Object);

            var node2Mock = new Mock<IContent>();
            node2Mock.Setup(x => x.Name).Returns(testPubReq2NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq2NodeId))).Returns(node2Mock.Object);

            var testPubReq1 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq1Id,
                NodeId = testPubReq1NodeId,
                SubmitterUserId = testPubReq1SubmitterUserId,
                VersionId = testPubReq1VersionId
            };

            var testPubReq2 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq2Id,
                NodeId = testPubReq2NodeId,
                SubmitterUserId = testPubReq2SubmitterUserId,
                VersionId = testPubReq2VersionId
            };

            var publicationRequestList = new List<PublicationRequest>() { testPubReq1, testPubReq2 };

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Read(testPubReq1Id)).Returns(testPubReq1);

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            var result = publicationRequestService.GetRequest(testPubReq1Id);

            //Then
            result.Id.Should().Be(testPubReq1Id);
            result.Name.Should().Be(testPubReq1NodeName);
            result.SubmittedBy.Should().Be(testPubReq1SubmittedBy);
            result.VersionId.Should().Be(testPubReq1VersionId);
        }

        [Test]
        public void RemoveForDocumentIsCalled_GivenRequestForDocumentNodeExists_RepositoryDeleteIsInvoked()
        {
            const int testPubReq1Id = 1;
            const int testPubReq1NodeId = 2;
            const string testPubReq1NodeName = "testPubReq1NodeName";
            const int testPubReq1SubmitterUserId = 3;
            const string testPubReq1SubmittedBy = "testPubReq1SubmittedBy";
            var testPubReq1VersionId = Guid.NewGuid().ToString();

            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var user1Mock = new Mock<IUser>();
            user1Mock.Setup(x => x.Name).Returns(testPubReq1SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq1SubmitterUserId))).Returns(user1Mock.Object);


            var node1Mock = new Mock<IContent>();
            node1Mock.Setup(x => x.Name).Returns(testPubReq1NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq1NodeId))).Returns(node1Mock.Object);

            var testPubReq1 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq1Id,
                NodeId = testPubReq1NodeId,
                SubmitterUserId = testPubReq1SubmitterUserId,
                VersionId = testPubReq1VersionId
            };

            var publicationRequestList = new List<PublicationRequest>() { testPubReq1 };

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(publicationRequestList);
            publicationRequestRepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            publicationRequestService.RemoveForDocument(2);

            publicationRequestRepositoryMock.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void RemoveForDocumentIsCalled_GivenRequestForDocumentNodeDoesntExist_RepositoryDeleteIsNotInvoked()
        {
            const int testPubReq1Id = 1;
            const int testPubReq1NodeId = 2;
            const string testPubReq1NodeName = "testPubReq1NodeName";
            const int testPubReq1SubmitterUserId = 3;
            const string testPubReq1SubmittedBy = "testPubReq1SubmittedBy";
            var testPubReq1VersionId = Guid.NewGuid().ToString();

            var userServiceMock = new Mock<IUserService>();
            var contentServiceMock = new Mock<IContentService>();

            var user1Mock = new Mock<IUser>();
            user1Mock.Setup(x => x.Name).Returns(testPubReq1SubmittedBy);
            userServiceMock.Setup(x => x.GetUserById(It.IsIn(testPubReq1SubmitterUserId))).Returns(user1Mock.Object);


            var node1Mock = new Mock<IContent>();
            node1Mock.Setup(x => x.Name).Returns(testPubReq1NodeName);
            contentServiceMock.Setup(x => x.GetById(It.IsIn(testPubReq1NodeId))).Returns(node1Mock.Object);

            var testPubReq1 = new PublicationRequest()
            {
                DateTimeQueued = DateTime.Now,
                Id = testPubReq1Id,
                NodeId = testPubReq1NodeId,
                SubmitterUserId = testPubReq1SubmitterUserId,
                VersionId = testPubReq1VersionId
            };

            var publicationRequestList = new List<PublicationRequest>() { testPubReq1 };

            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Read()).Returns(publicationRequestList);
            publicationRequestRepositoryMock.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object, userServiceMock.Object, contentServiceMock.Object);

            publicationRequestService.RemoveForDocument(2);

            publicationRequestRepositoryMock.Verify(x => x.Delete(3), Times.Never);
        }
    }
}
