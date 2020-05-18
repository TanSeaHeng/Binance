//#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.Market;
using Binance.API.Csharp.Client.Models.Account;
using Nito.AsyncEx;
using ServiceStack;
using Newtonsoft.Json;

namespace BinanceHMARobot.BinanceAPI
{
    public class BinanceApiClient
    {
        private ApiClient m_ApiClient = null;
        private BinanceClient m_BinanceClient = null;
        private string m_ApiKey = "";
        private string m_ApiSecret = "";
        private string m_ListenKey = "";
        private string m_ApiURL = "https://api.binance.com";
        private string m_WebSocketEndPoint = "wss://stream.binance.com:9443/ws/";

        public BinanceApiClient(string apiKey, string apiSecret)
        {
            m_ApiKey = apiKey;
            m_ApiSecret = apiSecret;
            m_ApiClient = new ApiClient(apiKey, apiSecret, m_ApiURL, m_WebSocketEndPoint, true);
            m_BinanceClient = new BinanceClient(m_ApiClient);
            //m_ListenKey = StartUserStream();
        }
        ~BinanceApiClient()
        {
            m_ApiClient = null;
            m_BinanceClient = null;
            m_ApiKey = "";
            m_ApiSecret = "";
            //CloseUserStream(m_ListenKey);
        }
        public DateTime GetServerTime()
        {
            if (m_BinanceClient == null) return DateTime.MinValue;
            long time = m_BinanceClient.GetServerTime().Result.ServerTime;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(time).ToLocalTime();
            return date;
        }

        public bool GetServerConnection()
        {
            if (m_BinanceClient == null) return false;
            var test= m_BinanceClient.TestConnectivity().Result;
            var res = JsonConvert.SerializeObject(test);
            if (res == "{}") return true;
            //task.Wait();
            //var test = task.Result;
            //if (test == null) return false;
            return false;
        }

        public IEnumerable<SymbolPrice> GetAllPriceTickers()
        {
            if (m_BinanceClient == null) return null;
            var tickerPrices = m_BinanceClient.GetAllPrices().Result;
            return tickerPrices;
        }

        public decimal GetCurrentPrice(string symbol)
        {
            if (m_BinanceClient == null) return 0;
            var tickerPrices = GetAllPriceTickers();
            if (tickerPrices.Count() <= 0) return 0;

            foreach (var item in tickerPrices)
            {
                if (item.Symbol == symbol)
                    return item.Price;
            }
            return 0;
        }

        public OrderBook GetOrderBook(string symbol, int limit = 100)
        {
            if (m_BinanceClient == null) return null;
            var orderBook = m_BinanceClient.GetOrderBook(symbol, limit).Result;
            return orderBook;
        }

        public IEnumerable<Candlestick> GetCandleStick(string symbol, string strInterval)
        {
            if (m_BinanceClient == null) return null;
            var candlestick = m_BinanceClient.GetCandleSticks(symbol, ConvertToTimeinterval(strInterval)).Result;
            return candlestick;
        }

        public TimeInterval ConvertToTimeinterval(string strInterval)
        {
            switch (strInterval)
            {
                case "M1":
                    return TimeInterval.Minutes_1;
                case "M5":
                    return TimeInterval.Minutes_5;
                case "M15":
                    return TimeInterval.Minutes_15;
                case "M30":
                    return TimeInterval.Minutes_30;
                case "H1":
                    return TimeInterval.Hours_1;
                case "H4":
                    return TimeInterval.Hours_4;
                case "D1":
                    return TimeInterval.Days_1;
                case "W1":
                    return TimeInterval.Weeks_1;
                case "MN1":
                    return TimeInterval.Months_1;
                default:
                    break;
            }
            return TimeInterval.Hours_1;
        }

        public AccountInfo GetAccountInfo(long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var accountInfo = m_BinanceClient.GetAccountInfo(recvWindow).Result;
            return accountInfo;
        }

        public IEnumerable<Trade> GetTradesList(string symbol, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var tradeList = m_BinanceClient.GetTradeList(symbol, recvWindow).Result;
            return tradeList;
        }

        public WithdrawResponse SubmitWithdrawRequest(string asset, decimal amount, string address, string addressName = "", long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var withdrawResult = m_BinanceClient.Withdraw(asset, amount, address, addressName, recvWindow).Result;
            return withdrawResult;
        }

        public DepositHistory GetDepositHistory(string asset, DepositStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var depositHistory = m_BinanceClient.GetDepositHistory(asset, status, startTime, endTime, recvWindow).Result;
            return depositHistory;
        }
        public WithdrawHistory GetWithdrawHistory(string asset, WithdrawStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var withdrawHistory = m_BinanceClient.GetWithdrawHistory(asset, status, startTime, endTime, recvWindow).Result;
            return withdrawHistory;
        }

        public NewOrder PostNewOrder(string symbol, decimal quantity, decimal price, OrderSide side, OrderType orderType = OrderType.MARKET, TimeInForce timeInForce = TimeInForce.GTC, decimal icebergQty = 0m, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
#if (DEBUG) 
            var order = m_BinanceClient.PostNewOrderTest(symbol, quantity, price, side, orderType, timeInForce, icebergQty, recvWindow).Result;
            var res = JsonConvert.SerializeObject(order);
            return null;
#else
            try
            {
                var order = m_BinanceClient.PostNewOrder(symbol, quantity, price, side, orderType, timeInForce, icebergQty, recvWindow).Result;
                return order;
            }
            catch (Exception e)
            {
                throw e;
            }
#endif
        }

        public CanceledOrder CancelOrder(string symbol, long? orderId = null, string origClientOrderId = null, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var canceledOrder = m_BinanceClient.CancelOrder(symbol, orderId, origClientOrderId, recvWindow).Result;
            return canceledOrder;
        }

        public IEnumerable<Order> GetCurrentOpenOrders(string symbol, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var openOrders = m_BinanceClient.GetCurrentOpenOrders(symbol, recvWindow).Result;
            return openOrders;
        }

        public IEnumerable<Order> GetAllOrders(string symbol, long? orderId = null, int limit = 500, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var allOrders = m_BinanceClient.GetAllOrders(symbol, orderId, limit, recvWindow).Result;
            return allOrders;
        }

        public Order GetOrder(string symbol, long? orderId = null, string origClientOrderId = null, long recvWindow = 5000)
        {
            if (m_BinanceClient == null) return null;
            var order = m_BinanceClient.GetOrder(symbol, orderId, origClientOrderId, recvWindow).Result;
            return order;
        }

        public string StartUserStream()
        {
            if (m_BinanceClient == null) return null;
            string listenKey = m_BinanceClient.StartUserStream().Result.ListenKey;
            return listenKey;
        }

        public void KeepAliveUserStream(string listenkey)
        {
            if (m_BinanceClient == null) return;
            var ping = m_BinanceClient.KeepAliveUserStream(listenkey).Result;
        }
        public void CloseUserStream(string listenkey="")
        {
            if (m_BinanceClient == null) return;
            
            if (listenkey == "")
            {
                var ret = m_BinanceClient.CloseUserStream(m_ListenKey).Result;
            }
            else
            {
                var res = m_BinanceClient.CloseUserStream(listenkey).Result;
            }
        }
    }
}
