using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        private BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        // private Model.ProductCategory productCategory = null;
        private IList<Model.ProductCategory> _detail = new List<Model.ProductCategory>();

        private DataSet ds = new DataSet();
        private DataTable dt1 = new DataTable("dt1");
        private DataTable dt2 = new DataTable("dt2");
        private DataTable dt3 = new DataTable("dt3");
        DataRow drOne;
        DataRow drTwo;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.ProductCategory.PROPERTY_ID, new AA(Properties.Resources.RequireIdName, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.ProductCategory.PROPERTY_ID, new AA(Properties.Resources.EntityExists, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME, new AA(Properties.Resources.EntityName, this.gridControl1 as Control));
            this.action = "view";

            DataRow drOne = dt1.NewRow();
            DataRow drTwo = dt2.NewRow();
        }
        #endregion

        #region Form Events

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.Visibles();
        }

        #endregion

        #region Overrieded
        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            //this.productCategoryManager.Delete(this.bindingSource1.Current as Model.ProductCategory);
            GridView gv = this.gridControl1.FocusedView as GridView;
            if (gv != null)
            {
                DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);
                if (dr != null)
                {
                    Model.ProductCategory pc = new Book.Model.ProductCategory();
                    pc.ProductCategoryId = dr["ProductCategoryId"].ToString();

                    this.productCategoryManager.Delete(pc);
                    return;
                }
            }
            throw new global::Helper.MessageValueException("请先选择要删除的行！");
        }

        protected override void Save()
        {
            foreach (GridView item in this.gridControl1.Views)
            {
                item.PostEditor();
                item.UpdateCurrentRow();
            }
            if (this.action != "view")
                this.productCategoryManager.Update(this.ds);
            //this.productCategoryManager.Update(this._detail);
        }

        #endregion

        protected override void AddNew()
        {
            this.action = "insert";
        }

        public override void Refresh()
        {
            //this._detail = productCategoryManager.Select();
            //this.bindingSource1.DataSource = this._detail;
            dt1 = productCategoryManager.SelectDTByFilter("where CategoryLevel=1");
            dt1.TableName = "DT1";

            dt2 = productCategoryManager.SelectDTByFilter("where CategoryLevel=2");
            dt2.TableName = "DT2";

            dt3 = productCategoryManager.SelectDTByFilter("where CategoryLevel=3");
            dt3.TableName = "DT3";

            ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);

            ds.Relations.Add("1", dt1.Columns["ProductCategoryId"], dt2.Columns["ProductCategoryParentId"]);
            ds.Relations.Add("2", dt2.Columns["ProductCategoryId"], dt3.Columns["ProductCategoryParentId"]);

            this.bindingSource1.DataSource = dt1;

            if (this.action == "insert")
            {
                //Model.ProductCategory pc = new Book.Model.ProductCategory();
                //pc.ProductCategoryId = Guid.NewGuid().ToString();
                //this._detail.Add(pc);
                //this.bindingSource1.DataSource = this._detail;

                DataRow dr = dt1.NewRow();
                dr["ProductCategoryId"] = Guid.NewGuid().ToString();
                dr["InsertTime"] = DateTime.Now;
                dr["UpdateTime"] = DateTime.Now;
                dr["CategoryLevel"] = 1;
                dt1.Rows.Add(dr);
                this.bindingSource1.Position = dt1.Rows.IndexOf(dr);
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

            if (this.action != "view")
                this.btn_AddTwo.Enabled = true;
            this.btn_AddThree.Enabled = false;
        }

        protected override bool HasRows()
        {
            return this.productCategoryManager.HasRows();
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this });
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Model.ProductCategory pc = this.gridView1.GetRow(e.RowHandle) as Model.ProductCategory;
            //if (e.Value == null) return;
            //switch (e.Column.Name)
            //{
            //    case "gridColumn1":
            //        pc.Id = e.Value.ToString();
            //        break;
            //    case "gridColumn2":
            //        pc.ProductCategoryName = e.Value.ToString();
            //        break;
            //}
        }

        private void btn_AddTwo_Click(object sender, EventArgs e)
        {
            if (drOne == null)
                drOne = (this.gridControl1.FocusedView as GridView).GetDataRow((this.gridControl1.FocusedView as GridView).FocusedRowHandle);
            DataRow dr = ds.Tables["DT2"].NewRow();
            dr["ProductCategoryId"] = Guid.NewGuid().ToString();
            dr["ProductCategoryParentId"] = drOne["ProductCategoryId"].ToString();
            dr["InsertTime"] = DateTime.Now;
            dr["UpdateTime"] = DateTime.Now;
            dr["CategoryLevel"] = 2;
            dt2.Rows.Add(dr);

            this.gridControl1.RefreshDataSource();
        }

        private void btn_AddThree_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables["DT3"].NewRow();
            dr["ProductCategoryId"] = Guid.NewGuid().ToString();
            dr["ProductCategoryParentId"] = drTwo["ProductCategoryId"].ToString();
            dr["InsertTime"] = DateTime.Now;
            dr["UpdateTime"] = DateTime.Now;
            dr["CategoryLevel"] = 3;
            dt3.Rows.Add(dr);

            this.gridControl1.RefreshDataSource();
        }

        private void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView gv = e.View as GridView;
            gv.OptionsView.ShowColumnHeaders = false;
            gv.Columns.Clear();
            GridColumn gc1 = gv.Columns.Add();
            GridColumn gc2 = gv.Columns.Add();

            gc1.FieldName = "Id";
            gc1.Name = "gc1";
            gc1.Caption = "商品类别编号";
            gc1.Visible = true;

            gc2.FieldName = "ProductCategoryName";
            gc2.Name = "gc2";
            gc2.Caption = "商品类别名称";
            gc2.Visible = true;

            if (this.gridControl1.Views.Count == 2)
            {
                gc1.Width = 165;
                gc2.Width = 232;
            }
            else if (this.gridControl1.Views.Count == 3)
            {
                gc1.Width = 126;
                gc2.Width = 232;
            }

            gv.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gv_FocusedRowChanged);
            gv.KeyDown += new KeyEventHandler(gv_KeyDown);
        }

        void gv_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view" && e.KeyData == Keys.Delete)
            {
                DataRow dr = (sender as GridView).GetDataRow((sender as GridView).FocusedRowHandle);
                dr.Table.Rows.Remove(dr);

                this.gridControl1.RefreshDataSource();
            }
        }

        void gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null)
                return;

            if (gridControl1.Views[0] == gv)
            {
                this.drOne = gv.GetDataRow(gv.FocusedRowHandle);
            }
            else if (gridControl1.Views[1] == gv)
            {
                this.drTwo = gv.GetDataRow(gv.FocusedRowHandle);
            }
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            GridView gv = e.View as GridView;
            if (gv == null)
                return;

            if (gridControl1.Views[0] == gv)
            {
                if (this.action != "view")
                {
                    this.btn_AddTwo.Enabled = true;
                    this.btn_AddThree.Enabled = false;
                }

                this.drOne = gv.GetDataRow(gv.FocusedRowHandle);
            }
            else if (gridControl1.Views[1] == gv)
            {
                if (this.action != "view")
                {
                    this.btn_AddTwo.Enabled = false;
                    this.btn_AddThree.Enabled = true;
                }

                this.drTwo = gv.GetDataRow(gv.FocusedRowHandle);
            }
            else
            {
                if (this.action != "view")
                {
                    this.btn_AddTwo.Enabled = false;
                    this.btn_AddThree.Enabled = false;
                }
            }
        }
    }
}