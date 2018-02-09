using System;
using System.Net;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.GameZero.Service.Utilities;
using VL_GameZero.DomainModel;

namespace VL.GameZero.ClientConsole.Utilities
{
    /// <summary>
    /// 游戏主界面
    /// </summary>
    public class SQLiteCMDs : NavigatorItem
    {
        public SQLiteCMDs(HelperBase parent, string description = "SQLite 指令工具", string doorPlate = "") : base(parent, description, doorPlate)
        {
            SonList.Add(new FunctionItem(this, () =>
            {
                Console.WriteLine("开始初始化数据库");
                SQLiteHelper.PrepareTables();
                Console.WriteLine("数据库已初始化");
            }, "PrepareTables"));
            SonList.Add(new FunctionItem(this, () =>
            {
                TAccount account = new TAccount() { AccountName = "vlong638", Password = "701616" };
                var result = TransactionHelper.HandleTransactionEvent((session) =>
                {
                    if (IsExistence(session, account))
                    {
                        Console.WriteLine("用户已存在");
                        return false;
                    }
                    account.UId = Guid.NewGuid();
                    account.CreatedOn = DateTime.Now;
                    return account.DbInsert(session);
                });
                Console.WriteLine(result ? "执行成功" : "执行失败");
            }, "CreateAccount"));
        }
        private static bool IsExistence(DbSession session, TAccount account)
        {
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add(new ComponentValueOfSelect("1"));
            query.ComponentWhere.Add(new ComponentValueOfWhere(TAccountProperties.AccountName, account.AccountName, LocateType.Equal));
            var result = session.GetQueryOperator().SelectAsInt<TAccount>(query);
            return result.HasValue && result.Value == 1;
        }
    }
}