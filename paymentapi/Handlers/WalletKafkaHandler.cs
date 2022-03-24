using Camunda.Worker;

namespace paymentapi.Handlers
{
    [HandlerTopics("walletpublish")]
    public class WalletKafkaHandler : IExternalTaskHandler
    {
        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
