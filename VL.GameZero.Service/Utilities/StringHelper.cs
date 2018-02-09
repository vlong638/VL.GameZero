using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VL.GameZero.Service.Utilities
{
    public static class StringHelper
    {
        public static string ErrorMessageForManager = "功能出现异常,请联系管理员";

        public static string PadToRight(this string s, int length, string paddingString)
        {
            if (s.Length >= length)
                return s;

            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            var stringLength = System.Text.Encoding.Default.GetBytes(s).Length; ;
            for (int i = 0; i < length - stringLength; i++)
                sb.Append(paddingString);
            s = sb.ToString();
            return sb.ToString();
        }

        public static List<string> Combines(this IEnumerable<string> content1s, IEnumerable<string> content2s)
        {
            List<string> result = new List<string>();
            result.AddRange(content1s);
            result.AddRange(content2s);
            return result;
        }
    }
}