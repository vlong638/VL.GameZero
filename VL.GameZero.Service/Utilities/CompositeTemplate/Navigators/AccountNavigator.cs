using System;
using Test.Helper.CompositeTemplate;

namespace VL.GameZero.Service.Utilities.CompositeTemplate.Navigators
{
    /// <summary>
    /// 游戏主界面
    /// </summary>
    public class AccountNavigator : NavigatorItem
    {
        public AccountNavigator() : base()
        {
            SonList.Add(new FunctionItem(this, () =>
            {
                Console.WriteLine("用户登录-已被执行");
            }, "用户登录"));
            SonList.Add(new FunctionItem(this, () =>
            {
                Console.WriteLine("注册账户-已被执行");
            }, "注册账户"));
        }

        private static bool GetInput(string messageInfo, out string input, params Func<string, bool>[] checks)
        {
            Console.WriteLine(messageInfo);
            input = Console.ReadLine();
            foreach (var check in checks)
            {
                if (!check(input))
                    return false;
            }
            return true;
        }
    }
}