using System;
using  VL.GameZero.ClientConsole.Utilities;

namespace VL.GameZero.ClientConsole.Utilities
{
    /// <summary>
    /// 主菜单
    /// </summary>
    public class MainNavigator : NavigatorItem
    {
        //public MainNavigator() : base()
        //{
        //    SonList.Add(new FunctionItem(this, () =>
        //    {
        //        string account;
        //        string password;
        //        bool isSuccess = GetInput("请输入用户名", out account, (input) =>
        //         {
        //             if (string.IsNullOrEmpty(input))
        //             {
        //                 Console.WriteLine("用户名不可为空");
        //                 return false;
        //             }
        //             return true;
        //         })
        //        && GetInput("请输入密码", out password, (input) =>
        //        {
        //            if (string.IsNullOrEmpty(input))
        //            {
        //                Console.WriteLine("密码不可为空");
        //                return false;
        //            }
        //            return true;
        //        })
        //        && CommonService.CreateAccount(new TAccount() { AccountName = account, Password = password }).IsSuccess;
        //        if (!isSuccess)
        //            return;
        //        //new HomeNavigator()

        //    }, "用户登录"));
        //    SonList.Add(new FunctionItem(this, () =>
        //    {
        //    }, "注册账户"));
        //}

        public MainNavigator(HelperBase parent, string description = "未添加功能描述", string doorPlate = "") : base(parent, description, doorPlate)
        {
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
