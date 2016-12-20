namespace Tool.CSManager
{
    partial class Access2003ConnectionEditForm 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Access2003ConnectionEditForm));
            this.buttonEditDataFile = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDataFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEditDataFile
            // 
            resources.ApplyResources(this.buttonEditDataFile, "buttonEditDataFile");
            this.buttonEditDataFile.BackgroundImage = null;
            this.buttonEditDataFile.EditValue = null;
            this.buttonEditDataFile.Name = "buttonEditDataFile";
            this.buttonEditDataFile.Properties.AccessibleDescription = null;
            this.buttonEditDataFile.Properties.AccessibleName = null;
            this.buttonEditDataFile.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditDataFile.Properties.AutoHeight")));
            this.buttonEditDataFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditDataFile.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("buttonEditDataFile.Properties.Mask.AutoComplete")));
            this.buttonEditDataFile.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("buttonEditDataFile.Properties.Mask.BeepOnError")));
            this.buttonEditDataFile.Properties.Mask.EditMask = resources.GetString("buttonEditDataFile.Properties.Mask.EditMask");
            this.buttonEditDataFile.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("buttonEditDataFile.Properties.Mask.IgnoreMaskBlank")));
            this.buttonEditDataFile.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("buttonEditDataFile.Properties.Mask.MaskType")));
            this.buttonEditDataFile.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("buttonEditDataFile.Properties.Mask.PlaceHolder")));
            this.buttonEditDataFile.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("buttonEditDataFile.Properties.Mask.SaveLiteral")));
            this.buttonEditDataFile.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("buttonEditDataFile.Properties.Mask.ShowPlaceHolders")));
            this.buttonEditDataFile.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("buttonEditDataFile.Properties.Mask.UseMaskAsDisplayFormat")));
            this.buttonEditDataFile.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditDataFile_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.AccessibleDescription = null;
            this.labelControl1.AccessibleName = null;
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.AccessibleDescription = null;
            this.simpleButtonOK.AccessibleName = null;
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            this.simpleButtonOK.BackgroundImage = null;
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Click += new System.EventHandler(this.simpleButtonOK_Click);
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AccessibleDescription = null;
            this.simpleButtonCancel.AccessibleName = null;
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            this.simpleButtonCancel.BackgroundImage = null;
            this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancel.Name = "simpleButtonCancel";
            // 
            // labelControl2
            // 
            this.labelControl2.AccessibleDescription = null;
            this.labelControl2.AccessibleName = null;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // textEditName
            // 
            resources.ApplyResources(this.textEditName, "textEditName");
            this.textEditName.BackgroundImage = null;
            this.textEditName.EditValue = null;
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.AccessibleDescription = null;
            this.textEditName.Properties.AccessibleName = null;
            this.textEditName.Properties.AutoHeight = ((bool)(resources.GetObject("textEditName.Properties.AutoHeight")));
            this.textEditName.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditName.Properties.Mask.AutoComplete")));
            this.textEditName.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditName.Properties.Mask.BeepOnError")));
            this.textEditName.Properties.Mask.EditMask = resources.GetString("textEditName.Properties.Mask.EditMask");
            this.textEditName.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditName.Properties.Mask.IgnoreMaskBlank")));
            this.textEditName.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditName.Properties.Mask.MaskType")));
            this.textEditName.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditName.Properties.Mask.PlaceHolder")));
            this.textEditName.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditName.Properties.Mask.SaveLiteral")));
            this.textEditName.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditName.Properties.Mask.ShowPlaceHolders")));
            this.textEditName.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditName.Properties.Mask.UseMaskAsDisplayFormat")));
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "mdb";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // AccessConnectionEditForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.simpleButtonCancel;
            this.Controls.Add(this.textEditName);
            this.Controls.Add(this.simpleButtonCancel);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.buttonEditDataFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccessConnectionEditForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDataFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit buttonEditDataFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;



    }
}