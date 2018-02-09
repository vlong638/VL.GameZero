using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.GameZero.Service.Utilities;

namespace Test.Helper.CompositeTemplate
{
    public class NavigatorItem
        : HelperBase
    {
        public NavigatorItem()
        {
            DoorPlate = "";
        }

        public NavigatorItem(HelperBase parent, string description = "未添加功能描述", string doorPlate = "")
            : base(parent, description)
        {
            DoorPlate = doorPlate;
        }

        public override void  Execute()
        {
            ShowMenuWithResult();
        }

        public string DoorPlate { set; get; }
        public string DisplayContents(List<string> contents)
        {
            StringBuilder sb = new StringBuilder();
            int boardLength = 80;//640 83
            string lineBeam = "".PadToRight(boardLength, "*");
            string lineColumn = "*".PadToRight(boardLength - 1, " ") + "*";
            sb.Append(lineBeam);
            for (int i = 0; i < 20; i++)
            {
                if (i < contents.Count)
                    sb.Append(contents[i]);
                else
                    sb.Append(lineColumn);
            }
            sb.Append(lineBeam);
            return sb.ToString();
        }

        public void ShowMenuWithResult()
        {
            ShowMenu();
            string input;
            while (!string.Equals(input = Console.ReadLine().ToLower(), "b"))
            {
                if (Parent == null && string.Equals(input, "q"))
                {
                    //退出程序
                    break;
                }

                int index = -1;
                if (int.TryParse(input, out index))
                {
                    HelperBase son = SonList[index];
                    if (son != null)
                    {
                        son.Execute();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                ShowMenu();
            }
        }
        protected void ShowMenu()
        {
            Console.WriteLine(DisplayContents(SonList.Select(c => c.MenuStr).ToList()));






            //Console.ForegroundColor = ConsoleColor.Yellow;
            //if (SonList.Count() > 0)
            //{
            //    Console.WriteLine("请选择功能:");
            //    SonList.ForEach((c) => { Console.WriteLine(c.MenuStr); });
            //}
            //else
            //{
            //    Console.WriteLine("该节点无下属功能");
            //}
            //if (Parent != null)
            //{
            //    Console.WriteLine("输入b返回上级");
            //}
            //else
            //{
            //    Console.WriteLine("输入q退出程序");
            //}
            Console.WriteLine();
        }
    }
}
