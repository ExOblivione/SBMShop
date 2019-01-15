using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using WebshopProt2.Models.WebShop;

namespace SBMDispatcher
{
    [ServiceContract]
    [ServiceKnownType(typeof(ContactUS))]
    public interface IInboundMessageHandlerService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessIncomingMessage(MsmqMessage<ContactUS> incomingOrderMessage);
    }
}
