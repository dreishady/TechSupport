/*
 * Author     : Ben Moir | Micheal Thompson
 * Date       : 10/11/2017
 * Student ID : 5101965116 | 3100553617
 * Known Bugs : None
 */

using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assessment3
{
    public partial class ProductMaintenance : Page
    {
        /// <summary>
        /// Generates the product table
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

            foreach (var product in Product.ProductList)
            {
                var editButton = new Button { Text = "Edit", CssClass = "btn btn-primary EditButton" };
                var deleteButton = new Button { Text = "Delete", CssClass = "btn btn-primary" };

                editButton.Command += ButtonCommand;
                editButton.CommandName = "Edit";
                editButton.CommandArgument = product.Code;

                deleteButton.Command += ButtonCommand;
                deleteButton.CommandName = "Delete";
                deleteButton.CommandArgument = product.Code;

                var row = new TableRow
                {
                    Cells =
                    {
                        new TableCell { Text = product.Code },
                        new TableCell { Text = product.Name },
                        new TableCell { Text = product.Version.ToString() },
                        new TableCell { Text = product.ReleaseDate.ToString() },
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

                ItemsTable.Rows.Add(row);
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
                        Global.EditProductCode = e.CommandArgument.ToString();
                        Response.Redirect("~/Pages/EditProduct.aspx");
                    } break;
                case "Delete":
                    {
                        using (var command = new SqlCommand(connection: Database.Connection,
                            cmdText: "delete from [Products] where ProductCode = @ProductCode"))
                        {
                            command.Parameters.Add(new SqlParameter("@ProductCode", e.CommandArgument));
                            command.ExecuteNonQuery();
                        }

                        Response.Redirect(Request.RawUrl);
                    } break;
            }
        }

        /// <summary>
        /// Toggles the add product menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClickEvent(object sender, EventArgs e)
        {
            AddProductTable.Visible = !AddProductTable.Visible;
            Update();
        }

        /// <summary>
        /// Adds a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddProduct(object sender, EventArgs e)
        {
            var product = Product.Add(ProductCodeTextBox.Text);
            product.Name = ProductNameTextBox.Text;
            product.Version = Convert.ToDecimal(ProductVersionTextBox.Text);
            product.ReleaseDate = DateTime.Now;

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
                    AddProductButton.Visible = true;
                    break;

                default:
                    AddProductButton.Visible = false;
                    break;
            }
            AddProductButton.Text = AddProductTable.Visible ? "Hide" : "Add New Product";
        }
    }
}
