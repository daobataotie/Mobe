namespace Book.UI.Invoices
{
    partial class ChooseIncomingKindForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseIncomingKindForm));
            this.colIncomingKindName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIncomingKindDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIncomingKindName,
            this.colIncomingKindDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
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
            // colIncomingKindName
            // 
            this.colIncomingKindName.AppearanceCell.Options.UseTextOptions = true;
            this.colIncomingKindName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIncomingKindName.AppearanceHeader.Options.UseTextOptions = true;
            this.colIncomingKindName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colIncomingKindName, "colIncomingKindName");
            this.colIncomingKindName.FieldName = "IncomingKindName";
            this.colIncomingKindName.Name = "colIncomingKindName";
            this.colIncomingKindName.OptionsColumn.AllowEdit = false;
            this.colIncomingKindName.OptionsColumn.ReadOnly = true;
            // 
            // colIncomingKindDescription
            // 
            this.colIncomingKindDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colIncomingKindDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colIncomingKindDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colIncomingKindDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colIncomingKindDescription, "colIncomingKindDescription");
            this.colIncomingKindDescription.FieldName = "IncomingKindDescription";
            this.colIncomingKindDescription.Name = "colIncomingKindDescription";
            this.colIncomingKindDescription.OptionsColumn.AllowEdit = false;
            this.colIncomingKindDescription.OptionsColumn.ReadOnly = true;
            // 
            // ChooseIncomingKindForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseIncomingKindForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colIncomingKindName;
        private DevExpress.XtraGrid.Columns.GridColumn colIncomingKindDescription;
    }
}