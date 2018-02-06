using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL_GameZero.DomainModel;

namespace VL_GameZero.DomainModel
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TPlayer entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, entity.UId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TPlayer>(query);
        }
        public static bool DbDelete(this List<TPlayer> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.UId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TPlayer>(query);
        }
        public static bool DbInsert(this TPlayer entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.SlotIndex, entity.SlotIndex));
            if (entity.PlayerName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.PlayerName));
            }
            if (entity.PlayerName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.PlayerName), entity.PlayerName.Length, 20));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.PlayerName, entity.PlayerName));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.CreatedOn, entity.CreatedOn));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TPlayer>(query);
        }
        public static bool DbInsert(this List<TPlayer> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.SlotIndex, entity.SlotIndex));
            if (entity.PlayerName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.PlayerName));
            }
            if (entity.PlayerName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.PlayerName), entity.PlayerName.Length, 20));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.PlayerName, entity.PlayerName));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TPlayerProperties.CreatedOn, entity.CreatedOn));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TPlayer>(query);
        }
        public static bool DbUpdate(this TPlayer entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, entity.UId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.SlotIndex, entity.SlotIndex));
                builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.PlayerName, entity.PlayerName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.CreatedOn, entity.CreatedOn));
            }
            else
            {
                if (fields.Contains(TPlayerProperties.SlotIndex))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.SlotIndex, entity.SlotIndex));
                }
                if (fields.Contains(TPlayerProperties.PlayerName))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.PlayerName, entity.PlayerName));
                }
                if (fields.Contains(TPlayerProperties.CreatedOn))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.CreatedOn, entity.CreatedOn));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TPlayer>(query);
        }
        public static bool DbUpdate(this List<TPlayer> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, entity.UId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.SlotIndex, entity.SlotIndex));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.PlayerName, entity.PlayerName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.CreatedOn, entity.CreatedOn));
                }
                else
                {
                    if (fields.Contains(TPlayerProperties.SlotIndex))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.SlotIndex, entity.SlotIndex));
                    }
                    if (fields.Contains(TPlayerProperties.PlayerName))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.PlayerName, entity.PlayerName));
                    }
                    if (fields.Contains(TPlayerProperties.CreatedOn))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TPlayerProperties.CreatedOn, entity.CreatedOn));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TPlayer>(query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TPlayer DbSelect(this TPlayer entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TPlayer>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TPlayer DbSelect(this TPlayer entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TPlayerProperties.UId);
                builder.ComponentSelect.Add(TPlayerProperties.SlotIndex);
                builder.ComponentSelect.Add(TPlayerProperties.PlayerName);
                builder.ComponentSelect.Add(TPlayerProperties.CreatedOn);
            }
            else
            {
                builder.ComponentSelect.Add(TPlayerProperties.UId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, entity.UId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TPlayer>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 new List<T>()
        /// </summary>
        public static List<TPlayer> DbSelect(this List<TPlayer> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TPlayerProperties.UId);
                builder.ComponentSelect.Add(TPlayerProperties.SlotIndex);
                builder.ComponentSelect.Add(TPlayerProperties.PlayerName);
                builder.ComponentSelect.Add(TPlayerProperties.CreatedOn);
            }
            else
            {
                builder.ComponentSelect.Add(TPlayerProperties.UId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.UId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TPlayerProperties.UId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TPlayer>(query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TPlayer entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.SlotIndex = result.SlotIndex;
                entity.PlayerName = result.PlayerName;
                entity.CreatedOn = result.CreatedOn;
            }
            else
            {
                if (fields.Contains(TPlayerProperties.SlotIndex))
                {
                    entity.SlotIndex = result.SlotIndex;
                }
                if (fields.Contains(TPlayerProperties.PlayerName))
                {
                    entity.PlayerName = result.PlayerName;
                }
                if (fields.Contains(TPlayerProperties.CreatedOn))
                {
                    entity.CreatedOn = result.CreatedOn;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TPlayer> entities, DbSession session, params PDMDbProperty[] fields)
        {
            bool result = true;
            foreach (var entity in entities)
            {
                result = result && entity.DbLoad(session, fields);
            }
            return result;
        }
        #endregion
        #endregion
    }
}
