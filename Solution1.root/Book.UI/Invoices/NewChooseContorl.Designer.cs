namespace Book.UI.Invoices
{
    partial class NewChooseContorl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonEditId = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControlName = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEditId
            // 
            this.buttonEditId.Location = new System.Drawing.Point(0, 0);
            this.buttonEditId.Name = "buttonEditId";
            this.buttonEditId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null)});
            this.buttonEditId.Properties.ReadOnly = true;
            this.buttonEditId.Size = new System.Drawing.Size(86, 21);
            this.buttonEditId.TabIndex = 0;
            this.buttonEditId.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditId_ButtonClick);
            this.buttonEditId.Leave += new System.EventHandler(this.buttonEditId_Leave);
            this.buttonEditId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonEditId_KeyDown);
            // 
            // labelControlName
            // 
            this.labelControlName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelControlName.Location = new System.Drawing.Point(92, 3);
            this.labelControlName.Name = "labelControlName";
            this.labelControlName.Size = new System.Drawing.Size(0, 14);
            this.labelControlName.TabIndex = 1;
            // 
            // NewChooseContorl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.labelControlName);
            this.Controls.Add(this.buttonEditId);
            this.Name = "NewChooseContorl";
            this.Size = new System.Drawing.Size(192, 24);
            this.Load += new System.EventHandler(this.NewChooseContorl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit buttonEditId;
        private DevExpress.XtraEditors.LabelControl labelControlName;
    }
}
