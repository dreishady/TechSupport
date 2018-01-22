using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * Author     : Ben Moir | Micheal Thompson
 * Date       : 13/11/2017
 * Student ID : 5101965116 | 3100553617
 * Known Bugs : None
 */

namespace Assessment3.Pages
{
    public partial class Registration : Page
    {
        private List<Account> _customers;
        private List<Product> _products;

        /// <summary>
        /// Fills in dropdowns
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

            foreach(var register in Assessment3.Registration.RegistrationList)
            {
                var deleteButton = new Button { Text = "Delete", CssClass = "btn btn-primary" };

                deleteButton.Command += ButtonCommand;
                deleteButton.CommandName = "Delete";
                deleteButton.CommandArgument = register.Id.ToString();

                var row = new TableRow
                {
                    Cells =
                    {
                        new TableCell { Text = register.RegistrationID.ToString() },
                        new TableCell { Text = register.CustomerId.ToString() },
                        new TableCell { Text = register.ProductCode },
                        new TableCell { Text = register.RegistrationDate.ToString() }
                    }
                };

                if (Global.CurrentAccount.Role == AccountRole.Administrator)
                {
                    row.Cells.Add(new TableCell
                    {
                        Width = Unit.Pixel(80),
                        Controls = { deleteButton }
                    });
                }

                ItemsTable.Rows.Add(row);
            }

            _customers = Account.AccountList.Where(a => a.Role == AccountRole.Customer).ToList();
            _products = (List<Product>)Product.ProductList;

            ProductDropDown.DataSource = _products;
            ProductDropDown.DataBind();

            CustomerDropDown.DataSource = _customers;
            CustomerDropDown.DataBind();

            DateTextBox.Text = DateTime.Now.ToString("M/d/yyyy");
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
                case "Delete":
                    {
                        using (var command = new SqlCommand(connection: Database.Connection,
                            cmdText: "delete from [Registrations] where RegistrationID = @RegistrationID"))
                        {
                            command.Parameters.Add(new SqlParameter("@RegistrationID", e.CommandArgument));
                            command.ExecuteNonQuery();
                        }

                        Response.Redirect(Request.RawUrl);
                    }
                    break;
            }
        }

        /// <summary>
        /// Adds a registration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddRegistration(object sender, EventArgs e)
        {
            Assessment3.Registration.Add(
                customerId: _customers[CustomerDropDown.SelectedIndex].Id,
                productCode: _products[ProductDropDown.SelectedIndex].Code,
                registrationDate: DateTime.Now);

            Response.Redirect(Request.RawUrl);
        }
    }
}
