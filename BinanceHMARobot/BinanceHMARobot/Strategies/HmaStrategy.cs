using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Csharp.Client;
using BinanceHMARobot.Indicators;
using BinanceHMARobot.BinanceAPI;
using Binance.API.Csharp.Client.Models.Market.TradingRules;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.Account;

namespace BinanceHMARobot.Strategies
{
    public class HmaStrategy
    {
        private HMA m_Hma = null;
        private string m_Symbol = "";
        private string m_TimeInterval = "";
        private int m_Period = 14;
        private string m_Applied = "Close";
        private decimal m_Quantity = 1;
        private NewOrder m_NewOrder = null;

        public HmaStrategy(string symbol, 
                    string strTimeInterval, int period, string applied, decimal quantity, BinanceApiClient client, Order last=null)
        {
            m_Symbol = symbol;
            m_TimeInterval = strTimeInterval;
            m_Period = period;
            m_Applied = applied;
            m_Quantity = quantity;
            m_Hma = new HMA(m_Period, m_Applied);
            try
            {
                if (last == null) m_NewOrder = null;
                else
                {
                    var candles = client.GetCandleStick(m_Symbol, m_TimeInterval);
                    if (candles == null)
                    {
                        m_NewOrder = null;
                    }
                    else
                    {
                        var hmaArray = m_Hma.iHMA_Array(candles);
                        int preSignal = -1;
                        for (int i = hmaArray.Length - 1; i >= 0; i--)
                        {
                            if (last.Side == "BUY")
                            {
                                if (hmaArray[i].Trend != ENUM_TREND.UP_TREND)
                                {
                                    preSignal = i;
                                    break;
                                }
                            }
                            else if (last.Side == "SELL")
                            {
                                if (hmaArray[i].Trend != ENUM_TREND.DOWN_TREND)
                                {
                                    preSignal = i;
                                    break;
                                }
                            }
                        }
                        if (preSignal < 0 || preSignal >= hmaArray.Length) m_NewOrder = null;
                        else
                        {
                            if (candles.ElementAt(preSignal).CloseTime >= last.Time) m_NewOrder = null;
                            else
                            {
                                m_NewOrder = new NewOrder();
                                m_NewOrder.OrderId = last.OrderId;
                                m_NewOrder.ClientOrderId = last.ClientOrderId;
                                m_NewOrder.Symbol = symbol;
                                m_NewOrder.TransactTime = last.Time;
                            }
                        }
                    }
                }
            }
            catch
            {
                m_NewOrder = null;
            }
        }

        
        
        public string GetSymbol() { return m_Symbol; }
        public bool IsExisted(string symbol, string timeInterval)
        {
            if (m_Symbol == symbol && m_TimeInterval == timeInterval) return true;
            return false;
        }

        public HMA_RESULT Engine(BinanceApiClient client)
        {
            HMA_RESULT hma = new HMA_RESULT(ENUM_TREND.NO_TREND, 0.0m);
            if (!client.GetServerConnection()) return hma; // server disconnected

            var candles = client.GetCandleStick(m_Symbol, m_TimeInterval);
            if (candles == null) return hma; // candle sticks is null
            try
            {
                hma = m_Hma.iHMA(candles, 1, true);
                if (hma.Trend == ENUM_TREND.NO_TREND)
                {
                    //CloseOrder(client);
                    m_NewOrder = null;
                    return hma; // No Signal
                }
                if (hma.Trend == ENUM_TREND.UP_TREND)
                {
                    int result = CloseOrder(client, "SELL");
                    if (result == 1) return hma; // Buy Order Existing
                    m_NewOrder = client.PostNewOrder(m_Symbol, m_Quantity, 0.0m, OrderSide.BUY, OrderType.MARKET);
                }
                else if (hma.Trend == ENUM_TREND.DOWN_TREND)
                {
                    int result = CloseOrder(client, "BUY");
                    if (result == 1) return hma; // SELL Order Existing
                    m_NewOrder = client.PostNewOrder(m_Symbol, m_Quantity, 0.0m, OrderSide.SELL, OrderType.MARKET);
                }
                return hma; // Sumit Order
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public int CloseAllOrders(BinanceApiClient client, string symbol)
        {
            var orders = client.GetCurrentOpenOrders(symbol);
            foreach (var order in orders)
            {
                if (order.Side == "BUY")
                {
                    client.PostNewOrder(m_Symbol, order.ExecutedQty, 0.0m, OrderSide.SELL, OrderType.MARKET);
                }
                else if (order.Side == "SELL")
                {
                    client.PostNewOrder(m_Symbol, order.ExecutedQty, 0.0m, OrderSide.BUY, OrderType.MARKET);
                }
            }
            return 0;
        }

        public int CloseOrder(BinanceApiClient client, string side = "")
        {
            if (m_NewOrder == null) return -1;
            var order = client.GetOrder(m_Symbol, m_NewOrder.OrderId);
            if (side != "" && order.Side != side) return 1;

            /*if (order.Status == "FILLED" || order.Status == "PARTIALLY_FILLED")
            {
                if (order.Side == "BUY")
                {
                    client.PostNewOrder(m_Symbol, order.ExecutedQty, 0.0m, OrderSide.SELL, OrderType.MARKET);
                }
                else if (order.Side == "SELL")
                {
                    client.PostNewOrder(m_Symbol, order.ExecutedQty, 0.0m, OrderSide.BUY, OrderType.MARKET);
                }
            }
            else if (order.Status == "NEW")
            {
                client.CancelOrder(m_Symbol, order.OrderId);
            }*/
            m_NewOrder = null;
            return 0;
        }

    }
}
