using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtAccountSubject
{
    public partial class TheirBalanceForm : DevExpress.XtraEditors.XtraForm
    {
        BL.AtAccountSubjectManager accountSubjectManager = new Book.BL.AtAccountSubjectManager();
        public TheirBalanceForm()
        {
            InitializeComponent();
        }

        private void TheirBalanceForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = accountSubjectManager.Select();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gridView1.PostEditor();
            this.gridView1.UpdateCurrentRow();

            IList<Model.AtAccountSubject> accountList =this.bindingSource1.DataSource as IList<Model.AtAccountSubject>;
            //int rowCount = dt.Rows.Count;
            if (accountList != null)
            {
                if (accountList.Count > 0)
                {
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        this.accountSubjectManager.Update(accountList[i]);
                    }

                   
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}