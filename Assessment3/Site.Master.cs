/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 * Summary    : Site master page code
 * Notes      : The site master page must use Global.CurrentSession instead of Session
 */

using System;
using System.Web.UI;

namespace Assessment3
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default.OnLoginAttempt -= OnLoginAttempt;
            Default.OnLoginAttempt += OnLoginAttempt;

            Update();
        }

        protected void LogoutClick(object sender, EventArgs e)
        {
            Global.ResetLoginState();
            Response.Redirect(Request.RawUrl);
        }

        private void Update()
        {
            NavbarSettings.Visible = LogoutButton.Visible =
                Global.CurrentSession != null
                ? Global.LoginState
                : false;

            if (Global.LoginState)
            {
                switch (Global.CurrentAccount.Role)
                {
                    case AccountRole.TechnicianLevel1:
                    case AccountRole.TechnicianLevel2:
                    case AccountRole.Administrator:
                        RegisterButton.Visible = Global.LoginState;
                        break;

                    default:
                        RegisterButton.Visible = false;
                        break;
                }
            }
        }

        public void OnLoginAttempt(object sender, EventArgs e)
        {
            Update();
        }
    }
}
