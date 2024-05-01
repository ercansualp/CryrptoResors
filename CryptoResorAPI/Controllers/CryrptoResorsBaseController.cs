using CryrptoResorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using WebSocketSharp;

namespace CryrptoResorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryrptoResorsBaseController : ControllerBase
    {
        readonly ICoinService _coinService;

        public CryrptoResorsBaseController(ICoinService coinService)
        {
            _coinService = coinService;
        }

        [HttpGet("[action]")]
        public void BTCUSDT()
        {
            WebSocket _socket = new WebSocket("wss://stream.binance.com:9443/ws/btcusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected BTCUSDT");
        }

        [HttpGet("[action]")]
        public void ETHFIUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/ethfiusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected ETHFIUSDT");
        }

        [HttpGet("[action]")]
        public void APTUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/aptusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected APTUSDT");
        }

        [HttpGet("[action]")]
        public void XAIUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/xaiusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected XAIUSDT");
        }

        [HttpGet("[action]")]
        public void RDNTUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/rdntusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected RDNTUSDT");
        }

        [HttpGet("[action]")]
        public void ARBUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/arbusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected ARBUSDT");
        }

        [HttpGet("[action]")]
        public void ETHUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/ethusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected ETHUSDT");
        }

        [HttpGet("[action]")]
        public void BNBUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/bnbusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected BNBUSDT");
        }

        [HttpGet("[action]")]
        public void SOLUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/solusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected SOLUSDT");
        }

        [HttpGet("[action]")]
        public void USDCUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/usdcusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected USDCUSDT");
        }

        [HttpGet("[action]")]
        public void XRPUSDT()
        {
            var _socket = new WebSocket("wss://stream.binance.com:9443/ws/xrpusdt@kline_1m");
            _coinService.FollowCoin(_socket, 0.1, 0.1);
            Console.WriteLine("WebSocket connected XRPUSDT");
        }
    }
}
