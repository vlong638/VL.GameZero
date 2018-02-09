using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL_GameZero.DomainModel
{
    [DataContract]
    public partial class TPlayer : IPDMTBase
    {
        #region Properties
        /// <summary>
        /// 独立标识符
        /// </summary>
        [DataMember]
        public Guid UId { get; set; }
        /// <summary>
        /// 槽位号
        /// </summary>
        [DataMember]
        public Int16 SlotIndex { get; set; }
        /// <summary>
        /// 玩家名称
        /// </summary>
        [DataMember]
        public String PlayerName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }
        #endregion

        #region Constructors
        public TPlayer()
        {
        }
        public TPlayer(Guid uId)
        {
            UId = uId;
        }
        public TPlayer(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UId = new Guid(reader[nameof(this.UId)].ToString());
            this.SlotIndex = Convert.ToInt16(reader[nameof(this.SlotIndex)]);
            this.PlayerName = Convert.ToString(reader[nameof(this.PlayerName)]);
            this.CreatedOn = Convert.ToDateTime(reader[nameof(this.CreatedOn)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UId)))
            {
                this.UId = new Guid(reader[nameof(this.UId)].ToString());
            }
            if (fields.Contains(nameof(SlotIndex)))
            {
                this.SlotIndex = Convert.ToInt16(reader[nameof(this.SlotIndex)]);
            }
            if (fields.Contains(nameof(PlayerName)))
            {
                this.PlayerName = Convert.ToString(reader[nameof(this.PlayerName)]);
            }
            if (fields.Contains(nameof(CreatedOn)))
            {
                this.CreatedOn = Convert.ToDateTime(reader[nameof(this.CreatedOn)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TPlayer);
            }
        }
        #endregion

        #region Manual
        #endregion
    }
}