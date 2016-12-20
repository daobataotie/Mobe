using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;


namespace Book.UI.Settings.Privileges.Roles
{
    public partial class LegalPowerEditForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.OperationManager operationManager = new BL.OperationManager();
        private BL.RoleManager roleManager = new BL.RoleManager();
        private BL.RoleOperationManager roleOperationManager = new BL.RoleOperationManager();
        private DataSet ds = new DataSet();
        private string _roleid;

        public LegalPowerEditForm()
        {
            InitializeComponent();
            this.bindingSourceGrid.CurrentChanged += new EventHandler(bindingSourceGrid_CurrentChanged);
            this.gridView2.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gridView2_CellValueChanging);

            DataTable da=new DataTable();
            DataColumn dc=new DataColumn("id",typeof(string));
            da.Columns.Add(dc);
            DataColumn dc1=new DataColumn("pName",typeof(string));
            da.Columns.Add(dc1);
            DataRow dr;
            for (int i = 0; i <=int.Parse( System.Configuration.ConfigurationManager.AppSettings["AuditMaxRank"].ToString()); i++)
            {
                dr=da.NewRow();
                dr[0] = i;
                if (i == 0) dr[1] = "無";
                else 
                dr[1]=i.ToString()+"审";
                da.Rows.Add(dr);
            }

