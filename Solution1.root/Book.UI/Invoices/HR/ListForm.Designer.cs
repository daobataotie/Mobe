namespace Book.UI.Invoices.HR
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colInvoiceDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee0Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceNote = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleDescription");
            this.gridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleName");
            this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControl1.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControl1.EmbeddedNavigator.Anchor")));
            this.gridControl1.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImage")));
            this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControl1.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode")));
            this.gridControl1.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation")));
            this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
            this.gridControl1.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType")));
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colInvoiceDate,
            this.colInvoiceId,
            this.colCompanyId,
            this.colEmployee0Id,
            this.colInvoiceNote});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceDate.AppearanceCell.GradientMode")));
            this.colInvoiceDate.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceDate.AppearanceCell.Image")));
            this.colInvoiceDate.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInvoiceDate.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceDate.AppearanceHeader.GradientMode")));
            this.colInvoiceDate.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceDate.AppearanceHeader.Image")));
            this.colInvoiceDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colInvoiceDate, "colInvoiceDate");
            this.colInvoiceDate.FieldName = "InvoiceDate";
            this.colInvoiceDate.Name = "colInvoiceDate";
            // 
            // colInvoiceId
            // 
            this.colInvoiceId.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceId.AppearanceCell.GradientMode")));
            this.colInvoiceId.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceId.AppearanceCell.Image")));
            this.colInvoiceId.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceId.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceId.AppearanceHeader.GradientMode")));
            this.colInvoiceId.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceId.AppearanceHeader.Image")));
            this.colInvoiceId.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceId, "colInvoiceId");
            this.colInvoiceId.FieldName = "InvoiceId";
            this.colInvoiceId.Name = "colInvoiceId";
            this.colInvoiceId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colCompanyId
            // 
            this.colCompanyId.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colCompanyId.AppearanceCell.GradientMode")));
            this.colCompanyId.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colCompanyId.AppearanceCell.Image")));
            this.colCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyId.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colCompanyId.AppearanceHeader.GradientMode")));
            this.colCompanyId.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colCompanyId.AppearanceHeader.Image")));
            this.colCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyId, "colCompanyId");
            this.colCompanyId.FieldName = "Company";
            this.colCompanyId.Name = "colCompanyId";
            // 
            // colEmployee0Id
            // 
            this.colEmployee0Id.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colEmployee0Id.AppearanceCell.GradientMode")));
            this.colEmployee0Id.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colEmployee0Id.AppearanceCell.Image")));
            this.colEmployee0Id.AppearanceCell.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEmployee0Id.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colEmployee0Id.AppearanceHeader.GradientMode")));
            this.colEmployee0Id.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colEmployee0Id.AppearanceHeader.Image")));
            this.colEmployee0Id.AppearanceHeader.Options.UseTextOptions = true;
            this.colEmployee0Id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colEmployee0Id, "colEmployee0Id");
            this.colEmployee0Id.FieldName = "Employee0";
            this.colEmployee0Id.Name = "colEmployee0Id";
            // 
            // colInvoiceNote
            // 
            this.colInvoiceNote.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceNote.AppearanceCell.GradientMode")));
            this.colInvoiceNote.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceNote.AppearanceCell.Image")));
            this.colInvoiceNote.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceNote.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceNote.AppearanceHeader.GradientMode")));
            this.colInvoiceNote.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceNote.AppearanceHeader.Image")));
            this.colInvoiceNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceNote, "colInvoiceNote");
            this.colInvoiceNote.FieldName = "InvoiceNote";
            this.colInvoiceNote.Name = "colInvoiceNote";
            this.colInvoiceNote.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceNote.OptionsFilter.AllowFilter = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceId;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee0Id;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceNote;
    }
}