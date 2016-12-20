using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZX
{
    public partial class ZXParameterSet : Settings.BasicData.BaseEditForm
    {
        IList<Model.Setting> _detail;
        Model.Setting _setting;
        BL.SettingManager _settingManager = new Book.BL.SettingManager();

        public ZXParameterSet()
        {
            InitializeComponent();

            _detail = _settingManager.SelectTagOrderDefault("BOX");

            if (this._detail == null || this._detail.Count == 0)
                this._setting = null;
            else
                this._setting = this._detail[0];

            this.bindingSource1.DataSource = this._detail;

            this.requireValueExceptions.Add(Model.Setting.PRO_IdNO, new AA(Properties.Resources.NewNumbers, this.txt_Id));
            this.invalidValueExceptions.Add(Model.Setting.PRO_IdNO, new AA(Properties.Resources.EntityExists, this.txt_Id));
            this.invalidValueExceptions.Add(Model.Setting.PRO_Blong, new AA(Properties.Resources.BLongIsNotLessThanZero, this.spe_BLong));
            this.invalidValueExceptions.Add(Model.Setting.PRO_BWidth, new AA(Properties.Resources.BWidthIsNotLessThanZero, this.spe_BWide));
            this.invalidValueExceptions.Add(Model.Setting.PRO_BHeight, new AA(Properties.Resources.BHeigthIsNotLessThanZero, this.spe_BHigh));
            this.action = "view";
        }

        public ZXParameterSet(string invoiceId)
            : this()
        {
            this._setting = this._settingManager.Get(invoiceId);
            if (this._setting == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
        }

        public ZXParameterSet(Model.Setting setting)
            : this()
        {
            if (setting == null)
                throw new ArithmeticException("invoiceid");
            this._setting = setting;
            this.action = "view";
        }

        public ZXParameterSet(Model.Setting setting, string action)
            : this()
        {
            //this._PCOtherCheck = mPCOtherCheck;
            //this.action = action;
            //if (this.action == "view")
            //    LastFlag = 1;
            if (setting == null)
                throw new ArithmeticException("invoiceid");
            this._setting = setting;
            this.action = action;
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this._settingManager.Delete((bindingSource1.Current as Model.Setting).SettingId);
                this._detail.Remove(bindingSource1.Current as Model.Setting);
                this.gridControl1.RefreshDataSource();
            }
        }

        protected override void Save()
        {
            /*
             * 
             * 取得控件值 返回model 
             * 
             * 
             */

            this._setting.IdNO = this.txt_Id.Text;
            this._setting.PictrueLogo = new byte[] { };
            this._setting.SettingCurrentValue = this.spe_BLong.Value + "," + this.spe_BWide.Value + "," + this.spe_BHigh.Value + "," + this.spe_JWeight.Value + "," + this.spe_MWeight.Value + "," + this.spe_Caiji.Value;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._settingManager.Insert(this._setting);
                    break;
                case "update":
                    this._settingManager.Update(this._setting);
                    break;
                default:
                    break;
            }

            this._detail = _settingManager.SelectTagOrderDefault("BOX");
            this.bindingSource1.DataSource = this._detail;
            this.bindingSource1.Position = this.bindingSource1.IndexOf(_setting);

        }

        public override void Refresh()
        {
            if (this._detail.Count == 0)
            {
                AddNew();
            }

            this.txt_Id.Text = this._setting.IdNO;
            this.spe_BLong.Text = this._setting.Blong.ToString();
            this.spe_BWide.Text = this._setting.BWidth.ToString();
            this.spe_BHigh.Text = this._setting.BHeight.ToString();
            this.spe_JWeight.Text = this._setting.BJWeight.ToString();
            this.spe_MWeight.Text = this._setting.BMWeight.ToString();
            this.spe_Caiji.Text = this._setting.BCaiJi.ToString();

            base.Refresh();
            this.gridView1.OptionsBehavior.Editable = false;

        }

        protected override void AddNew()
        {
            this._setting = new Book.Model.Setting();
            _setting.SettingId = Guid.NewGuid().ToString();
            _setting.SettingTags = "BOX";
            _setting.SettingName = "Box";
            this.action = "insert";
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            if (this.bindingSource1.Count != 0)
            {
                this._setting = this.bindingSource1.Current as Model.Setting;
                this.txt_Id.Text = this._setting.IdNO;
                this.spe_BLong.Text = this._setting.Blong.ToString();
                this.spe_BWide.Text = this._setting.BWidth.ToString();
                this.spe_BHigh.Text = this._setting.BHeight.ToString();
                this.spe_JWeight.Text = this._setting.BJWeight.ToString();
                this.spe_MWeight.Text = this._setting.BMWeight.ToString();
                this.spe_Caiji.Text = this._setting.BCaiJi.ToString();
            }
            if(this.action=="update" || this.action=="insert")
            {
                this.action = "view";
                Refresh();
            }
        }
        protected override bool HasRows()
        {
            return true;
        }

    }
}