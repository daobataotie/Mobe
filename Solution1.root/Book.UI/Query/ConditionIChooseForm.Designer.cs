namespace Book.UI.Query
{
    partial class ConditionIChooseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionIChooseForm));
            this.comboBoxEditEndId = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditStartId = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditEndId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStartId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxEditEndId
            // 
            this.comboBoxEditEndId.EnterMoveNextControl = true;
            resources.ApplyResources(this.comboBoxEditEndId, "comboBoxEditEndId");
            this.comboBoxEditEndId.Name = "comboBoxEditEndId";
            this.comboBoxEditEndId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditEndId.Properties.Buttons"))))});
            this.comboBoxEditEndId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // comboBoxEditStartId
            // 
            this.comboBoxEditStartId.EnterMoveNextControl = true;
            resources.ApplyResources(this.comboBoxEditStartId, "comboBoxEditStartId");
            this.comboBoxEditStartId.Name = "comboBoxEditStartId";
            this.comboBoxEditStartId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditStartId.Properties.Buttons"))))});
            this.comboBoxEditStartId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // ConditionIChooseForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxEditEndId);
            this.Controls.Add(this.comboBoxEditStartId);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "ConditionIChooseForm";
            this.Load += new System.EventHandler(this.ConditionIChooseForm_Load);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.comboBoxEditStartId, 0);
            this.Controls.SetChildIndex(this.comboBoxEditEndId, 0);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditEndId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditStartId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditEndId;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStartId;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}