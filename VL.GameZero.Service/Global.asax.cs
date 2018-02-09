using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VL.GameZero.Service.Utilities;

namespace VL.GameZero.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                LogHelper.LogInfo("开始初始化数据库");
                SQLiteHelper.PrepareTables();
                LogHelper.LogInfo("数据库已初始化");
            }
            catch (Exception ex)
            {
                LogHelper.LogInfo("数据库初始化失败");
                LogHelper.LogError(ex);
            }
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
