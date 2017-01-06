using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using RandomChat.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomChat.Services
{
    class MessageService
    {
        private static MessageService _instance = null;

        private MessageService()
        {
        }

        public static MessageService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MessageService();
            }
            return _instance;
        }

        public void Post(string message)
        {
            CloudQueue queue = AzureConnection.GetInstance().CloudQueue;
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessage(queueMessage);
        }

        public string Get()
        {
            CloudQueue queue = AzureConnection.GetInstance().CloudQueue;
            CloudQueueMessage retrievedMessage = queue.GetMessage();
            if (retrievedMessage != null)
            {
                string message = retrievedMessage.AsString;
                queue.DeleteMessage(retrievedMessage);
                return message;
            }
            return null;
        }
    }
}
