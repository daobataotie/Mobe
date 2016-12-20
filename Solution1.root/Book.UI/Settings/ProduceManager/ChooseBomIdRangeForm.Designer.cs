namespace Book.UI.Settings.ProduceManager
{
    partial class ChooseBomIdRangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseBomIdRangeForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lookUpEditEndBomId = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSourceBomIds = new System.Windows.Forms.BindingSource(this.components);
            this.lookUpEditStartBomId = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButton_Sure = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEndBomId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBomIds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStartBomId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lookUpEditEndBomId);
            this.layoutControl1.Controls.Add(this.lookUpEditStartBomId);
            this.layoutControl1.Controls.Add(this.simpleButton_Sure);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(851, 160, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lookUpEditEndBomId
            // 
            resources.ApplyResources(this.lookUpEditEndBomId, "lookUpEditEndBomId");
            this.lookUpEditEndBomId.Name = "lookUpEditEndBomId";
            this.lookUpEditEndBomId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditEndBomId.Properties.Columns"), resources.GetString("lookUpEditEndBomId.Properties.Columns1"), ((int)(resources.GetObject("lookUpEditEndBomId.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lookUpEditEndBomId.Properties.Columns3"))), resources.GetString("lookUpEditEndBomId.Properties.Columns4"), ((bool)(resources.GetObject("lookUpEditEndBomId.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lookUpEditEndBomId.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditEndBomId.Properties.Columns7"), resources.GetString("lookUpEditEndBomId.Properties.Columns8"))});
            this.lookUpEditEndBomId.Properties.DataSource = this.bindingSourceBomIds;
            this.lookUpEditEndBomId.Properties.DisplayMember = "Id";
            this.lookUpEditEndBomId.Properties.DropDownRows = 10;
            this.lookUpEditEndBomId.Properties.NullText = resources.GetString("lookUpEditEndBomId.Properties.NullText");
            this.lookUpEditEndBomId.Properties.ValueMember = "BomId";
            this.lookUpEditEndBomId.StyleController = this.layoutControl1;
            // 
            // lookUpEditStartBomId
            // 
            resources.ApplyResources(this.lookUpEditStartBomId, "lookUpEditStartBomId");
            this.lookUpEditStartBomId.Name = "lookUpEditStartBomId";
            this.lookUpEditStartBomId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditStartBomId.Properties.Columns"), resources.GetString("lookUpEditStartBomId.Properties.Columns1"), ((int)(resources.GetObject("lookUpEditStartBomId.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lookUpEditStartBomId.Properties.Columns3"))), resources.GetString("lookUpEditStartBomId.Properties.Columns4"), ((bool)(resources.GetObject("lookUpEditStartBomId.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lookUpEditStartBomId.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditStartBomId.Properties.Columns7"), resources.GetString("lookUpEditStartBomId.Properties.Columns8"))});
            this.lookUpEditStartBomId.Properties.DataSource = this.bindingSourceBomIds;
            this.lookUpEditStartBomId.Properties.DisplayMember = "Id";
            this.lookUpEditStartBomId.Properties.DropDownRows = 10;
            this.lookUpEditStartBomId.Properties.NullText = resources.GetString("lookUpEditStartBomId.Properties.NullText");
            this.lookUpEditStartBomId.Properties.ValueMember = "BomId";
            this.lookUpEditStartBomId.StyleController = this.layoutControl1;
            // 
            // simpleButton_Sure
            // 
            resources.ApplyResources(this.simpleButton_Sure, "simpleButton_Sure");
            this.simpleButton_Sure.Name = "simpleButton_Sure";
            this.simpleButton_Sure.StyleController = this.layoutControl1;
            this.simpleButton_Sure.Click += new System.EventHandler(this.simpleButton_Sure_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(421, 258);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton_Sure;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(80, 117);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(238, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 143);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(401, 95);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(401, 67);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(318, 67);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(83, 76);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 67);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(80, 76);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lookUpEditStartBomId;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(80, 67);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(238, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lookUpEditEndBomId;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(80, 92);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(238, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(84, 14);
            // 
            // ChooseBomIdRangeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseBomIdRangeForm";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditEndBomId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBomIds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditStartBomId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton_Sure;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditEndBomId;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditStartBomId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource bindingSourceBomIds;
    }
}