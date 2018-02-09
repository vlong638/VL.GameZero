using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VL.Common.Core.DAS;
using VL.Common.Logger;

namespace VL.GameZero.Service.Utilities
{
    public class StateDbSession
    {
        public StateDbSession(bool isIdling, DbSession dbSession)
        {
            IsIdling = isIdling;
            DbSession = dbSession;
        }

        public bool IsIdling { set; get; }
        public DbSession DbSession { set; get; }
    }
    

    public class TransactionHelper
    {
        static List<StateDbSession> StateDbSessions { set; get; }
        static StateDbSession GetDbSession()
        {
            if (StateDbSessions == null || StateDbSessions.Count == 0)
            {
                StateDbSessions = new List<StateDbSession>();
                StateDbSessions.Add(new StateDbSession(true, new DbSession(EDatabaseType.SQLite, SQLiteHelper.GetConnectingString())));
            }
            var StateDbSession =StateDbSessions.First(c => c.IsIdling);
            StateDbSession.IsIdling = false;
            return StateDbSession;
        }
        public static HttpResponseMessage HandleTransactionEvent(ApiController api, Func<DbSession, HttpResponseMessage> fuction)
        {
            var dbSession = GetDbSession();
            var session1 = dbSession.DbSession;
            HttpResponseMessage result = null;
            bool hasError = false;
            try
            {
                session1.Open();
                session1.BeginTransaction();
                try
                {
                    result = fuction(session1);
                    session1.CommitTransaction();
                }
                catch (Exception ex)
                {
                    hasError = true;
                    session1.RollBackTransaction();
                    LogHelper.LogError(ex);
                }
                session1.Close();
            }
            catch (Exception ex)
            {
                hasError = true;
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                    session1.Close();
                LogHelper.LogError(ex);
            }
            dbSession.IsIdling = true;
            if (hasError)
                result = api.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "");
            return result;
        }
        public static T HandleTransactionEvent<T>(Func<DbSession,T> fuction)
        {
            var dbSession = GetDbSession();
            var session1 = dbSession.DbSession;
            T result = default(T);
            try
            {
                session1.Open();
                session1.BeginTransaction();
                try
                {
                    result= fuction(session1);
                    session1.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session1.RollBackTransaction();
                    LogHelper.LogError(ex);
                }
                session1.Close();
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                    session1.Close();
                LogHelper.LogError(ex);
            }
            dbSession.IsIdling = true;
            return result;
        }



        public static void HandleTransactionEvent(Action<DbSession> action)
        {
            var dbSession = GetDbSession();
            var session1 = dbSession.DbSession;
            try
            {
                session1.Open();
                session1.BeginTransaction();
                try
                {
                    action(session1);
                    session1.CommitTransaction();
                }
                catch (Exception ex)
                {
                    session1.RollBackTransaction();
                    LogHelper.LogError(ex);
                }
                session1.Close();
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                    session1.Close();
                LogHelper.LogError(ex);
            }
            dbSession.IsIdling = true;
        }

        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public static void HandleTransactionEvent(string sessionString, Action<DbSession> action, Action<Exception> onError = null)
        {
            using (var session1 = new DbSession(EDatabaseType.SQLite, sessionString))
            {
                session1.IsLogQuery = true;
                //业务逻辑处理
                try
                {
                    session1.Open();
                    session1.BeginTransaction();
                    try
                    {
                        action(session1);
                        session1.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session1.RollBackTransaction();
                        if (onError != null)
                        {
                            onError(ex);

                            var dllLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            var logger = new TextLogger("VLLogger.txt", dllLocation.Substring(0, dllLocation.LastIndexOf("\\")));
                            logger.Error(ex.ToString());
                        }
                    }
                    session1.Close();
                }
                catch (Exception ex)
                {
                    if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                        session1.Close();
                    if (onError != null)
                    {
                        onError(ex);

                        var dllLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                        var logger = new TextLogger("VLLogger.txt", dllLocation.Substring(0, dllLocation.LastIndexOf("\\")));
                        logger.Error(ex.ToString());
                    }
                }
            }
        }
    }
}