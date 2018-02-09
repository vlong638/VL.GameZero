using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VL.GameZero.Service.Utilities;

namespace VL.GameZero.ClientConsole.Utilities
{
    public class NavigatorItem
        : HelperBase
    {
        public NavigatorItem(HelperBase parent, string description = "未添加功能描述", string doorPlate = "")
            : base(parent, description)
        {
            DoorPlate = doorPlate;
            if (parent != null)
                ExtraCommands.Add(" b:返回上层");
            else

                ExtraCommands.Add(" q:退出程序");
        }

        public override void Execute()
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
                    sb.Append(GetFormatLine(contents[i]));
                else
                    sb.Append(lineColumn);
            }
            sb.Append(lineBeam);
            return sb.ToString();
        }

        List<string> ExtraCommands = new List<string>();
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
                    HelperBase son = SonList[index - 1];
                    if (son != null)
                    {
                        son.Execute();
                        //System.Threading.Thread.Sleep(1000);
                    }
                }
                ShowMenu();
            }
        }
        protected void ShowMenu()
        {
            Console.WriteLine(DisplayContents(ExtraCommands.Combines(SonList.Select(c => " " + c.Index + ":" + c.Description))));
            Console.WriteLine();
        }
    }
}
