using Microsoft.Azure;

namespace RandomChat
{
    static class Constants
    {
        public static string ConnectionString = CloudConfigurationManager.GetSetting("StorageAccountConnectionString");
        public static string BlobContainerNameKey = CloudConfigurationManager.GetSetting("StorageContainerString");
    }
}