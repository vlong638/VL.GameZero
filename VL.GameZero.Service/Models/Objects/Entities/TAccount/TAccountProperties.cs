using System;
using VL.Common.Core.ORM;

namespace VL_GameZero.DomainModel
{
    public class TAccountProperties
    {
        #region Properties
        public static PDMDbProperty<Int32> UId { get; set; } = new PDMDbProperty<Int32>(nameof(UId), "UId", "唯一标识", true, PDMDataType.numeric, 32, 0, true);
        public static PDMDbProperty<String> AccountName { get; set; } = new PDMDbProperty<String>(nameof(AccountName), "AccountName", "账户名称", false, PDMDataType.varchar, 20, 0, true);
        public static PDMDbProperty<String> Password { get; set; } = new PDMDbProperty<String>(nameof(Password), "Password", "密码", false, PDMDataType.varchar, 128, 0, true);
        public static PDMDbProperty<DateTime> CreatedOn { get; set; } = new PDMDbProperty<DateTime>(nameof(CreatedOn), "CreatedOn", "创建时间", false, PDMDataType.datetime, 0, 0, true);
        #endregion
    }
}
