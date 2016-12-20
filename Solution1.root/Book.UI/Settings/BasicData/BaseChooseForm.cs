using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace Book.UI.Settings.BasicData
{
    public partial class BaseChooseForm : XtraForm
    {
        #region Constructors

        public BaseChooseForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        #endregion

        #region Data

        protected BL.BaseManager manager;

        #endregion

        #region Properties

        public Object SelectedItem
        {
            get
            {
                return this.bindingSource1.Current;
            }
        }

        #endregion

        #region Overrideable

        protected virtual BaseEditForm GetEditForm()
        {
            return null;
        }
        /// <summary>
        /// 无列表添加页面
        /// </summary>
        /// <returns></returns>
        protected virtual BaseEditForm1 GetEditForm1()
        {
            return null;
        }
        protected virtual void LoadData()
        {
            if (this.manager != null)
            {
                MethodInfo methodInfo = null;
                methodInfo = this.manager.GetType().GetMethod("Select", new Type[] { });
                if (methodInfo != null)
                {
                    this.bindingSource1.DataSource = methodInfo.Invoke(this.manager, null);
                }
            }
        }

        #endregion

        #region Button Events

        private void simpleButtonNew_Click(object sender, EventArgs e)
        {

            BaseEditForm f = this.GetEditForm();

            if (f != null)
            {
                if (f != null && f.ShowDialog(this) == DialogResult.OK)
                {
                    this.LoadData();
                }
            }
            else
            {
                BaseEditForm1 f1 = this.GetEditForm1();
                if (f1 != null && f1.ShowDialog(this) == DialogResult.OK)
                {
                    this.LoadData();
                }
            }
        }
        public virtual void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current == null)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region Form Evenets

        private void BaseChooseForm_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = this.bindingSource1;
            this.LoadData();
        }


        #endregion

        #region GridView Events

        public virtual void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(view.GridControl.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !view.IsGroupRow(hitInfo.RowHandle))
            {
                this.simpleButtonOK.PerformClick();
            }
        }

        #endregion
    }
}