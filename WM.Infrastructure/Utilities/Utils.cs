using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WM.Infrastructure.Utilities
{
    public class Utils
    {
        #region 生成指定位数的验证码
        /// <summary>
        /// 生成指定位数的验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string MakeCode(int length)
        {
            string code = string.Empty;
            int[] randNum = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                code += randNum[rand.Next(randNum.Length)];
            }

            return code;
        }
        #endregion

        #region 时间戳

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式,返回格式：1468482273277
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeInt(DateTime time)
        {
            //double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            //intResult = (time- startTime).TotalMilliseconds;
            long t = (time.Ticks - startTime.Ticks) / 10000;            //除10000调整为13位
            return t;
        }

        /// <summary>
        /// 生成Unix时间戳
        /// </summary>
        public static string CreateUnix()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            string timeSpan = (DateTime.Now - startTime).TotalSeconds.ToString();

            timeSpan = timeSpan.Split('.')[0];

            return timeSpan;
        }

        #endregion

 
        /// <summary>
        /// 生成x位随机数
        /// </summary>
        /// <param name="iLength">长度</param>
        /// <returns></returns>
        public static string GetRandomString(int iLength)
        {
           string buffer = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();

            Random r = new Random(Guid.NewGuid().GetHashCode());

            int range = buffer.Length;

            for (int i = 0; i < iLength; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取MT标签
        /// </summary>
        /// <param name="mtComment">原标签</param>
        /// <param name="billNo">订单号</param>
        /// <returns></returns>
        public static string GetMTComment(string mtComment, string billNo)
        {
            return mtComment + "#" + billNo;
        }
        /// <summary>
        /// 生成随机密码
        /// </summary>
        /// <param name="charsLen">字母长度</param>
        /// <param name="numlen">数字长度</param>
        /// <returns></returns>
        public static string GetRandomPassword(int charsLen, int numlen)
        {
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ
            string chars = "abcdefghijkmnopqrstuvwxyz";
            string nums = "1234567890";
            string password = string.Empty;
            int randomNum;
            Random random = new Random();
            for (int i = 0; i < charsLen; i++)
            {
                randomNum = random.Next(chars.Length);
                password += chars[randomNum];
            }
            for (int i = 0; i < numlen; i++)
            {
                randomNum = random.Next(nums.Length);
                password = password.Insert(random.Next(password.Length), nums[randomNum].ToString());
            }
            return password;
        }
        ///<summary>
        ///计算时间差
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns>返回(秒)单位，</returns>
        public static double ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            return ts3.TotalSeconds;
        }
        /// <summary>
        /// 随机生成电话号码
        /// </summary>
        /// <returns></returns>
        public static string GetRandomTel(string regex="")
        {
            
            string[] telStarts = "134,135,136,137,138,139,150,151,152,157,158,159,130,131,132,155,156,133,153,180,181,182,183,185,186,176,187,188,189,177,178".Split(',');
            var ran = new Random();
            int n = ran.Next(10, 1000);
            int index = ran.Next(0, telStarts.Length - 1);
            string first = telStarts[index];
            string second = (ran.Next(100, 888) + 10000).ToString().Substring(1);
            string thrid = (ran.Next(1, 9100) + 10000).ToString().Substring(1);
            var tel = first + second + thrid;
            if (!string.IsNullOrWhiteSpace(regex))
            {
                try
                {
                    tel = Regex.Replace(tel, "(\\d{3})\\d{4}(\\d{4})", regex);
                }
                catch (Exception){
                    tel = first + second + thrid;
                }
            }
            return tel;
        }
        /// <summary>
        /// 手机号隐藏
        /// </summary>
        /// <returns></returns>
        public static string GetMobileNumberHide(string mobile, string regex = "")
        {
            var tel = string.Empty;
            try
            {
                tel = Regex.Replace(mobile, "(\\d{3})\\d{4}(\\d{4})", regex);
            }
            catch (Exception)
            {
                tel = mobile;
            }
            return tel;
        }
        ///// <summary>
        ///// 手机号隐藏 传pattern
        ///// </summary>
        ///// <returns></returns>
        public static string GetMobileNumberHide(string mobile, string pattern, string regex = "")
        {
            var tel = string.Empty;
            try
            {
                tel = Regex.Replace(mobile, pattern, regex);
            }
            catch (Exception)
            {
                tel = mobile;
            }
            return tel;
        }
        public static bool Deletefile(string filePath)
        {

            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch (System.IO.IOException e)
                {
                    //   Console.WriteLine(e.Message);
                }
            }
            return false;
        }
        /// <summary>
        /// 生成6位邀请码
        /// </summary>
        /// <returns></returns>
        public static string GetInvitationCode()
        {
            //自定义进制，长度为34。  0和1与o和l容易混淆，不包含在进制中。
            char[] r = new char[] { 'Q', 'w', 'E', '8', 'a', 'S', '2', 'd', 'Z', 'x', '9', 'c', '7', 'p', 'O', '5', 'i', 'K', '3', 'm', 'j', 'U', 'f', 'r', '4', 'V', 'y', 'L', 't', 'N', '6', 'b', 'g', 'H' };
            char[] b = new char[] { 'q', 'W', 'e', '5', 'A', 's', '3', 'D', 'z', 'X', '8', 'C', '2', 'P', 'o', '4', 'I', 'k', '9', 'M', 'J', 'u', 'F', 'R', '6', 'v', 'Y', 'T', 'n', '7', 'B', 'G', 'h' };
            char[] buf = new char[33];
            int s = 6;//生成六位的邀请码
            int binLen = r.Length;
            int charPos = 33;
            //以当前的毫秒数作为标准
            int id = DateTime.Now.Millisecond;
            while (id / binLen > 0)
            {
                int k = (int)(id % binLen);
                buf[--charPos] = r[k];
                id /= binLen;
            }
            buf[--charPos] = r[(int)(id % binLen)];
            String str = new String(buf, charPos, (33 - charPos));
            //长度不够6位时自动随机补全
            if (str.Length < s)
            {
                StringBuilder sb = new StringBuilder();
                Random rd = new Random();
                for (int i = 1; i <= s - str.Length; i++)
                {
                    sb.Append(b[rd.Next(33)]);
                }
                str += sb.ToString();
            }
            return str.ToUpper();
        }
        // 随机抽取数组中的数据
        public static T GetRandomNumber<T>(T[] a)
        {
            Random rnd = new Random();
            int index = rnd.Next(a.Length);
            return a[index];
        }
        /// <summary>
        /// 获取周一
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonday(DateTime date)
        {
            DateTime temp = new DateTime(date.Year, date.Month, date.Day);
            int count = date.DayOfWeek - DayOfWeek.Monday;
            if (count == -1) count = 6;

            return temp.AddDays(-count);
        }
    }
}
