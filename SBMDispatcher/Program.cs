using System;
using System.Configuration;
using System.Messaging;
using System.ServiceModel;
using System.Transactions;
using WebshopProt2.Models.WebShop;

namespace SBMDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a fake order, for simulation:
            var message = new ContactUS { ContactID =15, Email ="asd@asd.com", Message = "Just a thingie", DateofMessage  = DateTime.UtcNow };

            // create a MessageQueue to tell MSMQ where to send the message and how to connect to it
            var queue = new MessageQueue(ConfigurationManager.AppSettings["MessageQueuePath"]);

            // Create a Message and set the body to the order object above
            var msg = new Message { Body = message };

            // Create a transaction
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                queue.Send(msg, MessageQueueTransactionType.Automatic); // send the message
                ts.Complete(); // complete the transaction
            }

            Console.WriteLine("Message Sent");
            Console.ReadLine();
        }
    }
}