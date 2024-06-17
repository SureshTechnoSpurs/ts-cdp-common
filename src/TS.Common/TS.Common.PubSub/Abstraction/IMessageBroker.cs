using System.Threading.Tasks;

namespace Common.MessageBroker
{
    /// <summary>
    /// Represents abstraction over main functions of message broker.
    /// </summary>
    public interface IMessageBroker
    {
        /// <summary>
        /// Create all consumers of certant type of message broker,
        /// make subscribtion to nesessary queues,
        /// and run listen procceses for all of it.
        /// </summary>
        public Task CreateAndRunConsumer();
    }
}
