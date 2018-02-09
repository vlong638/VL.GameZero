using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VL.Common.Logger;

namespace VL.GameZero.Service.Utilities
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex)
        {
            //var dllLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //var logger = new TextLogger("VLLogger.txt", dllLocation.Substring(0, dllLocation.LastIndexOf("\\")));
            var dllLocation =@"E:\WorkingSpace\Publishes\VLGameZero";
            var logger = new TextLogger("VLLogger.txt", dllLocation);
            logger.Error(ex.ToString());
        }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="ex"></param>
        public static void LogInfo(string message)
        {
            var dllLocation = @"E:\WorkingSpace\Publishes\VLGameZero";
            var logger = new TextLogger("VLLogger.txt", dllLocation);
            logger.Info(message);
        }
    }
}