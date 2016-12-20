using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Company
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 貨運方式的維護頁面
   // 文 件 名：FreightForm
   // 编 码 人: 刘永亮                   完成时间:2010-08-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class FreightForm : BaseEditForm1
    {

        #region 变量对象定义
        private BL.ConveyanceMethodManager conveyanceManager = new Book.BL.ConveyanceMethodManager();
        private Model.ConveyanceMethod conveyance = new Book.Model.ConveyanceMethod();
        IList<Model.ConveyanceMethod> _detail = new List<Model.ConveyanceMethod>();
        #endregion

        #region 構造函數
        public FreightForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ConveyanceMethod.PROPERTY_CONVEYANCEMETHODNAME, new AA(Properties.Resources.RequireDataForName, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.ConveyanceMethod.PROPERTY_CONVEYANCEMETHODNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.bindingSource1.DataSource = conveyanceManager.Select();
            this.action = "view";
        }
        #endregion

        #region 繼承基類的方法
        protected override void Delete()
        {
            if ((bindingSource1.DataSource as IList<Model.ConveyanceMethod>).Count == 0) return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            Model.ConveyanceMethod convert = this.bindingSource1.Current as Model.ConveyanceMethod;
            this.conveyanceManager.Delete(convert.ConveyanceMethodId);
        }

        public override void Refresh()
        {
            this._detail = conveyanceManager.Select();
            this.bindingSource1.DataSource = this._detail;
            if (this.action == "insert")
            {
                Model.ConveyanceMethod convery = new Book.Model.ConveyanceMethod();
                convery.ConveyanceMethodId = Guid.NewGuid().ToString();
                conveyance.ConveyanceMethodName = string.Empty;
                this._detail.Add(convery);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(convery);
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

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.conveyanceManager.Update(_detail);
                    break;
                case "update":
                    this.conveyanceManager.Update(_detail);
                    break;
                default:
                    break;
            }
        }

        protected override void grid_keyDpwn()
        {
            Model.ConveyanceMethod convery = new Book.Model.ConveyanceMethod();
            convery.ConveyanceMethodId = Guid.NewGuid().ToString();
            convery.ConveyanceMethodName = string.Empty;
            this._detail.Add(convery);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(convery);
        }
        protected override void grid_KeyDelete()
        {
            Model.ConveyanceMethod con = this.bindingSource1.Current as Model.ConveyanceMethod;
            if (con == null) return;
            this._detail.Remove(con);
            this.conveyanceManager.Delete(con.ConveyanceMethodId);
        }
        #endregion
    }
}