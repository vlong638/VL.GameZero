using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.GameZero.Service.Utilities;
using VL_GameZero.DomainModel;

namespace VL.GameZero.Service.Controllers
{
    public class AccountController : ApiController
    {
        public HttpResponseMessage CreateAccount(TAccount account)
        {
            return TransactionHelper.HandleTransactionEvent((session) =>
            {
                if (IsExistence(session, account))
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "账号已创建");
                account.UId = Guid.NewGuid();
                account.CreatedOn = DateTime.Now;
                if (account.DbInsert(session))
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, StringHelper.ErrorMessageForManager);
            });
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