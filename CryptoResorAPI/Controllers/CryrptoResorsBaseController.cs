using CryrptoResorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using WebSocketSharp;
using TicTacTec.TA.Library;

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

        [HttpGet("[action]")]
        public async Task Test()
        {
            // 

            // 70006.23, 67116.52, 63924.51, 65661.84, 63419.99, 63793.39,
            //Örnek veri
            double[] closePrices = { 
                61277.37, 63470.08, 63818.01, 64940.59, 64941.15, 66819.32, 66414.00, 
                64289.59, 64498.34, 63770.01, 63461.98, 63118.62, 63866.00, 60672.00, 
                58364.97 };
            // , 59060.61
            //RSI hesaplama
            int outBegIdx, outNbElement;
            double[] rsi = new double[closePrices.Length];
            Core.RetCode retCode = Core.Rsi(
                0, closePrices.Length - 1, closePrices, 14, out outBegIdx, out outNbElement, rsi
                );

            if (retCode == Core.RetCode.Success)
            {
                for (int i = 0; i < outNbElement; i++)
                {
                    Console.WriteLine($"RSI[{i}] = {rsi[i]}");
                }
            }
            else
            {
                Console.WriteLine("Hesaplama başarısız ol");
            }
        }
    }
}
