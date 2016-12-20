namespace Book.UI.Invoices.XS
{
    partial class ListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInvoiceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee0Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepotId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoicePayTimeLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceOwed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTotal1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceAbstract = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceTotal0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZKE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceYHE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvoiceId,
            this.colEmployee0Id,
            this.colCompanyId,
            this.colDepotId,
            this.colInvoiceDate,
            this.colInvoicePayTimeLimit,
            this.colInvoiceOwed,
            this.colInvoiceTotal1,
            this.gridColumn1,
            this.colInvoiceStatus,
            this.colInvoiceAbstract,
            this.colInvoiceTotal0,
            this.colInvoiceZKE,
            this.colInvoiceZSE,
            this.colInvoiceYHE});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
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
            this.colInvoiceId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colEmployee0Id
            // 
            this.colEmployee0Id.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployee0Id.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colEmployee0Id, "colEmployee0Id");
            this.colEmployee0Id.FieldName = "Employee0";
            this.colEmployee0Id.Name = "colEmployee0Id";
            // 
            // colCompanyId
            // 
            this.colCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyId, "colCompanyId");
            this.colCompanyId.FieldName = "Customer";
            this.colCompanyId.Name = "colCompanyId";
            // 
            // colDepotId
            // 
            this.colDepotId.AppearanceCell.Options.UseTextOptions = true;
            this.colDepotId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDepotId.AppearanceHeader.Options.UseTextOptions = true;
            this.colDepotId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colDepotId, "colDepotId");
            this.colDepotId.FieldName = "Depot";
            this.colDepotId.Name = "colDepotId";
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
            // 
            // colInvoicePayTimeLimit
            // 
            this.colInvoicePayTimeLimit.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoicePayTimeLimit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoicePayTimeLimit.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoicePayTimeLimit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoicePayTimeLimit, "colInvoicePayTimeLimit");
            this.colInvoicePayTimeLimit.FieldName = "InvoicePayTimeLimit";
            this.colInvoicePayTimeLimit.Name = "colInvoicePayTimeLimit";
            // 
            // colInvoiceOwed
            // 
            this.colInvoiceOwed.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceOwed.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceOwed.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceOwed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceOwed, "colInvoiceOwed");
            this.colInvoiceOwed.FieldName = "InvoiceOwed";
            this.colInvoiceOwed.Name = "colInvoiceOwed";
            this.colInvoiceOwed.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colInvoiceTotal1
            // 
            this.colInvoiceTotal1.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceTotal1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceTotal1.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceTotal1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceTotal1, "colInvoiceTotal1");
            this.colInvoiceTotal1.FieldName = "InvoiceZongJi";
            this.colInvoiceTotal1.Name = "colInvoiceTotal1";
            this.colInvoiceTotal1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "InvoiceNote";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // colInvoiceStatus
            // 
            this.colInvoiceStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoiceStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoiceStatus, "colInvoiceStatus");
            this.colInvoiceStatus.FieldName = "InvoiceStatusDesc";
            this.colInvoiceStatus.Name = "colInvoiceStatus";
            // 
            // colInvoiceAbstract
            // 
            this.colInvoiceAbstract.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceAbstract.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceAbstract.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceAbstract.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceAbstract, "colInvoiceAbstract");
            this.colInvoiceAbstract.FieldName = "InvoiceAbstract";
            this.colInvoiceAbstract.Name = "colInvoiceAbstract";
            this.colInvoiceAbstract.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceAbstract.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceAbstract.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceTotal0
            // 
            resources.ApplyResources(this.colInvoiceTotal0, "colInvoiceTotal0");
            this.colInvoiceTotal0.FieldName = "InvoiceYiFu";
            this.colInvoiceTotal0.Name = "colInvoiceTotal0";
            this.colInvoiceTotal0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colInvoiceZKE
            // 
            resources.ApplyResources(this.colInvoiceZKE, "colInvoiceZKE");
            this.colInvoiceZKE.FieldName = "InvoiceZKE";
            this.colInvoiceZKE.Name = "colInvoiceZKE";
            this.colInvoiceZKE.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colInvoiceZSE
            // 
            resources.ApplyResources(this.colInvoiceZSE, "colInvoiceZSE");
            this.colInvoiceZSE.FieldName = "InvoiceZSE";
            this.colInvoiceZSE.Name = "colInvoiceZSE";
            this.colInvoiceZSE.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colInvoiceYHE
            // 
            resources.ApplyResources(this.colInvoiceYHE, "colInvoiceYHE");
            this.colInvoiceYHE.FieldName = "InvoiceYHE";
            this.colInvoiceYHE.Name = "colInvoiceYHE";
            this.colInvoiceYHE.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceAbstract;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTotal1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceYHE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZSE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZKE;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceTotal0;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoicePayTimeLimit;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceOwed;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee0Id;
        private DevExpress.XtraGrid.Columns.GridColumn colDepotId;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}