using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class ChooseCustomsForm2 : DevExpress.XtraEditors.XtraForm
    {
        public List<Model.Customer> Customers = new List<Book.Model.Customer>();

        public ChooseCustomsForm2()
        {
            InitializeComponent();

            this.gridControl1.DataSource = Customers = new BL.CustomerManager().Select().ToList();
        }

        public ChooseCustomsForm2(List<Model.Customer> list)
            : this()
        {
            if (list != null || list.Count > 0)
            {
                Customers.Intersect(list, new CustomerComparer()).ToList().ForEach(C => C.IsChecked = true);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class CustomerComparer : IEqualityComparer<Model.Customer>
    {
        public bool Equals(Model.Customer x, Model.Customer y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;
            return x != null && y != null && x.CustomerId == y.CustomerId;
        }

        public int GetHashCode(Model.Customer obj)
        {
            return obj.CustomerId.GetHashCode();
        }
    }
}