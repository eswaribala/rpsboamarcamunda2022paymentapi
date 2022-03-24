namespace paymentapi.Bpmns
{
    public class BpmDeployService : IHostedService
    {
        private readonly BpmnService bpmnService;
        public BpmDeployService(BpmnService bpmnService)
        {
            this.bpmnService = bpmnService;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await bpmnService.DeployProcessDefinition();

            await bpmnService.CleanupProcessInstances();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
