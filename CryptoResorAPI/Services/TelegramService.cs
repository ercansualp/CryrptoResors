using Telegram.Bot;

namespace CryrptoResorAPI.Services
{
    public interface ITelegramService
    {
        Task SendMessageToGroupAsync(string message);
    }

    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        readonly IConfiguration _configuration;

        public TelegramService(IConfiguration configuration)
        {
            _botClient = new TelegramBotClient(configuration["TelegramBot:BotToken"]);
            _configuration = configuration;
        }

        public async Task SendMessageToGroupAsync(string message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(_configuration["TelegramBot:CryptoResorGroupId"], message);
                Console.WriteLine($"Message sent to group {_configuration["TelegramBot:CryptoResorGroupId"]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}
