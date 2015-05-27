using System.Collections.Generic;
using System.Data;
using Umbraco.Core;

namespace uPubDash.Persistence
{
    public class PublicationRequestRepository : IPublicationRequestRepository
    {
        public int Create(PublicationRequest publicationRequest)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;

            if (!databaseContext.IsDatabaseConfigured)
            {
                throw new DataException("Database is not configured");
            }

            var db = databaseContext.Database;

            try
            {
                db.OpenSharedConnection();
                db.Insert(publicationRequest);
            }
            finally
            {
                db.CloseSharedConnection();
            }

            return publicationRequest.Id;

        }

        public List<PublicationRequest> Read()
        {
            throw new System.NotImplementedException();
        }

        public PublicationRequest Read(int publicationRequestId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PublicationRequest publicationRequest)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int publicationRequestId)
        {
            throw new System.NotImplementedException();
        }
    }
}