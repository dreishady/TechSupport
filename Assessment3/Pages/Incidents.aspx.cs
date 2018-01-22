using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * Author     : Ben Moir | Micheal Thompson | Corey Schmid
 * Date       : 13/11/2017
 * Student ID : 5101965116 | 3100553617 | 0100601817
 * Known Bugs : None
 */

namespace Assessment3
{
    public partial class Incidents : Page
    {

        private List<Account> _customers;
        private List<Account> _technicians;

        /// <summary>
        /// Searches for a customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchForCustomer(object sender, EventArgs e)
        {
            int i;
            if (!int.TryParse(CustomerIDTxtBox.Text, out i)) return;

            Account account;
            Account.Find(Convert.ToInt32(CustomerIDTxtBox.Text), out account);

            if (account == null) return;

            CustomerTextBox1.Text = account.Id.ToString();
            NameTextBox1.Text = account.Name;
            AddressTextBox1.Text = account.Address;

            LocationTextBox1.Text = string.Join(", ", new[] { account.City, account.State, account.ZipCode });

            PhoneTextBox1.Text = account.Phone;
            EmailTextBox1.Text = account.Email;
        }

        /// <summary>
        /// Generates the incidents table
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

            foreach (var products in Product.ProductList)
            {
                ProductDropDown.Items.Add(products.Code);
            }

            _customers = Account.AccountList.Where(a => a.Role == AccountRole.Customer).ToList();
            _technicians = Account.AccountList.Where(a => a.Role != AccountRole.Customer).ToList();

            CustomerDropDown.DataSource = _customers;
            CustomerDropDown.DataBind();

            TechDropDown.DataSource = _technicians;
            TechDropDown.DataBind();

            Update();

            foreach (var incident in Incident.IncidentList)
            {
                var editButton = new Button { Text = "Edit", CssClass = "btn btn-primary EditButton" };
                var deleteButton = new Button { Text = "Delete", CssClass = "btn btn-primary" };

                editButton.Command += ButtonCommand;
                editButton.CommandName = "Edit";
                editButton.CommandArgument = incident.Id.ToString();

                deleteButton.Command += ButtonCommand;
                deleteButton.CommandName = "Delete";
                deleteButton.CommandArgument = incident.Id.ToString();

                var row = new TableRow
                {
                    Cells =
                    {
                        new TableCell { Text = incident.Id.ToString() },
                        new TableCell { Text = incident.CustomerID.ToString() },
                        new TableCell { Text = incident.ProductCode },
                        new TableCell { Text = incident.TechID.ToString() },
                        new TableCell { Text = incident.DateOpened.ToString() },
                        new TableCell { Text = incident.DateClosed.ToString() },
                        new TableCell { Text = incident.Title },
                        new TableCell { Text = incident.Description },
                    }
                };

                switch(Global.CurrentAccount.Role)
                {
                    case AccountRole.TechnicianLevel1:
                    case AccountRole.TechnicianLevel2:
                    case AccountRole.Administrator:
                        row.Cells.Add(new TableCell
                        {
                            Width = Unit.Pixel(140),
                            Controls = { editButton, deleteButton }
                        });
                        break;
                }
                IncidentsTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Toggles the add incident menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClickEvent(object sender, EventArgs e)
        {
            AddIncidentTable.Visible = !AddIncidentTable.Visible;
            Update();
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
                        Global.EditIncidentId = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("~/Pages/EditIncident");
                    }
                    break;
                case "Delete":
                    {
                        using (var command = new SqlCommand(connection: Database.Connection,
                            cmdText: "delete from [Incidents] where IncidentID = @Id"))
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
        /// Adds an incident
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddIncident(object sender, EventArgs e)
        {
            var incident = Incident.Add(
                customerId: _customers[CustomerDropDown.SelectedIndex].Id,
                productCode: ProductDropDown.SelectedItem.Text);

            incident.TechID = _technicians[TechDropDown.SelectedIndex].Id;
            incident.DateOpened = DateTime.Now;
            incident.Title = TitleTextBox.Text;
            incident.Description = DescriptionTextBox.InnerText;

            Response.Redirect(Request.RawUrl);
        }

        /// <summary>
        /// Updates page data
        /// </summary>
        private void Update()
        {
            switch(Global.CurrentAccount.Role)
            {
                case AccountRole.TechnicianLevel1:
                case AccountRole.TechnicianLevel2:
                case AccountRole.Administrator:
                    AddIncidentButton.Visible = true;
                    CustomerTable.Visible = true;
                    break;

                default:
                    AddIncidentButton.Visible = false;
                    CustomerTable.Visible = false;
                    break;
            }

            AddIncidentButton.Text = AddIncidentTable.Visible ? "Hide" : "Add Incident";         
        }
    }
}
