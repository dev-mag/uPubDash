using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Persistence;

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
            var databaseContext = ApplicationContext.Current.DatabaseContext;

            if(!databaseContext.IsDatabaseConfigured)
            {
                throw new DataException("Database is not configured");
            }

            var db = databaseContext.Database;

            var publicationRequests = new List<PublicationRequest>();

            try
            {
                var query = new Sql().Select("*").From("uPubDash");

                db.OpenSharedConnection();

                publicationRequests = db.Fetch<PublicationRequest>(query);
            }
            finally
            {
                db.CloseSharedConnection();
            }

            return publicationRequests;
        }

        public PublicationRequest Read(int publicationRequestId)
        {
            return Read().FirstOrDefault(request => request.Id == publicationRequestId);
        }

        public int Update(PublicationRequest publicationRequest)
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
                db.Update(publicationRequest);
                Debug.WriteLine("**** publication request updated ****");
            }
            finally
            {
                db.CloseSharedConnection();
            }

            return publicationRequest.Id;
        }

        public void Delete(int publicationRequestId)
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;

            if(!databaseContext.IsDatabaseConfigured)
            {
                throw new DataException("Database is not configured");
            }

            var db = databaseContext.Database;

            try
            {
                db.OpenSharedConnection();
                db.Delete<PublicationRequest>("WHERE Id = @0", publicationRequestId);
            }
            finally
            {
                db.CloseSharedConnection();
            }
        }
    }
}