using System;
using VL.Common.Core.ORM;

namespace VL_GameZero.DomainModel
{
    public class TPlayerProperties
    {
        #region Properties
        public static PDMDbProperty<Int32> UId { get; set; } = new PDMDbProperty<Int32>(nameof(UId), "UId", "独立标识符", true, PDMDataType.numeric, 32, 0, true);
        public static PDMDbProperty<Int16> SlotIndex { get; set; } = new PDMDbProperty<Int16>(nameof(SlotIndex), "SlotIndex", "槽位号", false, PDMDataType.numeric, 2, 0, true);
        public static PDMDbProperty<String> PlayerName { get; set; } = new PDMDbProperty<String>(nameof(PlayerName), "PlayerName", "玩家名称", false, PDMDataType.varchar, 20, 0, true);
        public static PDMDbProperty<DateTime> CreatedOn { get; set; } = new PDMDbProperty<DateTime>(nameof(CreatedOn), "CreatedOn", "创建时间", false, PDMDataType.datetime, 0, 0, true);
        #endregion
    }
}
