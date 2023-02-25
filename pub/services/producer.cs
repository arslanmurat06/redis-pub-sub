using System.Text.Json;
using StackExchange.Redis;

public class Producer : BackgroundService
{

    private const string RedisConnectionString = "localhost:6379";
    private readonly ConnectionMultiplexer RedisConnection = ConnectionMultiplexer.Connect(RedisConnectionString);
    private const string Channel = "myPrivateRedis-channel";

    ILogger<Producer> _logger;

    public Producer(ILogger<Producer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = RedisConnection.GetSubscriber();

        while (!stoppingToken.IsCancellationRequested)
        {

            var message = new Message(Guid.NewGuid(), DateTime.UtcNow);

            _logger.LogInformation("Producer produces message : {message}", message);

            await subscriber.PublishAsync(Channel, JsonSerializer.Serialize<Message>(message));
            await Task.Delay(1000);
        }

    }
}