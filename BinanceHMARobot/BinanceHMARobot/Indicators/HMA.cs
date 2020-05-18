using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.API.Csharp.Client;
using Binance.API.Csharp.Client.Models.Enums;
using Binance.API.Csharp.Client.Models.Market;

namespace BinanceHMARobot.Indicators
{
    public enum ENUM_TREND
    {
        UP_TREND,
        DOWN_TREND,
        NO_TREND
    }

    public struct HMA_RESULT
    {
        public ENUM_TREND  Trend;
        public decimal     Value;
        public HMA_RESULT(ENUM_TREND trend, decimal value)
        {
            Trend = trend;
            Value = value;
        }
        public void Init()
        {
            Trend = ENUM_TREND.NO_TREND;
            Value = 0.0m;
        }
    }

    public class HMA: Indicator
    {
        protected int m_Period = 14;
        protected decimal[] m_Prices;
        private string m_Applied;

        public HMA(int period, string applied="Close")
        {
            m_Period = period;
            m_Applied = applied;
            m_Prices = new decimal[0];
        }
        public HMA_RESULT iHMA(IEnumerable<Candlestick> bars, int index, bool reverse=false)
        {
            BarsToDecimal(bars, ref m_Prices, m_Applied);
            HMA_RESULT zero = new HMA_RESULT(ENUM_TREND.NO_TREND, 0.0m);
            if (bars.Count() <= index) return zero;
            

            var result = Calculate(m_Prices, m_Period);
            if (result == null) return zero;
            if (reverse) return result[bars.Count() - index - 1];
            return result[index];
        }

        public HMA_RESULT[] iHMA_Array(IEnumerable<Candlestick> bars)
        {
            BarsToDecimal(bars, ref m_Prices, m_Applied);
            var result = Calculate(m_Prices, m_Period);
            return result;
        }

        public HMA_RESULT[] Calculate(decimal[] price, int period)
        {
            if (price.Length <= 0) return null;
            if (period <= 0) return null;

            var hma = new HMA_RESULT[price.Length];
            decimal [] ind_buffer1 = new decimal[price.Length];
            int draw_begin0 = Convert.ToInt32(period + Math.Floor(Convert.ToDecimal(Math.Sqrt(period))));
            for (int i = 0; i < draw_begin0; i++) ind_buffer1[i] = 0;
            for (int i = 0; i < period; i++) hma[i].Init();
            var lwma1 = MA.Calculate(ENUM_MA_TYPE.LWMA, price, Convert.ToInt32(Math.Floor(period / 2.0)));
            var lwma2 = MA.Calculate(ENUM_MA_TYPE.LWMA, price, period);

            if (lwma1 == null) return null;
            if (lwma2 == null) return null;

            for (int i = 0; i < price.Length; i++) ind_buffer1[i] = 2.0m * lwma1[i] - lwma2[i];

            var lwmaHma = MA.Calculate(ENUM_MA_TYPE.LWMA, ind_buffer1, Convert.ToInt32(Math.Floor(Math.Sqrt(period))));

            for (int i = 0; i < price.Length; i++)
            {
                hma[i].Init();
                hma[i].Value = lwmaHma[i];
                if (i == 0) continue;

                //hma[i].Trend = hma[i - 1].Trend;
                if (hma[i].Value - hma[i - 1].Value > 0) hma[i].Trend = ENUM_TREND.UP_TREND;
                else if (hma[i - 1].Value - hma[i].Value > 0) hma[i].Trend = ENUM_TREND.DOWN_TREND;
                else hma[i].Trend = ENUM_TREND.NO_TREND;
            }

            return hma;
        }
    }
}
