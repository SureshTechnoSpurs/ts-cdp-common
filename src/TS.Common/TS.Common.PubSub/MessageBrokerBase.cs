using Microsoft.Extensions.Logging;

namespace TS.Common.PubSubs
{
    /// <summary>
    /// Common class for all Azure services
    /// </summary>
    public class MessageBrokerBase
    {
        private readonly ILogger<MessageBrokerBase> _logger;

        //public MessageBrokerBase(IAzureClientSecretCredential azureCredential, ILogger<MessageBrokerBase> logger)
        public MessageBrokerBase(  ILogger<MessageBrokerBase> logger)
        {
            //_credential = new ClientSecretCredential(azureCredential.TenantId,
            //    azureCredential.ClientId,
            //    azureCredential.ClientSecret);

            //_logger = logger;
        }
        
        protected void AddInfoLog(string message)
        {
            _logger.Log(LogLevel.Information, message);
        }

        protected void AddErrorLog(string message)
        {
            _logger.Log(LogLevel.Error, message);
        }
    }
}
