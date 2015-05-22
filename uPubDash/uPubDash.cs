using Ninject;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using uPubDash.DependencyInjection;
using uPubDash.Persistence;

namespace uPubDash
{
    public class uPubDash
    {
        public static void Initialise()
        {
            TableFactory.CreateTables();
        }

        public static void Enqueue(Document sender, SendToPublishEventArgs e)
        {
            Ioc.Initialize(NinjectIocContainer.Create(new StandardKernel(new uPubDashModule())));
        }
    }
}