using log4net;
using Ninject;
using System;
using System.Collections.Generic;
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
                var publicationRequestService = Ioc.Get<IPublicationRequestService>();

                var submitPublicationRequestDto = new SubmitPublicationRequestDto();

                submitPublicationRequestDto.UserId = User.GetCurrent().Id;
                submitPublicationRequestDto.NodeId = document.Id;
                submitPublicationRequestDto.VersionId = document.Version;

                publicationRequestService.Submit(submitPublicationRequestDto);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public static List<PublicationRequestDto> GetPublicationRequests()
        {
            return Ioc.Get<IPublicationRequestService>().GetRequests();
        }

        public static void Dequeue(Document document)
        {
            try
            {
                var publicationRequestService = Ioc.Get<IPublicationRequestService>();

                publicationRequestService.RemoveForDocument(document.Id);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}