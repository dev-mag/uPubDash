using System;
using log4net;
using Ninject;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using uPubDash.DependencyInjection;
using uPubDash.Models;
using uPubDash.Persistence;
using uPubDash.Services;

namespace uPubDash
{
    public static class uPubDash
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(uPubDash));

        public static void Initialise()
        {
            Ioc.Initialize(NinjectIocContainer.Create(new StandardKernel(new uPubDashModule())));
            TableFactory.CreateTables();
        }

        public static void Enqueue(Document document)
        {
            try
            {
                var addPublicationRequest = new AddPublicationRequestDto();

                addPublicationRequest.UserId = User.GetCurrent().Id;
                addPublicationRequest.NodeId = document.Id;
                addPublicationRequest.VersionId = document.Version;

                var publicationRequestService = Ioc.Get<IPublicationRequestService>();

                publicationRequestService.Add(addPublicationRequest);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            
        }
    }
}