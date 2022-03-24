using Camunda.Worker;
using Camunda.Worker.Client;
using paymentapi.Bpmns;
using paymentapi.Handlers;
using System.Text.Json.Serialization;
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Camunda worker startUp
builder.Services.AddSingleton(_ => new BpmnService(configuration["RestApiUri"]));
builder.Services.AddHostedService<BpmnDeployService>();
builder.Services.AddExternalTaskClient()
    .ConfigureHttpClient((provider, client) =>
    {
        client.BaseAddress = new Uri(configuration["RestApiUri"]);
    });
builder.Services.AddCamundaWorker("PreparePaymentCamundaWorker", 1)
    .AddHandler<WalletHandler>()
    .AddHandler<WalletKafkaHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
