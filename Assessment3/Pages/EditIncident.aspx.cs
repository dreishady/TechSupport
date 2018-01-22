using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Assessment3.Pages
{
    /*
        Author     : Ben Moir | Waleed
        Student ID : 5101965116 | 6100758617
        Date       : 13/11/2017
        Known Bugs : None
    */

    public partial class EditIncident : Page
    {
        private List<Account> _customers;
        private List<Account> _technicians;
        private List<Product> _products;

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

            _products = (List<Product>)Product.ProductList;
            _customers = Account.AccountList.Where(a => a.Role == AccountRole.Customer).ToList();
            _technicians = Account.AccountList.Where(a => a.Role != AccountRole.Customer).ToList();

            if (IsPostBack) return;

            ProductDropDown.DataSource = _products;
            ProductDropDown.DataBind();

            CustomerDropDown.DataSource = _customers;
            CustomerDropDown.DataBind();

            TechnicianDropDown.DataSource = _technicians;
            TechnicianDropDown.DataBind();

            Incident incident;
            Incident.Find(Global.EditIncidentId, out incident);

            CustomerDropDown.SelectedIndex = _customers.FindIndex(a => a.Id == incident.CustomerID);
            TechnicianDropDown.SelectedIndex = _technicians.FindIndex(a => a.Id == incident.TechID);
            ProductDropDown.SelectedIndex = _products.FindIndex(p => p.Code == incident.ProductCode);

            OpenDateTextBox.Text = incident.DateOpened.ToString();
            CloseDateTextBox.Text = incident.DateClosed.ToString();
            TitleTextBox.Text = incident.Title;
            DescriptionTextBox.Text = incident.Description;
        }

        /// <summary>
        /// Applies changes to the incident
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplyChanges(object sender, EventArgs e)
        {
            Incident incident;
            Incident.Find(Global.EditIncidentId, out incident);

            incident.CustomerID = _customers[CustomerDropDown.SelectedIndex].Id;
            incident.TechID = _technicians[TechnicianDropDown.SelectedIndex].Id;
            incident.ProductCode = _products[ProductDropDown.SelectedIndex].Code;

            DateTime closeDate;
            DateTime.TryParse(CloseDateTextBox.Text, out closeDate);

            var low = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            var high = new DateTime(9999, 12, 31, 22, 59, 59);

            incident.DateClosed = new DateTime(Math.Max(low.Ticks, Math.Min(high.Ticks, closeDate.Ticks)));

            incident.Title = TitleTextBox.Text;
            incident.Description = DescriptionTextBox.Text;

            Response.Redirect("~/Pages/Incidents");
        }

        /// <summary>
        /// Returns to the incidents page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Incidents");
        }
    }
}
