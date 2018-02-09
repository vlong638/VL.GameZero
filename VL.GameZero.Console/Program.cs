using System;
using VL.GameZero.ClientConsole.Utilities;
using VL_GameZero.DomainModel;

namespace VL.GameZero.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new HomeNavigator(null).ShowMenuWithResult();




            //Run();
            //RunForTest();
            Console.ReadLine();
        }

        static async void Run()
        {
            int endpoint = 8003;
            Console.WriteLine($"添加结果：{await HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetProduct/?id=1")}"); 
            Console.WriteLine($"添加结果：{await HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetAllProducts")}");
            string postString = string.Format("AccountName={0}&&Password={1}", "vlong638", "701616");
            Console.WriteLine($"添加结果：{await HTTPHelper.POST($@"http://localhost:{endpoint}/api/Account/CreateAccount", postString)}");
        }
        static async void RunForTest()
        {
            int endpoint = 51840;
            Console.WriteLine($"添加结果：{await HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetProduct/?id=1")}");
            Console.WriteLine($"添加结果：{await HTTPHelper.GET($@"http://localhost:{endpoint}/api/products/GetAllProducts")}");
            Console.WriteLine($"添加结果：{await HTTPHelper.POST($@"http://localhost:{endpoint}/api/Account/CreateAccount", Newtonsoft.Json.JsonConvert.SerializeObject(new TAccount() { AccountName = "vlong638", Password = "701616" }))}");
        }
    }

}
