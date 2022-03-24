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
        private IConfiguration _configuration;
        private CamundaClient _client;
        public WalletController(ILogger<WalletController> logger,IConfiguration configuration)
        {
            this._configuration = configuration;
            this._logger = logger;
            _client = CamundaClient.Create(this._configuration["RestApiUri"]);
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

        [HttpGet("processDefinitions")]
        //camunda rest api calls
        public async Task<IActionResult> GetProcessDefinitions()
        {
            using var client = new HttpClient();

            var url = this._configuration["RestApiUri"] + "process-definition";
            var result = await client.GetAsync(url);
            return Ok(result.Content.ReadAsStringAsync());
            //return Ok(this._client.ProcessDefinitions);
        }
        [HttpPost("tasks")]
        public  async Task<IActionResult> GetTasksList()
        {
            using var client = new HttpClient();

            var url = this._configuration["RestApiUri"] + "task";
            var result = await client.GetAsync(url);
            /*
            var contentStream = await result.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(contentStream);
            using var jsonReader = new JsonTextReader(streamReader);

            JsonSerializer serializer = new JsonSerializer();
            TaskList TaskList = null;

            try
            {
               TaskList=  serializer.Deserialize<TaskList>(jsonReader);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Invalid JSON.");
            }
            return TaskList;
            */
            return Ok(result.Content.ReadAsStringAsync());
        }

    }
    public enum MyBPMNProcess
    {
        Process_Wallet
    }
}
