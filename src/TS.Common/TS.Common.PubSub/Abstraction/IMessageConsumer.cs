using System.Threading.Tasks;

namespace Common.MessageBroker
{
    /// <summary>
    /// Represents abstraction over Events\Messages comsuption object that handle certant type of message.
    /// </summary>
    /// <typeparam name="TMessage">Consumer message type.</typeparam>
    public interface IMessageConsumer<TMessage>
    {
        /// <summary>
        /// Procces incoming service bus messages.
        /// </summary>
        /// <param name="message">Passed message from service bus.</param>
        Task MessageHandler(TMessage message);
    }
}
