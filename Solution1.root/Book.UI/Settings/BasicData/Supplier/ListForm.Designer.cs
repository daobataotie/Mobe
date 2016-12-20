namespace Book.UI.Settings.BasicData.Supplier
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CompanyName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.bindingSourceSupplierCategory = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplierCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "SupplierPhone1";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "SupplierFax";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "Id";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // CompanyName1
            // 
            resources.ApplyResources(this.CompanyName1, "CompanyName1");
            this.CompanyName1.FieldName = "SupplierFullName";
            this.CompanyName1.Name = "CompanyName1";
            this.CompanyName1.OptionsColumn.AllowEdit = false;
            this.CompanyName1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.CompanyName1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.CompanyName1.OptionsColumn.ReadOnly = true;
            this.CompanyName1.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyName0
            // 
            resources.ApplyResources(this.colCompanyName0, "colCompanyName0");
            this.colCompanyName0.FieldName = "SupplierShortName";
            this.colCompanyName0.Name = "colCompanyName0";
            this.colCompanyName0.OptionsColumn.AllowEdit = false;
            this.colCompanyName0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyName0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyName0.OptionsColumn.ReadOnly = true;
            this.colCompanyName0.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyDescription
            // 
            this.colCompanyDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCompanyDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colCompanyDescription, "colCompanyDescription");
            this.colCompanyDescription.FieldName = "Remark";
            this.colCompanyDescription.Name = "colCompanyDescription";
            this.colCompanyDescription.OptionsColumn.AllowEdit = false;
            this.colCompanyDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyDescription.OptionsColumn.ReadOnly = true;
            this.colCompanyDescription.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "SupplierMobile";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.DataSource = this.bindingSourceSupplierCategory;
            resources.ApplyResources(this.listBoxControl1, "listBoxControl1");
            this.listBoxControl1.Name = "listBoxControl1";
            // 
            // bindingSourceSupplierCategory
            // 
            this.bindingSourceSupplierCategory.CurrentChanged += new System.EventHandler(this.bindingSourceSupplierCategory_CurrentChanged);
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxControl1);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.Controls.SetChildIndex(this.listBoxControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSupplierCategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn CompanyName1;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName0;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private System.Windows.Forms.BindingSource bindingSourceSupplierCategory;

    }
}