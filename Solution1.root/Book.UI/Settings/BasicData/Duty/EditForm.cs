using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Duty
{
    public partial class EditForm : BaseEditForm1
    {

        #region 定义对象
        private IList<Model.Duty> _detail = new List<Model.Duty>();
        private Model.Duty _duty = new Book.Model.Duty();
        private BL.DutyManager _dutymanager = new Book.BL.DutyManager();
        #endregion
          

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.Duty.PROPERTY_DUTYNAME, new AA(Properties.Resources.RequireDataForName, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.Duty.PROPERTY_DUTYNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._dutymanager.Delete(bindingSource1.Current as Model.Duty);
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    //this._dutymanager.Update(this._detail);
                    //break;
                case "update":
                    this._dutymanager.Update(this._detail);
                    break;
                default:
                    break;
            }
        }

        public override void Refresh()
        {
            this._detail = this._dutymanager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.Duty duty = new Book.Model.Duty();
                duty.DutyId = Guid.NewGuid().ToString();
                //  department.Id = string.Empty;
                duty.DutyName = string.Empty;
                // department.DepartmentDescription = string.Empty;
                this._detail.Add(duty);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(duty);
                this.gridControl1.RefreshDataSource();
            }
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }
            base.Refresh();
        }
        protected override void grid_keyDpwn()
        {
            Model.Duty duty = new Book.Model.Duty();
            duty.DutyId = Guid.NewGuid().ToString();
            //department.Id = string.Empty;
            duty.DutyName = string.Empty;
            // department.DepartmentDescription = string.Empty;
            this._detail.Add(duty);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(duty);
        }

        protected override void grid_KeyDelete()
        {
            this._detail.Remove(this.bindingSource1.Current as Model.Duty);
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
