using System;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using WebshopProt2.Models.WebShop;

namespace SBMDispatcher
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single,
               ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InboundMessageHandlerService : IInboundMessageHandlerService
    {
        public void ProcessIncomingMessage(MsmqMessage<ContactUS> incomingOrderMessage)
        {
            var orderRequest = incomingOrderMessage.Body;
            Console.WriteLine(orderRequest.ContactID);
            Console.WriteLine(orderRequest.Email);
            Console.WriteLine(orderRequest.Message);
            Console.WriteLine(orderRequest.DateofMessage);
        }
    }
}
