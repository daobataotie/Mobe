namespace Book.UI
{
    partial class setwcfadd
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
            this.simpleButton_Search = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textProductNameOrId = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textProductNameOrId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton_Search
            // 
            this.simpleButton_Search.Location = new System.Drawing.Point(75, 92);
            this.simpleButton_Search.Name = "simpleButton_Search";
            this.simpleButton_Search.Size = new System.Drawing.Size(90, 22);
            this.simpleButton_Search.TabIndex = 8;
            this.simpleButton_Search.Text = "確認";
            this.simpleButton_Search.Click += new System.EventHandler(this.simpleButton_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "服务器地址：http://";
            // 
            // textProductNameOrId
            // 
            this.textProductNameOrId.Location = new System.Drawing.Point(114, 44);
            this.textProductNameOrId.Name = "textProductNameOrId";
            this.textProductNameOrId.Size = new System.Drawing.Size(207, 21);
            this.textProductNameOrId.TabIndex = 10;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(213, 92);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(90, 22);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "取消";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // setwcfadd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 154);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textProductNameOrId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.simpleButton_Search);
            this.Name = "setwcfadd";
            this.Text = "远程连接地址设置";
            ((System.ComponentModel.ISupportInitialize)(this.textProductNameOrId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton_Search;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textProductNameOrId;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}