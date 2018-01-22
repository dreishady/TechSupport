/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 */

using System;
using System.Web;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.SessionState;

namespace Assessment3
{
    public class Global : HttpApplication
    {
        public static HttpSessionState CurrentSession { get; private set; }

        public static bool LoginState
        {
            get { return Convert.ToBoolean(CurrentSession["LoginState"]); }
            set { CurrentSession["LoginState"] = value; }
        }

        public static int LoginId
        {
            get { return Convert.ToInt32(CurrentSession["LoginID"]); }
            set { CurrentSession["LoginID"] = value; }
        }

        public static string EditProductCode
        {
            get { return Convert.ToString(CurrentSession["EditProductCode"]); }
            set { CurrentSession["EditProductCode"] = value; }
        }

        public static int EditCustomerId
        {
            get { return Convert.ToInt32(CurrentSession["EditCustomerId"]); }
            set { CurrentSession["EditCustomerId"] = value; }
        }

        public static int EditIncidentId
        {
            get { return Convert.ToInt32(CurrentSession["EditIncidentId"]); }
            set { CurrentSession["EditIncidentId"] = value; }
        }

        public static Account CurrentAccount
        {
            get
            {
                Account account;
                Account.Find(LoginId, out account);
                return account;
            }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        void Session_Start(object sender, EventArgs e)
        {
            CurrentSession = Session;
            LoginState = false;
            LoginId = -1;
        }

        void Session_End(object sender, EventArgs e)
        {
            CurrentSession = null;
        }

        public static void ResetLoginState()
        {
            LoginState = false;
            LoginId = -1;
        }
    }
}
