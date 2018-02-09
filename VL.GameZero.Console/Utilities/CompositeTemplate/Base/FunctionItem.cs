using System;

namespace VL.GameZero.ClientConsole.Utilities
{
    public class FunctionItem : HelperBase
    {
        Action MyAction { set; get; }

        public FunctionItem(HelperBase parent, Action action, string description = "未添加功能描述")
            : base(parent, description)
        {
            MyAction = action;
            IsExecutable = true;
        }

        public override void Execute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                MyAction();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
