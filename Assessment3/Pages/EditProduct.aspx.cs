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

    public partial class EditProduct : Page
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

            if (IsPostBack) return;

            Product product;
            Product.Find(Global.EditProductCode, out product);

            NameTextBox.Text = product.Name;
            VersionTextBox.Text = product.Version.ToString();
            ReleaseDateTextBox.Text = product.ReleaseDate.ToString();
        }

        /// <summary>
        /// Applies changes to the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplyChanges(object sender, EventArgs e)
        {
            Product product;
            Product.Find(Global.EditProductCode, out product);

            product.Name = NameTextBox.Text;
            product.Version = Convert.ToDecimal(VersionTextBox.Text);

            DateTime releaseDate;
            DateTime.TryParse(ReleaseDateTextBox.Text, out releaseDate);

            var low = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            var high = new DateTime(9999, 12, 31, 22, 59, 59);

            if (releaseDate < low) releaseDate = low;
            if (releaseDate > high) releaseDate = high;

            product.ReleaseDate = new DateTime(Math.Max(low.Ticks, Math.Min(high.Ticks, releaseDate.Ticks)));

            Response.Redirect("~/Pages/ProductMaintenance");
        }

        /// <summary>
        /// Returns to the product maintenance page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/ProductMaintenance");
        }
    }
}
