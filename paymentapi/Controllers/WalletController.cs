using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paymentapi.Models;

namespace paymentapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private CamundaClient _client;
        public WalletController(ILogger<WalletController> logger)
        {
            this._logger = logger;
            _client = CamundaClient.Create("http://localhost:8080/engine-rest/");
        }
        [HttpGet("startProcess")]
        public IActionResult StartProcess(MyBPMNProcess myBPMNProcess)
        {
            _logger.LogInformation("Starting the sample Camunda process...");
            try
            {
                Random random = new Random();

                //Creating process parameters
                StartProcessInstance processParams;

                Wallet wallet = new Wallet
                {
                   WalletId=29469,
                   WalletBalance=464724,
                   WalletName="HDFC Wallet",
                   WalletVersion=1
                };

                processParams = new StartProcessInstance()
                   
                   .SetVariable("wallet", JsonConvert.SerializeObject(wallet));

                _logger.LogInformation($"Camunda process to demonstrate Saga based orchestrator started..........");


                //Startinng the process
                var proceStartResult = _client.ProcessDefinitions.ByKey(myBPMNProcess.ToString())
                    .StartProcessInstance(processParams);

                //return Ok("Done");

                return Ok(proceStartResult.Result.DefinitionId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured!! error messge: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
    public enum MyBPMNProcess
    {
        Process_Wallet
    }
}
