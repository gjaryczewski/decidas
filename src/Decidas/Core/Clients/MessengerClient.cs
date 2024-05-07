namespace Decidas.Core.Clients;

public class MessengerClient(ILogger<MessengerClient> _logger)
{
    public async Task SendAsync(Message message)
    {
        _logger.LogInformation("Sending message: {message}", message);

        await Task.CompletedTask;
    }
}

public record Message(string Subject, string Body, DateTime PublishTime);
