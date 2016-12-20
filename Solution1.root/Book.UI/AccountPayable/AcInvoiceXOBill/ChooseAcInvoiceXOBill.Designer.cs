namespace Book.UI.AccountPayable.AcInvoiceXOBill
{
    partial class ChooseAcInvoiceXOBill
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcInvoiceXOBillDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZongMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeJiMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaxRateMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmHeXiaoJingE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoHeXiaoTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Location = new System.Drawing.Point(105, 294);
            this.gridControl1.Size = new System.Drawing.Size(515, 29);
            // 
            // gridView1
            // 
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.dateEditEnd);
            this.layoutControl1.Controls.Add(this.dateEditStart);
            this.layoutControl1.Location = new System.Drawing.Point(10, -2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(679, 55);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(497, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(170, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "确认查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateEditEnd
            // 
            this.dateEditEnd.EditValue = null;
            this.dateEditEnd.Location = new System.Drawing.Point(321, 12);
            this.dateEditEnd.Name = "dateEditEnd";
            this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEnd.Size = new System.Drawing.Size(172, 21);
            this.dateEditEnd.StyleController = this.layoutControl1;
            this.dateEditEnd.TabIndex = 5;
            // 
            // dateEditStart
            // 
            this.dateEditStart.EditValue = null;
            this.dateEditStart.Location = new System.Drawing.Point(76, 12);
            this.dateEditStart.Name = "dateEditStart";
            this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStart.Size = new System.Drawing.Size(177, 21);
            this.dateEditStart.StyleController = this.layoutControl1;
            this.dateEditStart.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(679, 55);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditStart;
            this.layoutControlItem1.CustomizationFormText = "起始日期：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(245, 35);
            this.layoutControlItem1.Text = "起始日期：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditEnd;
            this.layoutControlItem2.CustomizationFormText = "终止日期：";
            this.layoutControlItem2.Location = new System.Drawing.Point(245, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(240, 35);
            this.layoutControlItem2.Text = "终止日期：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSearch;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(485, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(174, 35);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bindingSource1;
            this.gridControl2.Location = new System.Drawing.Point(14, 38);
            this.gridControl2.MainView = this.gridView3;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridControl2.Size = new System.Drawing.Size(665, 306);
            this.gridControl2.TabIndex = 8;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.ActiveFilterEnabled = false;
            this.gridView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnCheck,
            this.colId,
            this.colAcInvoiceXOBillDate,
            this.colSupplier,
            this.colZongMoney,
            this.colHeJiMoney,
            this.colTaxRateMoney,
            this.colmHeXiaoJingE,
            this.colNoHeXiaoTotal});
            this.gridView3.GridControl = this.gridControl2;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsCustomization.AllowFilter = false;
            this.gridView3.OptionsCustomization.AllowGroup = false;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnCheck
            // 
            this.gridColumnCheck.Caption = "选择";
            this.gridColumnCheck.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumnCheck.FieldName = "Checked";
            this.gridColumnCheck.Name = "gridColumnCheck";
            this.gridColumnCheck.Visible = true;
            this.gridColumnCheck.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colId
            // 
            this.colId.Caption = "单据编号";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.Visible = true;
            this.colId.VisibleIndex = 1;
            // 
            // colAcInvoiceXOBillDate
            // 
            this.colAcInvoiceXOBillDate.Caption = "发票日期";
            this.colAcInvoiceXOBillDate.FieldName = "AcInvoiceXOBillDate";
            this.colAcInvoiceXOBillDate.Name = "colAcInvoiceXOBillDate";
            this.colAcInvoiceXOBillDate.OptionsColumn.AllowEdit = false;
            this.colAcInvoiceXOBillDate.Visible = true;
            this.colAcInvoiceXOBillDate.VisibleIndex = 2;
            // 
            // colSupplier
            // 
            this.colSupplier.Caption = "客户";
            this.colSupplier.FieldName = "Supplier";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.OptionsColumn.AllowEdit = false;
            this.colSupplier.Visible = true;
            this.colSupplier.VisibleIndex = 3;
            // 
            // colZongMoney
            // 
            this.colZongMoney.Caption = "总额";
            this.colZongMoney.FieldName = "ZongMoney";
            this.colZongMoney.Name = "colZongMoney";
            this.colZongMoney.OptionsColumn.AllowEdit = false;
            this.colZongMoney.Visible = true;
            this.colZongMoney.VisibleIndex = 4;
            // 
            // colHeJiMoney
            // 
            this.colHeJiMoney.Caption = "合计";
            this.colHeJiMoney.FieldName = "HeJiMoney";
            this.colHeJiMoney.Name = "colHeJiMoney";
            this.colHeJiMoney.OptionsColumn.AllowEdit = false;
            this.colHeJiMoney.Visible = true;
            this.colHeJiMoney.VisibleIndex = 5;
            // 
            // colTaxRateMoney
            // 
            this.colTaxRateMoney.Caption = "税额";
            this.colTaxRateMoney.FieldName = "TaxRateMoney";
            this.colTaxRateMoney.Name = "colTaxRateMoney";
            this.colTaxRateMoney.OptionsColumn.AllowEdit = false;
            this.colTaxRateMoney.Visible = true;
            this.colTaxRateMoney.VisibleIndex = 6;
            // 
            // colmHeXiaoJingE
            // 
            this.colmHeXiaoJingE.Caption = "已核销金额";
            this.colmHeXiaoJingE.FieldName = "mHeXiaoJingE";
            this.colmHeXiaoJingE.Name = "colmHeXiaoJingE";
            this.colmHeXiaoJingE.OptionsColumn.AllowEdit = false;
            this.colmHeXiaoJingE.Visible = true;
            this.colmHeXiaoJingE.VisibleIndex = 7;
            // 
            // colNoHeXiaoTotal
            // 
            this.colNoHeXiaoTotal.Caption = "未核销金额";
            this.colNoHeXiaoTotal.FieldName = "NoHeXiaoTotal";
            this.colNoHeXiaoTotal.Name = "colNoHeXiaoTotal";
            this.colNoHeXiaoTotal.OptionsColumn.AllowEdit = false;
            this.colNoHeXiaoTotal.Visible = true;
            this.colNoHeXiaoTotal.VisibleIndex = 8;
            // 
            // ChooseAcInvoiceXOBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(691, 401);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChooseAcInvoiceXOBill";
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            this.Controls.SetChildIndex(this.simpleButtonNew, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.gridControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dateEditEnd;
        private DevExpress.XtraEditors.DateEdit dateEditStart;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCheck;
        protected DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colAcInvoiceXOBillDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colZongMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colHeJiMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colTaxRateMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colmHeXiaoJingE;
        private DevExpress.XtraGrid.Columns.GridColumn colNoHeXiaoTotal;
    }
}
