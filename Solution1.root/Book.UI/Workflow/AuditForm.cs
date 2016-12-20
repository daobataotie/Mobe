using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Workflow
{
    public partial class AuditForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.RoleAuditingManager roleAuditingManager = new Book.BL.RoleAuditingManager();
        private BL.RoleOperationManager roleOperationManager = new Book.BL.RoleOperationManager();
        public AuditForm()
        {
            InitializeComponent();
        
            this.dateEditStart.DateTime = DateTime.Now.Date.AddDays(-7);
            this.dateEditEnd.DateTime = DateTime.Now;
            this.checkEditNoAudit.Checked = true;
            this.bindingSource1.DataSource = roleAuditingManager.GetByDate(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, this.newChooseDeprent.EditValue as Model.Department, this.newChooseEmp0.EditValue as Model.Employee, BL.V.ActiveOperator, this.checkEditNoAudit.Checked, this.checkEditHasAudit.Checked);
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                Model.RoleAuditing roleAuditing = this.bindingSource1.Current as Model.RoleAuditing;
                // string formname = "Settings.ProduceManager.Techonlogy.EidtForm";//wfr.Workflow.Tables.TableCode;
                string formname = roleOperationManager.GetbyTable(roleAuditing.TableName);
                if (roleAuditing != null)
                {
                    Form f = null;
                    foreach (Form form in this.MdiParent.MdiChildren)
                    {
                        if (form.GetType().FullName.EndsWith(formname))
                        {
                            f = form;
                            break;
                        }
                    }

                    if (f == null)
                    {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                        f = (Form)assembly.CreateInstance(string.Format("{0}.{1}", assembly.GetName().Name, formname), false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { roleAuditing.InvoiceId }, null, null);
                    }

                    f.MdiParent = this.MdiParent;
                    f.Show();
                    f.BringToFront();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = roleAuditingManager.GetByDate(this.dateEditStart.DateTime, this.dateEditEnd.DateTime, this.newChooseDeprent.EditValue as Model.Department, this.newChooseEmp0.EditValue as Model.Employee, BL.V.ActiveOperator, this.checkEditNoAudit.Checked, this.checkEditHasAudit.Checked);

        }
    }
}