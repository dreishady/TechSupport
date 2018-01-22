/*
 * Author     : Ben Moir | Micheal Thompson | Andrei Rico
 * Date       : 13/11/2017
 * Student ID : 5101965116 | 3100553617 | 3106107616
 * Known Bugs : None
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assessment3
{
    public partial class Contact : Page
    {
        /// <summary>
        /// Constructs the account table for technicians
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

            Update();

            foreach (var account in Account.AccountList)
            {
                var editButton = new Button { Text = "Edit", CssClass = "btn btn-primary EditButton" };
                var deleteButton = new Button { Text = "Delete", CssClass = "btn btn-primary" };

                int level;
                switch (account.Role)
                {
                    case AccountRole.TechnicianLevel1:
                        level = 1;
                        break;
                    case AccountRole.TechnicianLevel2:
                        level = 2;
                        break;
                    case AccountRole.Administrator:
                        level = 3;
                        break;
                    default:
                        continue;
                }

                editButton.Command += ButtonCommand;
                editButton.CommandName = "Edit";
                editButton.CommandArgument = account.Id.ToString();

                deleteButton.Command += ButtonCommand;
                deleteButton.CommandName = "Delete";
                deleteButton.CommandArgument = account.Id.ToString();

                var row = new TableRow
                {
                    Cells =
                    {
                        new TableCell { Text = account.Id.ToString() },
                        new TableCell { Text = account.Name },
                        new TableCell { Text = level.ToString() },
                        new TableCell { Text = account.Phone },
                        new TableCell { Text = account.Email },
                    }
                };

                if (Global.CurrentAccount.Role == AccountRole.Administrator)
                {
                    row.Cells.Add(new TableCell
                    {
                        Width = Unit.Pixel(140),
                        Controls = { editButton, deleteButton }
                    });
                }

                TechnicianTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Handles edit and delete buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCommand(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    {
                        Global.EditCustomerId = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("~/Pages/EditTechnician.aspx");
                    }
                    break;
                case "Delete":
                    {
                        using (var command = new SqlCommand(connection: Database.Connection,
                            cmdText: "delete from [Customers] where CustomerID = @Id"))
                        {
                            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Convert.ToInt32(e.CommandArgument) });
                            command.ExecuteNonQuery();
                        }

                        Response.Redirect(Request.RawUrl);
                    }
                    break;
            }
        }

        /// <summary>
        /// Toggles the add technician menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClickEvent(object sender, EventArgs e)
        {
            AddTechnicianTable.Visible = !AddTechnicianTable.Visible;
            Update();
        }

        /// <summary>
        /// Adds a technician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTechnician(object sender, EventArgs e)
        {
            var level = Convert.ToInt32(TechLevelTextBox.Text);

            if (level != 1 && level != 2 && level != 3) return;

            var account = Account.Add();
            account.Name = TechNameTextBox.Text;
            account.Phone = TechPhoneTextBox.Text;
            account.Email = TechEmailTextBox.Text;

            switch (level)
            {
                case 1: account.Role = AccountRole.TechnicianLevel1; break;
                case 2: account.Role = AccountRole.TechnicianLevel2; break;
                case 3: account.Role = AccountRole.Administrator; break;
            }

            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Updates page data
        /// </summary>
        private void Update()
        {
            switch (Global.CurrentAccount.Role)
            {
                case AccountRole.Administrator:
                    AddTechnicianButton.Visible = true;
                    break;

                default:
                    AddTechnicianButton.Visible = false;
                    break;
            }
            AddTechnicianButton.Text = AddTechnicianTable.Visible ? "Hide" : "Add New Technician";
        }
    }
}
