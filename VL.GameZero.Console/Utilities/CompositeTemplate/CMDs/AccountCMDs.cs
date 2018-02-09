using System;
using VL_GameZero.DomainModel;

namespace VL.GameZero.ClientConsole.Utilities
{
    /// <summary>
    /// 游戏主界面
    /// </summary>
    public class AccountCMDs : NavigatorItem
    {
        public AccountCMDs(HelperBase parent, string description = "Account的指令工具", string doorPlate = "") : base(parent, description, doorPlate)
        {
            int endpoint = 8003;
            //int endpoint = 51840;
            SonList.Add(new FunctionItem(this, () =>
            {
                var result = HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetProduct/?id=1");
                result.Wait();
                Console.WriteLine($"添加结果：{result.Result}");
                Console.ReadLine();
            }, "GetProduct"));
            SonList.Add(new FunctionItem(this, () =>
            {
                var result = HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetAllProducts");
                result.Wait();
                Console.WriteLine($"添加结果：{result.Result}");
                Console.ReadLine();
            }, "GetAllProducts"));
            SonList.Add(new FunctionItem(this, () =>
            {
                var data = Newtonsoft.Json.JsonConvert.SerializeObject(new TAccount() { AccountName = "vlong638", Password = "701616" });
                var result = HTTPHelper.POST($@"http://localhost:{endpoint}/api/Account/CreateAccount", data);
                result.Wait();
                Console.WriteLine($"添加结果：{result.Result}");
                Console.ReadLine();
            }, "CreateAccount"));
        }
    }
}