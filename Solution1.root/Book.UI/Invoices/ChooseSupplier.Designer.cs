namespace Book.UI.Invoices
{
    partial class ChooseSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseSupplier));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.colCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCompanyName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName0 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompanyId,
            this.colCompanyName1,
            this.colCompanyName0,
            this.colCompanyDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButtonCancel
            // 
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            // 
            // simpleButtonOK
            // 
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            // 
            // simpleButtonNew
            // 
            resources.ApplyResources(this.simpleButtonNew, "simpleButtonNew");
            // 
            // colCompanyId
            // 
            this.colCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colCompanyId, "colCompanyId");
            this.colCompanyId.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colCompanyId.FieldName = "Id";
            this.colCompanyId.Name = "colCompanyId";
            this.colCompanyId.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemButtonEdit1
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), resources.GetString("repositoryItemButtonEdit1.Buttons1"), ((int)(resources.GetObject("repositoryItemButtonEdit1.Buttons2"))), ((bool)(resources.GetObject("repositoryItemButtonEdit1.Buttons3"))), ((bool)(resources.GetObject("repositoryItemButtonEdit1.Buttons4"))), ((bool)(resources.GetObject("repositoryItemButtonEdit1.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("repositoryItemButtonEdit1.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("repositoryItemButtonEdit1.Buttons8"), ((object)(resources.GetObject("repositoryItemButtonEdit1.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("repositoryItemButtonEdit1.Buttons10"))), ((bool)(resources.GetObject("repositoryItemButtonEdit1.Buttons11"))))});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // colCompanyName1
            // 
            this.colCompanyName1.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyName1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCompanyName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colCompanyName1, "colCompanyName1");
            this.colCompanyName1.FieldName = "SupplierFullName";
            this.colCompanyName1.Name = "colCompanyName1";
            this.colCompanyName1.OptionsColumn.AllowEdit = false;
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
            // 
            // colCompanyName0
            // 
            this.colCompanyName0.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyName0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCompanyName0.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyName0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colCompanyName0, "colCompanyName0");
            this.colCompanyName0.FieldName = "SupplierShortName";
            this.colCompanyName0.Name = "colCompanyName0";
            this.colCompanyName0.OptionsColumn.AllowEdit = false;
            // 
            // ChooseSupplier
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChooseSupplier";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName1;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName0;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;

    }
}