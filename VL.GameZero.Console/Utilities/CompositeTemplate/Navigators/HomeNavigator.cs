using System;

namespace VL.GameZero.ClientConsole.Utilities
{
    /// <summary>
    /// 游戏主界面
    /// </summary>
    public class HomeNavigator : NavigatorItem
    {
        public HomeNavigator(HelperBase parent, string description = "主页", string doorPlate = "") : base(parent, description, doorPlate)
        {
            SonList.Add(new FunctionItem(this, () =>
            {
                Console.WriteLine("用户登录-已被执行");
            }, "用户登录"));
            SonList.Add(new FunctionItem(this, () =>
            {
                Console.WriteLine("注册账户-已被执行");
            }, "注册账户"));
            SonList.Add(new AccountCMDs(this));
            SonList.Add(new SQLiteCMDs(this));
        }
    }
}
