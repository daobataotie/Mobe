namespace Book.UI.Accounting.AtSummon
{
    partial class RO2
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.TCJieDai = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCKemuId = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCKemuName = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCNote = new DevExpress.XtraReports.UI.XRTableCell();
            this.TCMoney = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 63.5F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 79F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 79F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1844.146F, 63.5F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.TCJieDai,
            this.TCKemuId,
            this.TCKemuName,
            this.TCNote,
            this.TCMoney});
            this.xrTableRow2.Dpi = 254F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1;
            // 
            // TCJieDai
            // 
            this.TCJieDai.Dpi = 254F;
            this.TCJieDai.Name = "TCJieDai";
            this.TCJieDai.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.TCJieDai.StylePriority.UsePadding = false;
            this.TCJieDai.StylePriority.UseTextAlignment = false;
            this.TCJieDai.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.TCJieDai.Weight = 1.0416667067159817;
            // 
            // TCKemuId
            // 
            this.TCKemuId.Dpi = 254F;
            this.TCKemuId.Name = "TCKemuId";
            this.TCKemuId.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.TCKemuId.StylePriority.UsePadding = false;
            this.TCKemuId.StylePriority.UseTextAlignment = false;
            this.TCKemuId.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.TCKemuId.Weight = 1.4375003604438363;
            // 
            // TCKemuName
            // 
            this.TCKemuName.Dpi = 254F;
            this.TCKemuName.Name = "TCKemuName";
            this.TCKemuName.Text = "科目名称";
            this.TCKemuName.Weight = 1.7812498798520544;
            // 
            // TCNote
            // 
            this.TCNote.Dpi = 254F;
            this.TCNote.Name = "TCNote";
            this.TCNote.Text = "摘要";
            this.TCNote.Weight = 1.7265624999999998;
            // 
            // TCMoney
            // 
            this.TCMoney.Dpi = 254F;
            this.TCMoney.Name = "TCMoney";
            this.TCMoney.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 254F);
            this.TCMoney.StylePriority.UsePadding = false;
            this.TCMoney.StylePriority.UseTextAlignment = false;
            this.TCMoney.Text = "金额";
            this.TCMoney.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.TCMoney.Weight = 1.2734373798520546;
            // 
            // RO2
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(150, 150, 79, 79);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell TCJieDai;
        private DevExpress.XtraReports.UI.XRTableCell TCKemuId;
        private DevExpress.XtraReports.UI.XRTableCell TCKemuName;
        private DevExpress.XtraReports.UI.XRTableCell TCNote;
        private DevExpress.XtraReports.UI.XRTableCell TCMoney;
    }
}
