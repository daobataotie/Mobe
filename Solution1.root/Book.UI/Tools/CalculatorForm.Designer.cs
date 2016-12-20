namespace Book.UI.Tools
{
    partial class CalculatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculatorForm));
            this.memoEditResult = new DevExpress.XtraEditors.MemoEdit();
            this.textEditExpression = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExpression.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEditResult
            // 
            resources.ApplyResources(this.memoEditResult, "memoEditResult");
            this.memoEditResult.BackgroundImage = null;
            this.memoEditResult.EditValue = null;
            this.memoEditResult.Name = "memoEditResult";
            this.memoEditResult.Properties.AccessibleDescription = null;
            this.memoEditResult.Properties.AccessibleName = null;
            this.memoEditResult.Properties.ReadOnly = true;
            // 
            // textEditExpression
            // 
            resources.ApplyResources(this.textEditExpression, "textEditExpression");
            this.textEditExpression.BackgroundImage = null;
            this.textEditExpression.EditValue = null;
            this.textEditExpression.Name = "textEditExpression";
            this.textEditExpression.Properties.AccessibleDescription = null;
            this.textEditExpression.Properties.AccessibleName = null;
            this.textEditExpression.Properties.AutoHeight = ((bool)(resources.GetObject("textEditExpression.Properties.AutoHeight")));
            this.textEditExpression.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditExpression.Properties.Mask.AutoComplete")));
            this.textEditExpression.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditExpression.Properties.Mask.BeepOnError")));
            this.textEditExpression.Properties.Mask.EditMask = resources.GetString("textEditExpression.Properties.Mask.EditMask");
            this.textEditExpression.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditExpression.Properties.Mask.IgnoreMaskBlank")));
            this.textEditExpression.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditExpression.Properties.Mask.MaskType")));
            this.textEditExpression.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditExpression.Properties.Mask.PlaceHolder")));
            this.textEditExpression.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditExpression.Properties.Mask.SaveLiteral")));
            this.textEditExpression.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditExpression.Properties.Mask.ShowPlaceHolders")));
            this.textEditExpression.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditExpression.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEditExpression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEditExpression_KeyDown);
            // 
            // CalculatorForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.textEditExpression);
            this.Controls.Add(this.memoEditResult);
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculatorForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.memoEditResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditExpression.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEditResult;
        private DevExpress.XtraEditors.TextEdit textEditExpression;
    }
}