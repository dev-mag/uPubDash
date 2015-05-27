using log4net;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace uPubDash.Persistence
{
    public static class TableFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (TableFactory));

        public static void CreateTables()
        {
            var databaseContext = ApplicationContext.Current.DatabaseContext;

            if (!databaseContext.IsDatabaseConfigured)
            {
                Log.Debug("Database is not configured, skipping uPubDash table creation");
                return;
            }

            var database = databaseContext.Database;

            try
            {
                database.OpenSharedConnection();

                if (database.TableExist("PublicationRequestQueue"))
                {
                    Log.Debug("PublicationRequestQueue table already exists.");
                    return;
                }

                using (var transaction = database.GetTransaction())
                {
                    database.CreateTable<PublicationRequest>();
                    transaction.Complete();
                    Log.Info("Created PublicationRequestQueue table.");
                }
            }
            finally
            {
                database.CloseSharedConnection();
            }
        }
    }
}