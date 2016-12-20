using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class DateTimeParse
    {
        public static DateTime Convert(DateTime dt)
        {
            if (dt == NullDate)
                dt = DateTime.Now;
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public static DateTime Convert(DateTime? dt)
        {
            if (dt == null)
            {
                dt = DateTime.Now;
            }
            return Convert(dt.Value);
        }
        public static bool DateTimeEquls(DateTime dt, DateTime dt1)
        {
            if (dt.Year != dt1.Year)
                return false;
            if (dt.Month != dt1.Month)
                return false;
            if (dt.Day != dt1.Day)
                return false;
            if (dt.Hour != dt1.Hour)
                return false;
            if (dt.Minute != dt1.Minute)
                return false;
            if (dt.Second != dt1.Second)
                return false;
            return true;
        }
        public static bool DateTimeEquls(DateTime? date1, DateTime? date2)
        {
            if (date1 == null)
            {
                return false;
            }
            return DateTimeEquls(date1.Value, date2.Value);
        }

        //空时间
        public static readonly DateTime NullDate = new DateTime(1900, 1, 1, 12, 0, 0);
        //系统过期时间
        public static readonly DateTime InvalidDate = new DateTime(2099, 5, 16, 12, 0, 0);
        public static readonly DateTime EndDate = new DateTime(2100, 1, 1, 12, 0, 0);
        /// <summary>
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的Decimal类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
        /// <returns></returns>
        public static decimal GetSiSheWuRu(decimal objTarget, int mIndex)
        {
            decimal a1 = decimal.Parse(Math.Pow(10, mIndex).ToString());
            decimal a2 = decimal.Parse(Math.Pow(10, mIndex + 1).ToString());
            decimal b1 = Math.Truncate(objTarget * a1);
            decimal b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5)
            {
                return (b1 + 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }
        /// <summary>
        /// 四舍五入方法.
        /// </summary>
        /// <param name="objTarget">要操作的double类型数字</param>
        /// <param name="mIndex">欲保留小数位数</param>
        /// <returns></returns>
        public static double GetSiSheWuRu(double objTarget, int mIndex)
        {
            double a1 = Math.Pow(10, mIndex);
            double a2 = Math.Pow(10, mIndex + 1);
            double b1 = Math.Truncate(objTarget * a1);
            double b2 = Math.Truncate(objTarget * a2);
            if (b2 % 10 >= 5)
            {
                return (b1 + 1) / a1;
            }
            else
            {
                return b1 / a1;
            }
        }
        /// <summary>
        /// 设置格式方法. 返回格式 "0.000"
        /// </summary>
        /// <param name="objTarget">小数位长度</param>     
        /// <returns></returns>
        public static string GetFormat(int leng)
        {
            string a = "0";
            if (leng > 0)
            {
                a = "0.";
                for (int i = 0; i < leng; i++)
                {
                    a += "0";
                }
            }
            return a;
        }

        /// <summary>
        /// 设置格式方法,返回格式 "0:0.000"
        /// </summary>
        /// <param name="objTarget">小数位长度</param>     
        /// <returns></returns>
        public static string GetFormatA(int leng)
        {
            string a = "{0:0";
            if (leng > 0)
            {
                a = "{0:0.";
                for (int i = 0; i < leng; i++)
                {
                    a += "0";
                }
            }
            return a + "}";
        }




    }
}
