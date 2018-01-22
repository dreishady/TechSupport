using System;
using System.Web.UI;

namespace Assessment3.Pages
{
    /*
        Author     : Ben Moir | Waleed
        Student ID : 5101965116 | 6100758617
        Date       : 13/11/2017
        Known Bugs : None
    */

    public partial class EditTechnician : Page
    {
        /// <summary>
        /// Fills in data fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Global.LoginState)
            {
                Response.Redirect("~/");
                return;
            }

            LevelDropDown.DataSource = new[] { "Level 1", "Level 2", "Administrator" };
            LevelDropDown.DataBind();

            if (IsPostBack) return;

            Account account;
            Account.Find(Global.EditCustomerId, out account);

            switch (account.Role)
            {
                case AccountRole.Administrator:
                    LevelDropDown.SelectedIndex = 2;
                    break;
                case AccountRole.TechnicianLevel2:
                    LevelDropDown.SelectedIndex = 1;
                    break;
                case AccountRole.TechnicianLevel1:
                    LevelDropDown.SelectedIndex = 0;
                    break;
            }

            NameTextBox.Text = account.Name;
            PhoneTextBox.Text = account.Phone;
            EmailTextBox.Text = account.Email;
            PasswordTextBox.Text = account.Password;
        }

        /// <summary>
        /// Applies changes to the technician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplyChanges(object sender, EventArgs e)
        {
            Account account;
            Account.Find(Global.EditCustomerId, out account);

            account.Name = NameTextBox.Text;
            account.Phone = PhoneTextBox.Text;
            account.Email = EmailTextBox.Text;
            account.Password = PasswordTextBox.Text;

            switch (LevelDropDown.SelectedIndex)
            {
                case 0: account.Role = AccountRole.TechnicianLevel1; break;
                case 1: account.Role = AccountRole.TechnicianLevel2; break;
                case 2: account.Role = AccountRole.Administrator; break;
            }

            Response.Redirect("~/Pages/Technician");
        }

        /// <summary>
        /// Returns to the technicians page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Technician");
        }
    }
}