            this.repositoryItemLookUpEdit1.DataSource = da;
        }

        private void bindingSourceGrid_CurrentChanged(object sender, EventArgs e)
        {
            //object oo = this.bindingSourceGrid.Current;
            //int currentIndex = this.bindingSourceGrid.Position;
        }

        private void LegalPowerEditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceRole.DataSource = this.roleManager.Select();
            Model.Role r = this.bindingSourceRole.Current as Model.Role;
            if (r != null)
            {
                this.checkEditIsCOCount.Checked = r.IsCOCount == null ? false : r.IsCOCount.Value;
                this.checkEditIsCOPrice.Checked = r.IsCOPrice == null ? false : r.IsCOPrice.Value;
                this.checkEditIsXOPrice.Checked = r.IsXOPrice == null ? false : r.IsXOPrice.Value;
                this.checkEditIsXOQuantity.Checked = r.IsXOQuantity == null ? false : r.IsXOQuantity.Value;
                this.checkEditIsStockCount.Checked = r.IsStockCount == null ? false : r.IsStockCount.Value;
                this.checkEditIsProductCost.Checked = r.IsProductCost == null ? false : r.IsProductCost.Value;
                this.checkEditIsEmployeeBasicInfo.Checked = r.IsEmployeeBasicInfo == null ? false : r.IsEmployeeBasicInfo.Value;
                this.checkEditIsSalaryViewCalc.Checked = r.IsSalaryViewCalc == null ? false : r.IsSalaryViewCalc.Value;

                this.checkEditIsCOJiaoYiMingXi.Checked = r.IsCOJiaoYiMingXi.HasValue ? r.IsCOJiaoYiMingXi.Value : false;
                this.checkEditIsCOFaPiaoZiLiao.Checked = r.IsCOFaPiaoZiLiao.HasValue ? r.IsCOFaPiaoZiLiao.Value : false;
                this.checkEditIsCOZhangKuanZiLiao.Checked = r.IsCOZhangKuanZiLiao.HasValue ? r.IsCOZhangKuanZiLiao.Value : false;
                this.checkEditIsCOXiangGuanZiLiao.Checked = r.IsCOXiangGuanZiLiao.HasValue ? r.IsCOXiangGuanZiLiao.Value : false;
                this.checkEditIsCOJinHuoJinE.Checked = r.IsCOJinHuoJinE.HasValue ? r.IsCOJinHuoJinE.Value : false;

                this.checkEditIsXOJiaoYiMingXi.Checked = r.IsXOJiaoYiMingXi.HasValue ? r.IsXOJiaoYiMingXi.Value : false;
                this.checkEditIsXOFaPiaoZiLiao.Checked = r.IsXOFaPiaoZiLiao.HasValue ? r.IsXOFaPiaoZiLiao.Value : false;
                this.checkEditIsXOZhangKuanZiLiao.Checked = r.IsXOZhangKuanZiLiao.HasValue ? r.IsXOZhangKuanZiLiao.Value : false;
                this.checkEditIsXOXiangGuanZiLiao.Checked = r.IsXOXiangGuanZiLiao.HasValue ? r.IsXOXiangGuanZiLiao.Value : false;
                this.checkEditIsXOJinHuoJinE.Checked = r.IsXOJinHuoJinE.HasValue ? r.IsXOJinHuoJinE.Value : false;
            }
            treeLoad2();

        }

        private void treeLoad2()
        {
            foreach (Model.Operation operation in this.operationManager.Select_KeyTag0())
            {
                TreeListNode node = treeList2.AppendNode(new object[] { operation }, null, operation.OperationId);
                treeLoad21(operation, node);
            }
        }

        private void treeLoad21(Model.Operation operation, TreeListNode node)
        {
            foreach (Model.Operation operation1 in this.operationManager.Select_ByParent(operation.OperationId))
            {
                TreeListNode node1 = treeList2.AppendNode(new object[] { operation1 }, node, operation1.OperationId);
                if (this.operationManager.Select_ByParent(operation1.OperationId) == null) continue;
                treeLoad21(operation1, node1);
            }
        }

        private void treeList2_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null && e.Node.ParentNode != null)
            {
                if (this.bindingSourceRole.Current as Model.Role != null)
                {
                    ds = this.roleOperationManager.SelectByType(e.Node.Tag.ToString(), (this.bindingSourceRole.Current as Model.Role).RoleId);
                    this.bindingSourceGrid.DataSource = ds.Tables[0];
                }
            }
        }

        private void bindingSourceRole_CurrentChanged(object sender, EventArgs e)
        {
            _roleid = (this.bindingSourceRole.Current as Model.Role).RoleId;
            if (treeList2.Selection[0] != null)
            {
                ds = this.roleOperationManager.SelectByType(treeList2.Selection[0].Tag.ToString(), _roleid);
                this.bindingSourceGrid.DataSource = ds.Tables[0];
            }
            Model.Role r = this.bindingSourceRole.Current as Model.Role;
            if (r != null)
            {
                this.checkEditIsCOCount.Checked = r.IsCOCount == null ? false : r.IsCOCount.Value;
                this.checkEditIsCOPrice.Checked = r.IsCOPrice == null ? false : r.IsCOPrice.Value;
                this.checkEditIsXOPrice.Checked = r.IsXOPrice == null ? false : r.IsXOPrice.Value;
                this.checkEditIsXOQuantity.Checked = r.IsXOQuantity == null ? false : r.IsXOQuantity.Value;
                this.checkEditIsStockCount.Checked = r.IsStockCount == null ? false : r.IsStockCount.Value;
                this.checkEditIsProductCost.Checked = r.IsProductCost == null ? false : r.IsProductCost.Value;
                this.checkEditIsEmployeeBasicInfo.Checked = r.IsEmployeeBasicInfo == null ? false : r.IsEmployeeBasicInfo.Value;
                this.checkEditIsSalaryViewCalc.Checked = r.IsSalaryViewCalc == null ? false : r.IsSalaryViewCalc.Value;

                this.checkEditIsCOJiaoYiMingXi.Checked = r.IsCOJiaoYiMingXi.HasValue ? r.IsCOJiaoYiMingXi.Value : false;
                this.checkEditIsCOFaPiaoZiLiao.Checked = r.IsCOFaPiaoZiLiao.HasValue ? r.IsCOFaPiaoZiLiao.Value : false;
                this.checkEditIsCOZhangKuanZiLiao.Checked = r.IsCOZhangKuanZiLiao.HasValue ? r.IsCOZhangKuanZiLiao.Value : false;
                this.checkEditIsCOXiangGuanZiLiao.Checked = r.IsCOXiangGuanZiLiao.HasValue ? r.IsCOXiangGuanZiLiao.Value : false;
                this.checkEditIsCOJinHuoJinE.Checked = r.IsCOJinHuoJinE.HasValue ? r.IsCOJinHuoJinE.Value : false;

                this.checkEditIsXOJiaoYiMingXi.Checked = r.IsXOJiaoYiMingXi.HasValue ? r.IsXOJiaoYiMingXi.Value : false;
                this.checkEditIsXOFaPiaoZiLiao.Checked = r.IsXOFaPiaoZiLiao.HasValue ? r.IsXOFaPiaoZiLiao.Value : false;
                this.checkEditIsXOZhangKuanZiLiao.Checked = r.IsXOZhangKuanZiLiao.HasValue ? r.IsXOZhangKuanZiLiao.Value : false;
                this.checkEditIsXOXiangGuanZiLiao.Checked = r.IsXOXiangGuanZiLiao.HasValue ? r.IsXOXiangGuanZiLiao.Value : false;
                this.checkEditIsXOJinHuoJinE.Checked = r.IsXOJinHuoJinE.HasValue ? r.IsXOJinHuoJinE.Value : false;
            }
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.Role r = this.bindingSourceRole.Current as Model.Role;
            if (r != null)
            {
                r.IsXOQuantity = this.checkEditIsXOQuantity.Checked;
                r.IsXOPrice = this.checkEditIsXOPrice.Checked;
                r.IsCOPrice = this.checkEditIsCOPrice.Checked;
                r.IsCOCount = this.checkEditIsCOCount.Checked;
                r.IsStockCount = this.checkEditIsStockCount.Checked;
                r.IsProductCost = this.checkEditIsProductCost.Checked;
                r.IsEmployeeBasicInfo = this.checkEditIsEmployeeBasicInfo.Checked;
                r.IsSalaryViewCalc = this.checkEditIsSalaryViewCalc.Checked;

                r.IsCOJiaoYiMingXi = this.checkEditIsCOJiaoYiMingXi.Checked;
                r.IsCOFaPiaoZiLiao = this.checkEditIsCOFaPiaoZiLiao.Checked;
                r.IsCOZhangKuanZiLiao = this.checkEditIsCOZhangKuanZiLiao.Checked;
                r.IsCOXiangGuanZiLiao = this.checkEditIsCOXiangGuanZiLiao.Checked;
                r.IsCOJinHuoJinE = this.checkEditIsCOJinHuoJinE.Checked;

                r.IsXOJiaoYiMingXi = this.checkEditIsXOJiaoYiMingXi.Checked;
                r.IsXOFaPiaoZiLiao = this.checkEditIsXOFaPiaoZiLiao.Checked;
                r.IsXOZhangKuanZiLiao = this.checkEditIsXOZhangKuanZiLiao.Checked;
                r.IsXOXiangGuanZiLiao = this.checkEditIsXOXiangGuanZiLiao.Checked;
                r.IsXOJinHuoJinE = this.checkEditIsXOJinHuoJinE.Checked;              

                roleManager.Update(r);
                BL.V.RoleList = roleManager.Select(BL.V.ActiveOperator.OperatorsId);
            }
            if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;

            if (ds.HasChanges())
            {
                //int a = ds.GetChanges().Tables[0].Rows.Count;
                this.roleOperationManager.UpdateTable(ds.GetChanges(), _roleid);
                ds.AcceptChanges();
            }

            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colSelectRowAll)
            {
                this.gridView2.SetRowCellValue(e.RowHandle, this.colNew, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colUpdate, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colDel, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colView, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colExport, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colAudit, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colPrint, e.Value);
                this.gridView2.SetRowCellValue(e.RowHandle, this.colPortEdit, e.Value);
            }
        }
    }
}