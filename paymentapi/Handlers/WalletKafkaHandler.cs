using Camunda.Worker;
using Confluent.Kafka;
using System.Diagnostics;
using System.Net;

namespace paymentapi.Handlers
{
    [HandlerTopics("walletpublish")]
    public class WalletKafkaHandler : IExternalTaskHandler
    {
        private readonly ILogger<WalletKafkaHandler> _logger;
        private IConfiguration _configuration;
        public WalletKafkaHandler(IConfiguration configuration,ILogger<WalletKafkaHandler> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
        }
        public async  Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
           string topicName= this._configuration["TopicName"];
            string message = externalTask.Variables["walletBalace"].Value.ToString();

            ProcessOrder(topicName, message);
            return new CompleteResult();
            
          
        }

        private async Task<string> ProcessOrder(string topicName, string message)
        {
            ProducerConfig Config = new ProducerConfig
            {
                BootstrapServers = this._configuration["BootStrapServer"],

                ClientId = Dns.GetHostName()

            };
            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(Config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topicName, new Message<Null, string>
                    {
                        Value = message
                    });

                    this._logger.LogInformation($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                    return await Task.FromResult($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Error occured: {ex.Message}");
            }

            return await Task.FromResult("Not Published.....");
        }
    }
}
