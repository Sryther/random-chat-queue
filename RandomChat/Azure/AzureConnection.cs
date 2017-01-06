using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomChat.Azure
{
    class AzureConnection
    {
        private static AzureConnection _instance = null;
        private CloudStorageAccount _storageAccount;
        private CloudQueueClient _queueClient;
        private CloudQueue _cloudQueue;

        public CloudStorageAccount StorageAccount
        {
            get { return _storageAccount; }
            private set { _storageAccount = value; }
        }

        public CloudQueueClient QueueClient
        {
            get { return _queueClient; }
            private set { _queueClient = value; }
        }

        public CloudQueue CloudQueue
        {
            get { return _cloudQueue; }
            private set { _cloudQueue = value; }
        }

        private AzureConnection()
        {
            CreateBlobStorage();
            CreateQueueClient(_storageAccount);
        }

        public static AzureConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AzureConnection();
            }
            return _instance;
        }

        private void CreateBlobStorage()
        {
            string connectionString = Constants.ConnectionString;
            StorageAccount = CloudStorageAccount.Parse(connectionString);
        }

        private void CreateQueueClient(CloudStorageAccount storageAccount)
        {
            QueueClient = storageAccount.CreateCloudQueueClient();
        }

        public void CreateQueue(string name)
        {
            CloudQueue = _queueClient.GetQueueReference(name);
            CloudQueue.CreateIfNotExists();
        }
    }
}
