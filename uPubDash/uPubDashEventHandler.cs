using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;

namespace uPubDash
{
    public class uPubDashEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            uPubDash.Initialise();

            Document.AfterSendToPublish += DocumentOnAfterSendToPublish;

        }

        private void DocumentOnAfterSendToPublish(Document sender, SendToPublishEventArgs sendToPublishEventArgs)
        {
            uPubDash.Enqueue(sender);
        }
    }
}