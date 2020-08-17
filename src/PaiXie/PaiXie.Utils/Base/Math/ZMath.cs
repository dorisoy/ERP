using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PaiXie.Utils
{
    public class ZMath
    {

        public static decimal calcRound(decimal adTargetVal, int anDigits)
        {
            double dbTargetVal = Convert.ToDouble(adTargetVal);
            double decReturn = calcRound(dbTargetVal, anDigits);
            return Convert.ToDecimal(decReturn);
        }

        /// <summary>
        ///	四舍五入处理
        /// </summary>
        /// <param name="adTargetVal">处理对象值</param>
        /// <param name="anDigits">处理对象位数(负数时直接去位数)</param>
        /// <returns>处理结果数值</returns>
        public static double calcRound(double adTargetVal, int anDigits)
        {

            double dRet = adTargetVal;

            try
            {
                // 先转换成整数再进行计算
                dRet = dRet * System.Math.Pow(10, (double)anDigits);

                // 四舍五入处理
                dRet = dRet + (0.5 * System.Math.Sign(dRet));

                if (dRet >= 0)
                {
                    dRet = System.Math.Floor(dRet);
                }
                else
                {
                    dRet = System.Math.Ceiling(dRet);
                }

                // 还原回小数
                dRet = dRet / System.Math.Pow(10, (double)anDigits);
            }
            catch
            {
                dRet = 0;
            }

            return dRet;
        }

        /// <summary>
        ///	小数上进一处理
        /// </summary>
        /// <param name="adTargetVal">处理对象值</param>
        /// <param name="anDigits">处理对象位数(负数时直接去尾数)</param>
        /// <returns>处理结果数值</returns>
        public static double calcRoundUp(double adTargetVal, int anDigits)
        {

            double dRet = adTargetVal;

            try
            {
                // 先转换成整数再进行计算
                dRet = dRet * System.Math.Pow(10, (double)anDigits);

                // 小数上进一
                if (dRet >= 0)
                {
                    dRet = System.Math.Ceiling(dRet);
                }
                else
                {
                    dRet = System.Math.Floor(dRet);              // 负数直接去尾数取整
                }

                // 还原回小数
                dRet = dRet / System.Math.Pow(10, (double)anDigits);
            }
            catch
            {
                dRet = 0;
            }

            return dRet;

        }

        /// <summary>
        ///	小数直接舍去处理
        /// </summary>
        /// <param name="adTargetVal">处理对象值</param>
        /// <param name="anDigits">处理对象位数(负数时要上进一取整)</param>
        /// <returns>处理结果数值</returns>
        public static double calcRoundDown(double adTargetVal, int anDigits)
        {

            double dRet = adTargetVal;

            try
            {
                // 先转换成整数再进行计算
                dRet = dRet * System.Math.Pow(10, (double)anDigits);

                // 直接去尾数取整
                if (dRet >= 0)
                {
                    dRet = System.Math.Floor(dRet);
                }
                else
                {
                    dRet = System.Math.Ceiling(dRet);     // 负数要上进一取整
                }

                // 还原回小数
                dRet = dRet / System.Math.Pow(10, (double)anDigits);
            }
            catch
            {
                dRet = 0;
            }

            return dRet;

        }
    }
}
