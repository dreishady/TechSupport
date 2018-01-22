/*
 * Author     : Ben Moir
 * Date       : 10/11/2017
 * Student ID : 5101965116
 * Known Bugs : None
 * Summary    : Default page code
 */

using System;
using System.Web.UI;

namespace Assessment3
{
    public partial class Default : Page
    {
        public static EventHandler OnLoginAttempt;

        private string RoleToString(AccountRole role)
        {
            switch (role)
            {
                default:
                    return role.ToString();
                case AccountRole.TechnicianLevel1:
                    return "Level 1 Technician";
                case AccountRole.TechnicianLevel2:
                    return "Level 2 Technician";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Update();
        }

        protected void LoginProcess(object sender, EventArgs e)
        {
            try
            {
                if (Global.LoginState)
                {
                    Global.ResetLoginState();
                    return;
                }

                int.TryParse(CustomerField.Text, out var id);
                if (!Account.Find(id, out var account))
                {
                    Global.ResetLoginState();
                    // show message: unknown account id
                    return;
                }

                if (account.Password != PasswordField.Text)
                {
                    Global.ResetLoginState();
                    // show message: wrong password
                    return;
                }

                Global.LoginId = id;
                Global.LoginState = true;
            }
            finally
            {
                Update();
                OnLoginAttempt(this, null);
            }
        }

        private void Update()
        {
            const string loginHtml = "Login <span class=\"glyphicon glyphicon-arrow-right\"/>";
            const string logoutHtml = "Logout <span class=\"glyphicon glyphicon-arrow-left\"/>";

            LoginButton.InnerHtml = Global.LoginState ? logoutHtml : loginHtml;

            LoginScreen.Visible = !Global.LoginState;
            LoginScreenOne.Visible = !Global.LoginState;
            TestDiv.Visible = Global.LoginState;
            if (Global.LoginState)
            {
                WelcomeText.InnerText = $"Welcome, {Global.CurrentAccount.Name}";
                RoleText.InnerText = $"Your role is: {RoleToString(Global.CurrentAccount.Role)}";
            }
        }
    }
}
