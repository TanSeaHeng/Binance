using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceHMARobot.Indicators
{
    
    public class MA : Indicator
    {
        public MA()
        {
            this.Name = "Moving Average";
            this.ShortName = "MA";
        }

        /// <summary>
        /// Calculates indicator.
        /// </summary>
        /// <param name="price">Price series.</param>
        /// <param name="period">Indicator period.</param>
        /// <returns>Calculated indicator series.</returns>
        /// 

        public static decimal[] Calculate(ENUM_MA_TYPE maType, decimal[] price, int period)
        {
            switch (maType)
            {
                case ENUM_MA_TYPE.SMA:
                    return SimpleMovingAverage(price, period);
                case ENUM_MA_TYPE.LWMA:
                    return LWMovingAverage(price, period);
                case ENUM_MA_TYPE.EMA:
                    return EMovingAverage(price, period);
                default:
                    break;
            }
            return SimpleMovingAverage(price, period);
        }

        public static decimal[] SimpleMovingAverage(decimal[] price, int period)
        {
            if (price.Length <= 0) return null;
            if (period <= 0) return null;

            var sma = new decimal[price.Length];

            decimal sum = 0.0m;

            for (var i = 0; i < period; i++)
            {
                sum += price[i];
                sma[i] = sum / (i + 1);
            }

            for (var i = period; i < price.Length; i++)
            {
                sum = 0;
                for (var j = i; j > i - period; j--)
                {
                    sum += price[j];
                }

                sma[i] = sum / period;
            }

            return sma;
        }

        public static decimal[] LWMovingAverage(decimal[] price, int period)
        {
            if (price.Length <= 0) return null;
            if (period <= 0) return null;

            var lwma = new decimal[price.Length];
            decimal avgsum = 0.0m;
            decimal sum = 0.0m;
            for (int i = 0; i < period - 1; i++)
            {
                avgsum += price[i] * (i + 1);
                sum += price[i];
            }

            var divider = period * (period + 1) / 2;
            for (int i = period - 1; i < price.Length; i++)
            {
                avgsum += price[i] * period;
                sum += price[i];
                lwma[i] = avgsum / divider;
                avgsum -= sum;
                sum -= price[i - period + 1];
            }

            return lwma;
        }

        public static decimal[] EMovingAverage(decimal[] price, int period)
        {
            if (price.Length <= 0) return null;
            if (period <= 0) return null;

            /* Define EMA Function */
            var ema = new decimal[price.Length];
            decimal sum = price[0];
            decimal coeff = Convert.ToDecimal(2.0 / (1.0 + period));

            for (int i = 0; i < price.Length; i++)
            {
                sum += coeff * (price[i] - sum);
                ema[i] = sum;
            }

            /* End */
            return ema;
        }
    }
}
