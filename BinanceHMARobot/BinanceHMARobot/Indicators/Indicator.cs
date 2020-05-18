using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Csharp.Client.Models.Market;


namespace BinanceHMARobot.Indicators
{
    public enum ENUM_MA_TYPE
    {
        SMA,
        LWMA,
        EMA,
    };


    public abstract class Indicator
    {
        /// <summary>
        /// Gets or sets the indicator name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the indicator short name.
        /// </summary>
        public string ShortName { get; protected set; }

        public void BarsToDecimalBack(IEnumerable<Candlestick> candles, ref decimal[] _prices, string _applied = "Close", bool Continue = false)
        {
            int curr = _prices.Length;
            if (curr < candles.Count())
            {
                Array.Resize(ref _prices, candles.Count());
            }

            int index = 0;
            int end = curr - 1;
            if (end < 0) end = 0;
            if (!Continue) end = 0;

            Candlestick[] bars = candles.ToArray();

            for (int i = bars.Count() - 1; i >= end; i--, index++)
            {
                switch (_applied)
                {
                    case "Close":
                        _prices[i] = bars[index].Close;
                        break;
                    case "High":
                        _prices[i] = bars[index].High;
                        break;
                    case "Low":
                        _prices[i] = bars[index].Low;
                        break;
                    case "Open":
                        _prices[i] = bars[index].Open;
                        break;
                    case "Typical":
                        _prices[i] = Convert.ToDecimal((bars[index].High + bars[index].Low + bars[index].Close) / Convert.ToDecimal(3.0));
                        break;
                    case "Median":
                        _prices[i] = Convert.ToDecimal((bars[index].High + bars[index].Low) / Convert.ToDecimal(2.0));
                        break;
                    case "Weighted":
                        _prices[i] = Convert.ToDecimal((bars[index].High + bars[index].Low + bars[index].Close + bars[index].Close) / Convert.ToDecimal(4.0));
                        break;
                    default:
                        _prices[i] = bars[index].Close;
                        break;
                }
            }
        }

        public void BarsToDecimal(IEnumerable<Candlestick> candles, ref decimal[] _prices, string _applied = "Close", bool Continue=false)
        {
            int curr = _prices.Length;
            if (curr < candles.Count())
            {
                Array.Resize(ref _prices, candles.Count());
            }

            int start = curr - 1;
            if (start < 0) start = 0;
            if (!Continue) start = 0;

            Candlestick[] bars = candles.ToArray();

            for (int i = start; i < bars.Count(); i++)
            {
                switch (_applied)
                {
                    case "Close":
                        _prices[i] = bars[i].Close;
                        break;
                    case "High":
                        _prices[i] = bars[i].High;
                        break;
                    case "Low":
                        _prices[i] = bars[i].Low;
                        break;
                    case "Open":
                        _prices[i] = bars[i].Open;
                        break;
                    case "Typical":
                        _prices[i] = Convert.ToDecimal((bars[i].High + bars[i].Low + bars[i].Close) / Convert.ToDecimal(3.0));
                        break;
                    case "Median":
                        _prices[i] = Convert.ToDecimal((bars[i].High + bars[i].Low) / Convert.ToDecimal(2.0));
                        break;
                    case "Weighted":
                        _prices[i] = Convert.ToDecimal((bars[i].High + bars[i].Low + bars[i].Close + bars[i].Close) / Convert.ToDecimal(4.0));
                        break;
                    default:
                        _prices[i] = bars[i].Close;
                        break;
                }
            }
        }
    }
}
