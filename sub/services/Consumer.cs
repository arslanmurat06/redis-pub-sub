using System.Text.Json;
using StackExchange.Redis;

public class Consumer : BackgroundService
{
    private const string RedisConnectionString = "localhost:6379";
    private readonly ConnectionMultiplexer RedisConnection = ConnectionMultiplexer.Connect(RedisConnectionString);
    private const string Channel = "myPrivateRedis-channel";

    ILogger<Consumer> _logger;

    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = RedisConnection.GetSubscriber();

        await subscriber.SubscribeAsync(Channel, (channel, message) =>
        {

            var msg = JsonSerializer.Deserialize<Message>(message);
            _logger.LogInformation("{Message} taken by consumer", msg);
        });

    }

}
