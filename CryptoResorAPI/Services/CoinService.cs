using CryrptoResorAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using WebSocketSharp;
using Newtonsoft.Json.Linq;

namespace CryrptoResorAPI.Services
{
    public interface ICoinService
    {
        void FollowCoin(WebSocket _socket, double percentageIncrease, double percentageDecrease);
        double CalculatePercentage(double currentPrice, double openPrice);
    }

    public class CoinService : ICoinService
    {
        readonly IHubContext<NotificationHub> _notificationHub;

        public CoinService(IHubContext<NotificationHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public void FollowCoin(WebSocket _socket, double percentageIncrease, double percentageDecrease)
        {
            int? currentMinute = null;
            _socket.OnMessage += async (sender, e) =>
            {
                JObject jsonData = JObject.Parse(e.Data);

                int openMinute = DateTimeOffset.FromUnixTimeMilliseconds((long)Convert.ToDouble(jsonData["k"]["t"])).UtcDateTime.Minute;
                if (openMinute != currentMinute)
                {
                    double currentPrice = (double)jsonData["k"]["c"];
                    double openPrice = (double)jsonData["k"]["o"];
                    double percentage = CalculatePercentage(currentPrice, openPrice);
                    if (percentage >= percentageIncrease || percentage <= -percentageDecrease)
                    {
                        await _notificationHub.Clients.All.SendAsync("ReceiveMessage", new
                        {
                            Name = jsonData["s"].ToString(),
                            Change = percentage,
                            Time = DateTimeOffset.FromUnixTimeMilliseconds((long)Convert.ToDouble(jsonData["E"])).UtcDateTime.AddHours(3).ToString("MM/dd/yyyy HH:mm:ss") + " (" + jsonData["k"]["i"] + ")"
                        });
                        currentMinute = openMinute;
                    }
                }
            };
            _socket.Connect();
        }

        /// <summary>
        /// Yüzde Hesaplama Fonksiyonu
        /// </summary>
        /// <param name="currentPrice">Şimdiki Fiyat.</param>
        /// <param name="openPrice">Mumun Açılış Fiyatı</param>
        public double CalculatePercentage(double currentPrice, double openPrice)
            => ((currentPrice - openPrice) * 100) / openPrice; // ((Şimdiki Fiyat - Mumun Açılış Fiyatı) * 100) / Mumun Açılış Fiyatı
    }
}
