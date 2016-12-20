namespace Book.UI.Invoices.CO
{
    partial class ChooseDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseDetailsForm));
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceCODetailPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceCODetailQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceCODetailMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceCODetailNote = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelection,
            this.colProductId,
            this.colProduct,
            this.colInvoiceCODetailPrice,
            this.colInvoiceCODetailQuantity,
            this.colInvoiceCODetailMoney,
            this.colInvoiceCODetailNote});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colSelection
            // 
            this.colSelection.AppearanceHeader.Options.UseTextOptions = true;
            this.colSelection.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colSelection, "colSelection");
            this.colSelection.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colSelection.FieldName = "Selected";
            this.colSelection.Name = "colSelection";
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colProductId
            // 
            this.colProductId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId, "colProductId");
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.ReadOnly = true;
            // 
            // colProduct
            // 
            this.colProduct.AppearanceCell.Options.UseTextOptions = true;
            this.colProduct.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProduct.AppearanceHeader.Options.UseTextOptions = true;
            this.colProduct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProduct, "colProduct");
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.OptionsColumn.AllowEdit = false;
            this.colProduct.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceCODetailPrice
            // 
            this.colInvoiceCODetailPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceCODetailPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceCODetailPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceCODetailPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceCODetailPrice, "colInvoiceCODetailPrice");
            this.colInvoiceCODetailPrice.FieldName = "InvoiceCODetailPrice";
            this.colInvoiceCODetailPrice.Name = "colInvoiceCODetailPrice";
            // 
            // colInvoiceCODetailQuantity
            // 
            this.colInvoiceCODetailQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceCODetailQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceCODetailQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceCODetailQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceCODetailQuantity, "colInvoiceCODetailQuantity");
            this.colInvoiceCODetailQuantity.FieldName = "InvoiceCODetailQuantity";
            this.colInvoiceCODetailQuantity.Name = "colInvoiceCODetailQuantity";
            // 
            // colInvoiceCODetailMoney
            // 
            this.colInvoiceCODetailMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceCODetailMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceCODetailMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceCODetailMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceCODetailMoney, "colInvoiceCODetailMoney");
            this.colInvoiceCODetailMoney.FieldName = "InvoiceCODetailMoney";
            this.colInvoiceCODetailMoney.Name = "colInvoiceCODetailMoney";
            this.colInvoiceCODetailMoney.OptionsColumn.AllowEdit = false;
            this.colInvoiceCODetailMoney.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceCODetailNote
            // 
            this.colInvoiceCODetailNote.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceCODetailNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceCODetailNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceCODetailNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceCODetailNote, "colInvoiceCODetailNote");
            this.colInvoiceCODetailNote.FieldName = "InvoiceCODetailNote";
            this.colInvoiceCODetailNote.Name = "colInvoiceCODetailNote";
            // 
            // ChooseDetailsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButton1;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.simpleButton1);
            this.Name = "ChooseDetailsForm";
            this.Load += new System.EventHandler(this.ChooseDetailsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colSelection;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceCODetailPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceCODetailQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceCODetailMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceCODetailNote;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}