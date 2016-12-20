using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XS
{
    public partial class ChooseOperationForm : DevExpress.XtraEditors.XtraForm
    {        
        private BL.EmployeeManager employeeManager = new BL.EmployeeManager();        
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();             
        public ChooseOperationForm()
        {
            InitializeComponent();
            this.newChooseCustom.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            Model.Customer customer = newChooseCustom.EditValue as Model.Customer;

            if (customer == null)
                customer = new Book.Model.Customer();

            Model.Employee emp = this.comboBoxRole.EditValue as Model.Employee;
            if (emp == null)
                emp = new Book.Model.Employee();

            IList<Model.InvoiceXO> invoicesXOs = invoiceXOManager.SelectByYJRQCustomEmpCusXOId(customer, this.dateEditStartDate.DateTime, this.dateEditEndDate.DateTime, emp,null);

            if (invoicesXOs.Count <= 0)
            {
                MessageBox.Show("尚沒有應該處理的訂單。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            XSForm f = new XSForm(invoicesXOs);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void ChooseOperationForm_Load(object sender, EventArgs e)
        {
            IList<Model.Employee> roles = employeeManager.Select(Book.UI.Settings.BasicData.Employees.EmployeeParameters.BUSINESS);
            foreach (Model.Employee role in roles)
            {
                this.comboBoxRole.Properties.Items.Add(role);
            }
        }

    }
}