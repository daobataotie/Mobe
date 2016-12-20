namespace Book.UI.Settings.BasicData
{
    partial class BaseEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseEditForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemUndo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNew = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFirst = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrev = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNext = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLast = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAudit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonitemAllAttachment = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemSave,
            this.barButtonItemUndo,
            this.barButtonItemNew,
            this.barButtonItemUpdate,
            this.barButtonItemPrev,
            this.barButtonItemNext,
            this.barButtonItemPrint,
            this.barButtonItemDelete,
            this.barButtonItemFirst,
            this.barButtonItemLast,
            this.barButtonItemAudit,
            this.barButtonitemAllAttachment});
            this.barManager1.MaxItemId = 15;
            this.barManager1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barManager1_ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 6;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemUndo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemUpdate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFirst, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemPrev, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemNext, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemLast, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemAudit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonitemAllAttachment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemUndo
            // 
            resources.ApplyResources(this.barButtonItemUndo, "barButtonItemUndo");
            this.barButtonItemUndo.Id = 0;
            this.barButtonItemUndo.ImageIndex = 15;
            this.barButtonItemUndo.Name = "barButtonItemUndo";
            this.barButtonItemUndo.Tag = "undo";
            // 
            // barButtonItemSave
            // 
            resources.ApplyResources(this.barButtonItemSave, "barButtonItemSave");
            this.barButtonItemSave.Id = 1;
            this.barButtonItemSave.ImageIndex = 0;
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.Tag = "save";
            // 
            // barButtonItemNew
            // 
            resources.ApplyResources(this.barButtonItemNew, "barButtonItemNew");
            this.barButtonItemNew.Id = 3;
            this.barButtonItemNew.ImageIndex = 5;
            this.barButtonItemNew.Name = "barButtonItemNew";
            this.barButtonItemNew.Tag = "new";
            // 
            // barButtonItemUpdate
            // 
            resources.ApplyResources(this.barButtonItemUpdate, "barButtonItemUpdate");
            this.barButtonItemUpdate.Id = 4;
            this.barButtonItemUpdate.ImageIndex = 4;
            this.barButtonItemUpdate.Name = "barButtonItemUpdate";
            this.barButtonItemUpdate.Tag = "update";
            // 
            // barButtonItemDelete
            // 
            resources.ApplyResources(this.barButtonItemDelete, "barButtonItemDelete");
            this.barButtonItemDelete.Id = 9;
            this.barButtonItemDelete.ImageIndex = 2;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.Tag = "delete";
            // 
            // barButtonItemFirst
            // 
            resources.ApplyResources(this.barButtonItemFirst, "barButtonItemFirst");
            this.barButtonItemFirst.Id = 11;
            this.barButtonItemFirst.ImageIndex = 10;
            this.barButtonItemFirst.Name = "barButtonItemFirst";
            this.barButtonItemFirst.Tag = "first";
            // 
            // barButtonItemPrev
            // 
            resources.ApplyResources(this.barButtonItemPrev, "barButtonItemPrev");
            this.barButtonItemPrev.Id = 5;
            this.barButtonItemPrev.ImageIndex = 7;
            this.barButtonItemPrev.Name = "barButtonItemPrev";
            this.barButtonItemPrev.Tag = "prev";
            // 
            // barButtonItemNext
            // 
            resources.ApplyResources(this.barButtonItemNext, "barButtonItemNext");
            this.barButtonItemNext.Id = 6;
            this.barButtonItemNext.ImageIndex = 6;
            this.barButtonItemNext.Name = "barButtonItemNext";
            this.barButtonItemNext.Tag = "next";
            // 
            // barButtonItemLast
            // 
            resources.ApplyResources(this.barButtonItemLast, "barButtonItemLast");
            this.barButtonItemLast.Id = 12;
            this.barButtonItemLast.ImageIndex = 11;
            this.barButtonItemLast.Name = "barButtonItemLast";
            this.barButtonItemLast.Tag = "last";
            // 
            // barButtonItemAudit
            // 
            resources.ApplyResources(this.barButtonItemAudit, "barButtonItemAudit");
            this.barButtonItemAudit.Id = 13;
            this.barButtonItemAudit.ImageIndex = 0;
            this.barButtonItemAudit.Name = "barButtonItemAudit";
            this.barButtonItemAudit.Tag = "Audit";
            // 
            // barButtonItemPrint
            // 
            resources.ApplyResources(this.barButtonItemPrint, "barButtonItemPrint");
            this.barButtonItemPrint.Id = 8;
            this.barButtonItemPrint.ImageIndex = 8;
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.Tag = "print";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // barButtonitemAllAttachment
            // 
            resources.ApplyResources(this.barButtonitemAllAttachment, "barButtonitemAllAttachment");
            this.barButtonitemAllAttachment.Id = 14;
            this.barButtonitemAllAttachment.ImageIndex = 13;
            this.barButtonitemAllAttachment.Name = "barButtonitemAllAttachment";
            this.barButtonitemAllAttachment.Tag = "Attachment";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // imageCollection1
            // 
            resources.ApplyResources(this.imageCollection1, "imageCollection1");
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // BaseEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BaseEditForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BaseEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barButtonItemUndo;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNew;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrev;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNext;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLast;
        protected DevExpress.XtraBars.BarManager barManager1;
        protected DevExpress.Utils.ImageCollection imageCollection1;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemFirst;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAudit;
        protected DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonitemAllAttachment;


    }
}