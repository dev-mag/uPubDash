using System;
using System.Data;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using uPubDash.Models;
using uPubDash.Persistence;

namespace uPubDash.Tests.Unit
{
    [TestFixture]
    public class PublicationRequestServiceUnitTests
    {
        [Test]
        public void GivenValidModel_AddIsCalled_IdIsReturnedAsExpected()
        {
            //Given
            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1);
            
            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new AddPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object);
            var result = publicationRequestService.Add(validModel);

            //Then
            result.Should().Be(1);
        }

        [Test]
        public void GivenValidModelAndErroringRepository_AddIsCalled_RepositoryExceptionIsRaised()
        {
            //Given
            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Throws(new DataException());

            const int testNodeId = 2;
            const int testUserId = 3;
            var testVersionId = Guid.NewGuid();

            var validModel = new AddPublicationRequestDto()
            {
                NodeId = testNodeId,
                UserId = testUserId,
                VersionId = testVersionId
            };

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object);

            Action action = () => publicationRequestService.Add(validModel);

            //Then
            action.ShouldThrow<DataException>();
        }

        [Test]
        public void GivenNullModel_AddIsCalled_RepositoryIsCalledAsExpected()
        {
            //Given
            var publicationRequestRepositoryMock = new Mock<IPublicationRequestRepository>();
            publicationRequestRepositoryMock.Setup(x => x.Create(It.IsAny<PublicationRequest>())).Returns(1);

            //When
            var publicationRequestService = new PublicationRequestService(publicationRequestRepositoryMock.Object);

            Action action = () => publicationRequestService.Add(null);

            //Then
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}
