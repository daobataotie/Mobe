namespace Book.UI.Query
{
    partial class ConditionStockByProductForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionStockByProductForm));
            this.label1 = new System.Windows.Forms.Label();
            this.EditProduct = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.EditProduct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.AccessibleDescription = null;
            this.simpleButtonOK.AccessibleName = null;
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            this.simpleButtonOK.BackgroundImage = null;
            // 
            // simpleButtonCancel
            // 
            this.simpleButtonCancel.AccessibleDescription = null;
            this.simpleButtonCancel.AccessibleName = null;
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            this.simpleButtonCancel.BackgroundImage = null;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // EditProduct
            // 
            resources.ApplyResources(this.EditProduct, "EditProduct");
            this.EditProduct.BackgroundImage = null;
            this.EditProduct.EditValue = null;
            this.EditProduct.Name = "EditProduct";
            this.EditProduct.Properties.AccessibleDescription = null;
            this.EditProduct.Properties.AccessibleName = null;
            this.EditProduct.Properties.AutoHeight = ((bool)(resources.GetObject("EditProduct.Properties.AutoHeight")));
            this.EditProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.EditProduct.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("EditProduct.Properties.Mask.AutoComplete")));
            this.EditProduct.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("EditProduct.Properties.Mask.BeepOnError")));
            this.EditProduct.Properties.Mask.EditMask = resources.GetString("EditProduct.Properties.Mask.EditMask");
            this.EditProduct.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("EditProduct.Properties.Mask.IgnoreMaskBlank")));
            this.EditProduct.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("EditProduct.Properties.Mask.MaskType")));
            this.EditProduct.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("EditProduct.Properties.Mask.PlaceHolder")));
            this.EditProduct.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("EditProduct.Properties.Mask.SaveLiteral")));
            this.EditProduct.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("EditProduct.Properties.Mask.ShowPlaceHolders")));
            this.EditProduct.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("EditProduct.Properties.Mask.UseMaskAsDisplayFormat")));
            this.EditProduct.Click += new System.EventHandler(this.buttonEdit1_Click);
            // 
            // ConditionStockByProductForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EditProduct);
            this.Icon = null;
            this.Name = "ConditionStockByProductForm";
            this.Controls.SetChildIndex(this.EditProduct, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            ((System.ComponentModel.ISupportInitialize)(this.EditProduct.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ButtonEdit EditProduct;
    }
}