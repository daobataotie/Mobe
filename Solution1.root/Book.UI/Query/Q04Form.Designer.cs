namespace Book.UI.Query
{
    partial class Q04Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Q04Form));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.invoiceXTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colColumnCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTotal1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColumn1Received = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoicePayTimeLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceOwed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTotal0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee0Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepotId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee1Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee2Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee3Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceLRTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceGZTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZFTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZFCause = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceAbstract = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceYHE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZKE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTax = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceXTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.invoiceXTBindingSource;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colColumnCompanyId,
            this.colCompany,
            this.colInvoiceId,
            this.colInvoiceDate,
            this.colInvoiceTotal1,
            this.colColumn1Received,
            this.colInvoicePayTimeLimit,
            this.colInvoiceOwed,
            this.colInvoiceTotal0,
            this.colEmployee0Id,
            this.colDepotId,
            this.colEmployee1Id,
            this.colEmployee2Id,
            this.colEmployee3Id,
            this.colInvoiceLRTime,
            this.colInvoiceGZTime,
            this.colInvoiceZFTime,
            this.colInvoiceZFCause,
            this.colInvoiceAbstract,
            this.colInvoiceNote,
            this.colInvoiceStatus,
            this.colInvoiceYHE,
            this.colInvoiceZSE,
            this.colInvoiceZKE,
            this.colInvoiceTax});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colColumnCompanyId
            // 
            this.colColumnCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colColumnCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colColumnCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colColumnCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colColumnCompanyId, "colColumnCompanyId");
            this.colColumnCompanyId.FieldName = "CompanyId";
            this.colColumnCompanyId.Name = "colColumnCompanyId";
            this.colColumnCompanyId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colColumnCompanyId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colColumnCompanyId.OptionsFilter.AllowFilter = false;
            // 
            // colCompany
            // 
            this.colCompany.AppearanceCell.Options.UseTextOptions = true;
            this.colCompany.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompany.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompany.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompany, "colCompany");
            this.colCompany.FieldName = "CompanyName1";
            this.colCompany.Name = "colCompany";
            this.colCompany.OptionsColumn.AllowEdit = false;
            this.colCompany.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.colCompany.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompany.OptionsColumn.ReadOnly = true;
            this.colCompany.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceId
            // 
            this.colInvoiceId.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceId.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceId, "colInvoiceId");
            this.colInvoiceId.FieldName = "InvoiceId";
            this.colInvoiceId.Name = "colInvoiceId";
            this.colInvoiceId.OptionsColumn.AllowEdit = false;
            this.colInvoiceId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceId.OptionsColumn.ReadOnly = true;
            this.colInvoiceId.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoiceDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoiceDate, "colInvoiceDate");
            this.colInvoiceDate.FieldName = "InvoiceDate";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.OptionsColumn.AllowEdit = false;
            this.colInvoiceDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceDate.OptionsColumn.ReadOnly = true;
            this.colInvoiceDate.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceTotal1
            // 
            this.colInvoiceTotal1.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceTotal1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceTotal1.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceTotal1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceTotal1, "colInvoiceTotal1");
            this.colInvoiceTotal1.DisplayFormat.FormatString = "0";
            this.colInvoiceTotal1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceTotal1.FieldName = "InvoiceZongJi";
            this.colInvoiceTotal1.Name = "colInvoiceTotal1";
            this.colInvoiceTotal1.OptionsColumn.AllowEdit = false;
            this.colInvoiceTotal1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceTotal1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceTotal1.OptionsColumn.ReadOnly = true;
            this.colInvoiceTotal1.OptionsFilter.AllowFilter = false;
            // 
            // colColumn1Received
            // 
            this.colColumn1Received.AppearanceCell.Options.UseTextOptions = true;
            this.colColumn1Received.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colColumn1Received.AppearanceHeader.Options.UseTextOptions = true;
            this.colColumn1Received.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colColumn1Received, "colColumn1Received");
            this.colColumn1Received.DisplayFormat.FormatString = "0";
            this.colColumn1Received.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colColumn1Received.FieldName = "received";
            this.colColumn1Received.Name = "colColumn1Received";
            this.colColumn1Received.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colColumn1Received.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colColumn1Received.OptionsFilter.AllowFilter = false;
            // 
            // colInvoicePayTimeLimit
            // 
            this.colInvoicePayTimeLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoicePayTimeLimit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoicePayTimeLimit.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoicePayTimeLimit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoicePayTimeLimit, "colInvoicePayTimeLimit");
            this.colInvoicePayTimeLimit.DisplayFormat.FormatString = "0";
            this.colInvoicePayTimeLimit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoicePayTimeLimit.FieldName = "InvoicePayTimeLimit";
            this.colInvoicePayTimeLimit.Name = "colInvoicePayTimeLimit";
            this.colInvoicePayTimeLimit.OptionsColumn.AllowEdit = false;
            this.colInvoicePayTimeLimit.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoicePayTimeLimit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoicePayTimeLimit.OptionsColumn.ReadOnly = true;
            this.colInvoicePayTimeLimit.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceOwed
            // 
            this.colInvoiceOwed.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceOwed.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceOwed.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceOwed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceOwed, "colInvoiceOwed");
            this.colInvoiceOwed.DisplayFormat.FormatString = "0";
            this.colInvoiceOwed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceOwed.FieldName = "InvoiceOwed";
            this.colInvoiceOwed.Name = "colInvoiceOwed";
            this.colInvoiceOwed.OptionsColumn.AllowEdit = false;
            this.colInvoiceOwed.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceOwed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceOwed.OptionsColumn.ReadOnly = true;
            this.colInvoiceOwed.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceTotal0
            // 
            this.colInvoiceTotal0.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceTotal0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceTotal0.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceTotal0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceTotal0, "colInvoiceTotal0");
            this.colInvoiceTotal0.DisplayFormat.FormatString = "0";
            this.colInvoiceTotal0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceTotal0.FieldName = "InvoiceYiFu";
            this.colInvoiceTotal0.Name = "colInvoiceTotal0";
            this.colInvoiceTotal0.OptionsColumn.AllowEdit = false;
            this.colInvoiceTotal0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceTotal0.OptionsColumn.ReadOnly = true;
            this.colInvoiceTotal0.OptionsFilter.AllowFilter = false;
            // 
            // colEmployee0Id
            // 
            resources.ApplyResources(this.colEmployee0Id, "colEmployee0Id");
            this.colEmployee0Id.FieldName = "Employee0Id";
            this.colEmployee0Id.Name = "colEmployee0Id";
            this.colEmployee0Id.OptionsColumn.AllowEdit = false;
            this.colEmployee0Id.OptionsColumn.ReadOnly = true;
            // 
            // colDepotId
            // 
            resources.ApplyResources(this.colDepotId, "colDepotId");
            this.colDepotId.FieldName = "DepotId";
            this.colDepotId.Name = "colDepotId";
            this.colDepotId.OptionsColumn.AllowEdit = false;
            this.colDepotId.OptionsColumn.ReadOnly = true;
            // 
            // colEmployee1Id
            // 
            resources.ApplyResources(this.colEmployee1Id, "colEmployee1Id");
            this.colEmployee1Id.FieldName = "Employee1Id";
            this.colEmployee1Id.Name = "colEmployee1Id";
            this.colEmployee1Id.OptionsColumn.AllowEdit = false;
            this.colEmployee1Id.OptionsColumn.ReadOnly = true;
            // 
            // colEmployee2Id
            // 
            resources.ApplyResources(this.colEmployee2Id, "colEmployee2Id");
            this.colEmployee2Id.FieldName = "Employee2Id";
            this.colEmployee2Id.Name = "colEmployee2Id";
            this.colEmployee2Id.OptionsColumn.AllowEdit = false;
            this.colEmployee2Id.OptionsColumn.ReadOnly = true;
            // 
            // colEmployee3Id
            // 
            resources.ApplyResources(this.colEmployee3Id, "colEmployee3Id");
            this.colEmployee3Id.FieldName = "Employee3Id";
            this.colEmployee3Id.Name = "colEmployee3Id";
            this.colEmployee3Id.OptionsColumn.AllowEdit = false;
            this.colEmployee3Id.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceLRTime
            // 
            resources.ApplyResources(this.colInvoiceLRTime, "colInvoiceLRTime");
            this.colInvoiceLRTime.FieldName = "InvoiceLRTime";
            this.colInvoiceLRTime.Name = "colInvoiceLRTime";
            this.colInvoiceLRTime.OptionsColumn.AllowEdit = false;
            this.colInvoiceLRTime.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceGZTime
            // 
            resources.ApplyResources(this.colInvoiceGZTime, "colInvoiceGZTime");
            this.colInvoiceGZTime.FieldName = "InvoiceGZTime";
            this.colInvoiceGZTime.Name = "colInvoiceGZTime";
            this.colInvoiceGZTime.OptionsColumn.AllowEdit = false;
            this.colInvoiceGZTime.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceZFTime
            // 
            resources.ApplyResources(this.colInvoiceZFTime, "colInvoiceZFTime");
            this.colInvoiceZFTime.FieldName = "InvoiceZFTime";
            this.colInvoiceZFTime.Name = "colInvoiceZFTime";
            this.colInvoiceZFTime.OptionsColumn.AllowEdit = false;
            this.colInvoiceZFTime.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceZFCause
            // 
            resources.ApplyResources(this.colInvoiceZFCause, "colInvoiceZFCause");
            this.colInvoiceZFCause.FieldName = "InvoiceZFCause";
            this.colInvoiceZFCause.Name = "colInvoiceZFCause";
            this.colInvoiceZFCause.OptionsColumn.AllowEdit = false;
            this.colInvoiceZFCause.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceAbstract
            // 
            resources.ApplyResources(this.colInvoiceAbstract, "colInvoiceAbstract");
            this.colInvoiceAbstract.FieldName = "InvoiceAbstract";
            this.colInvoiceAbstract.Name = "colInvoiceAbstract";
            this.colInvoiceAbstract.OptionsColumn.AllowEdit = false;
            this.colInvoiceAbstract.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceNote
            // 
            resources.ApplyResources(this.colInvoiceNote, "colInvoiceNote");
            this.colInvoiceNote.FieldName = "InvoiceNote";
            this.colInvoiceNote.Name = "colInvoiceNote";
            this.colInvoiceNote.OptionsColumn.AllowEdit = false;
            this.colInvoiceNote.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceStatus
            // 
            resources.ApplyResources(this.colInvoiceStatus, "colInvoiceStatus");
            this.colInvoiceStatus.FieldName = "InvoiceStatus";
            this.colInvoiceStatus.Name = "colInvoiceStatus";
            this.colInvoiceStatus.OptionsColumn.AllowEdit = false;
            this.colInvoiceStatus.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceYHE
            // 
            resources.ApplyResources(this.colInvoiceYHE, "colInvoiceYHE");
            this.colInvoiceYHE.FieldName = "InvoiceYHE";
            this.colInvoiceYHE.Name = "colInvoiceYHE";
            this.colInvoiceYHE.OptionsColumn.AllowEdit = false;
            this.colInvoiceYHE.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceZSE
            // 
            resources.ApplyResources(this.colInvoiceZSE, "colInvoiceZSE");
            this.colInvoiceZSE.FieldName = "InvoiceZSE";
            this.colInvoiceZSE.Name = "colInvoiceZSE";
            this.colInvoiceZSE.OptionsColumn.AllowEdit = false;
            this.colInvoiceZSE.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceZKE
            // 
            resources.ApplyResources(this.colInvoiceZKE, "colInvoiceZKE");
            this.colInvoiceZKE.FieldName = "InvoiceZKE";
            this.colInvoiceZKE.Name = "colInvoiceZKE";
            this.colInvoiceZKE.OptionsColumn.AllowEdit = false;
            this.colInvoiceZKE.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceTax
            // 
            resources.ApplyResources(this.colInvoiceTax, "colInvoiceTax");
            this.colInvoiceTax.FieldName = "InvoiceTax";
            this.colInvoiceTax.Name = "colInvoiceTax";
            this.colInvoiceTax.OptionsColumn.AllowEdit = false;
            this.colInvoiceTax.OptionsColumn.ReadOnly = true;
            // 
            // Q04Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "Q04Form";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Q04Form_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceXTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource invoiceXTBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceId;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee0Id;
        private DevExpress.XtraGrid.Columns.GridColumn colDepotId;
        private DevExpress.XtraGrid.Columns.GridColumn colCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee1Id;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee2Id;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee3Id;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceLRTime;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceGZTime;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZFTime;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZFCause;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceAbstract;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNote;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTotal1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceYHE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZSE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZKE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoicePayTimeLimit;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTotal0;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTax;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceOwed;
        private DevExpress.XtraGrid.Columns.GridColumn colColumnCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colColumn1Received;
    }
}