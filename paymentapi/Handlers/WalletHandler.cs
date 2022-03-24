﻿
using Camunda.Worker;
using paymentapi.Models;

namespace paymentapi.Handlers
{
    [HandlerTopics("wallet")]
    public class WalletHandler : IExternalTaskHandler
    {
        private readonly ILogger<WalletHandler> _logger;
        public WalletHandler(ILogger<WalletHandler> logger)
        {
            _logger = logger;
        }
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Wallet handler is called from Camunda...");
            try
            {
                _logger.LogInformation($"Payment processing..........");
                //Mimicking operation
                Task.Delay(500).Wait();
                //Wallet wallet = (Wallet)externalTask.Variables["wallet"].Value;

               // return new CompleteResult();

                return new CompleteResult
                {
                    Variables = new Dictionary<string, Variable>
                    {
                        ["walletBalance"] = new Variable(externalTask.Variables["walletBalance"], VariableType.Long)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured!! error messge: {ex.Message}");
                //return failure
                return new BpmnErrorResult("Wallet Failure", "Error occured while processing payment..");
            }
        }
    }
}
