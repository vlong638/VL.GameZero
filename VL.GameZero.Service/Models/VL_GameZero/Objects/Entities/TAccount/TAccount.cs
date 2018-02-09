using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL_GameZero.DomainModel
{
    [DataContract]
    public partial class TAccount : IPDMTBase
    {
        #region Properties
        /// <summary>
        /// 唯一标识
        /// </summary>
        [DataMember]
        public Guid UId { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        [DataMember]
        public String AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public String Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }
        #endregion

        #region Constructors
        public TAccount()
        {
        }
        public TAccount(Guid uId)
        {
            UId = uId;
        }
        public TAccount(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UId = new Guid(reader[nameof(this.UId)].ToString());
            this.AccountName = Convert.ToString(reader[nameof(this.AccountName)]);
            this.Password = Convert.ToString(reader[nameof(this.Password)]);
            this.CreatedOn = Convert.ToDateTime(reader[nameof(this.CreatedOn)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UId)))
            {
                this.UId = new Guid(reader[nameof(this.UId)].ToString());
            }
            if (fields.Contains(nameof(AccountName)))
            {
                this.AccountName = Convert.ToString(reader[nameof(this.AccountName)]);
            }
            if (fields.Contains(nameof(Password)))
            {
                this.Password = Convert.ToString(reader[nameof(this.Password)]);
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
                return nameof(TAccount);
            }
        }
        #endregion

        #region Manual
        #endregion
    }
}
