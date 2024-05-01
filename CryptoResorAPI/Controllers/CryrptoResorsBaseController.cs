using CryrptoResorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using WebSocketSharp;

namespace CryptoResorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoResorsBaseController : ControllerBase
    {
        readonly ICoinService _coinService;

        public CryptoResorsBaseController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpGet("[action]")]
        public void FollowCoin(string coinPair)
        {
            var socketUrl = $"wss://stream.binance.com:9443/ws/{coinPair.ToLower()}@kline_1m";
            var socket = new WebSocket(socketUrl);
            _coinService.FollowCoin(socket, 0.1, 0.1);
            Console.WriteLine($"WebSocket connected to {coinPair}");
        }

        [HttpGet("[action]")]
        public IActionResult FollowCoins()
        {
            var coins = new string[]
            {
                "BTCUSDT", "ETHFIUSDT", "APTUSDT", "XAIUSDT", "RDNTUSDT",
                "ARBUSDT", "ETHUSDT", "BNBUSDT", "SOLUSDT", "USDCUSDT", "XRPUSDT", "ATOMUSDT"
            };

            foreach (var coin in coins)
            {
                FollowCoin(coin);
            }

            return Ok("All coins subscribed");
        }
    }
}
