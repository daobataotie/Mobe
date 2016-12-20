using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI
{
    public partial class FlowChartForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.RoleOperationManager RoleOperationManager = new BL.RoleOperationManager();

        public FlowChartForm()
        {
            InitializeComponent(); 

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            object obj = null;

            if (sender.GetType() == typeof(System.Windows.Forms.Button))
            {
                obj = (sender as System.Windows.Forms.Button).Tag;
            }
            if (sender.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
            {
                obj = (sender as DevExpress.XtraEditors.SimpleButton).Tag;
            }
            if (obj == null)
                return;

            string tag =  obj.ToString();

            if (!string.IsNullOrEmpty(tag))
            {
                Operations.Open(tag, this.MdiParent);
            }
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton button = sender as DevExpress.XtraEditors.SimpleButton;
            Point p = button.PointToScreen(new Point(0, button.ClientRectangle.Bottom));
            this.popupMenu1.ShowPopup(p);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            object obj = null;

            obj = e.Item.Tag;

            if (obj == null)
                return;

            string tag = obj.ToString();

            if (!string.IsNullOrEmpty(tag))
            {
                Operations.Open(tag, this.MdiParent);
            }




          
        }

        private void FlowChartForm_Load(object sender, EventArgs e)
        {
        
            IList<Model.Role> roleList;
            roleList = BL.V.RoleList;
            IList<Model.RoleOperation> RoleOPList = this.RoleOperationManager.SelectIsSearch(BL.V.ActiveOperator);
            IList<string> tagList = new List<string>();
            foreach (Model.RoleOperation roleop in RoleOPList)
            {
                if (!string.IsNullOrEmpty(roleop.Operation.KeyUrl) && !tagList.Contains(roleop.Operation.KeyUrl.ToLower()))
                {
                    tagList.Add(roleop.Operation.KeyUrl.ToLower());
                }
            }
            int flag = 0;
            foreach (Model.Role item in roleList)
            {
                if (item.Id == Settings.BasicData.Employees.EmployeeParameters.SYSTEMMANAGER)
                {
                    flag = 1;
                }
            }
            if (flag != 1)
            {
                foreach (Control con in this.Controls)
                {
                    if (con is SimpleButton)
                    {
                        if (((SimpleButton)con).Tag != null)
                        {
                            if (tagList.Contains(((SimpleButton)con).Tag.ToString().ToLower()))
                                con.Visible = true;
                            else
                                con.Visible = false;
                        }
                    }
                    else if (con is Button)
                    {
                        if (((Button)con).Tag != null)
                        {
                            if (tagList.Contains(((Button)con).Tag.ToString().ToLower()))
                                con.Visible = true;
                            else
                                con.Visible = false;
                        }
                    }
                }
            }
        }
    }
}