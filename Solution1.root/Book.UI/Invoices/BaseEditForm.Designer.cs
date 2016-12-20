namespace Book.UI.Invoices
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
            this.barButtonItemQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFirst = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrev = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNext = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLast = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAudit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAttachment = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
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
            this.barButtonItemNew,
            this.barButtonItemPrev,
            this.barButtonItemNext,
            this.barButtonItemPrint,
            this.barButtonItemUndo,
            this.barButtonItemUpdate,
            this.barButtonItemFirst,
            this.barButtonItemLast,
            this.barButtonItemDelete,
            this.barButtonItemQuery,
            this.barButtonItemAudit,
            this.barButtonItemAttachment});
            this.barManager1.MaxItemId = 16;
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 6;
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemUndo),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemUpdate, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemFirst, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemPrev, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemNext, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemLast, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemAudit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemAttachment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemUndo
            // 
            resources.ApplyResources(this.barButtonItemUndo, "barButtonItemUndo");
            this.barButtonItemUndo.Id = 8;
            this.barButtonItemUndo.Name = "barButtonItemUndo";
            this.barButtonItemUndo.Tag = "undo";
            this.barButtonItemUndo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemUndo_ItemClick);
            // 
            // barButtonItemSave
            // 
            resources.ApplyResources(this.barButtonItemSave, "barButtonItemSave");
            this.barButtonItemSave.Id = 2;
            this.barButtonItemSave.ImageIndex = 8;
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.Tag = "Normal";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
            // 
            // barButtonItemNew
            // 
            resources.ApplyResources(this.barButtonItemNew, "barButtonItemNew");
            this.barButtonItemNew.Id = 4;
            this.barButtonItemNew.ImageIndex = 4;
            this.barButtonItemNew.Name = "barButtonItemNew";
            this.barButtonItemNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNew_ItemClick);
            // 
            // barButtonItemUpdate
            // 
            resources.ApplyResources(this.barButtonItemUpdate, "barButtonItemUpdate");
            this.barButtonItemUpdate.Id = 9;
            this.barButtonItemUpdate.ImageIndex = 3;
            this.barButtonItemUpdate.Name = "barButtonItemUpdate";
            this.barButtonItemUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItemDelete
            // 
            resources.ApplyResources(this.barButtonItemDelete, "barButtonItemDelete");
            this.barButtonItemDelete.Id = 12;
            this.barButtonItemDelete.ImageIndex = 1;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDelete_ItemClick);
            // 
            // barButtonItemQuery
            // 
            resources.ApplyResources(this.barButtonItemQuery, "barButtonItemQuery");
            this.barButtonItemQuery.Id = 13;
            this.barButtonItemQuery.ImageIndex = 10;
            this.barButtonItemQuery.Name = "barButtonItemQuery";
            this.barButtonItemQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemQuery_ItemClick);
            // 
            // barButtonItemFirst
            // 
            resources.ApplyResources(this.barButtonItemFirst, "barButtonItemFirst");
            this.barButtonItemFirst.Id = 10;
            this.barButtonItemFirst.ImageIndex = 11;
            this.barButtonItemFirst.Name = "barButtonItemFirst";
            this.barButtonItemFirst.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemFirst_ItemClick);
            // 
            // barButtonItemPrev
            // 
            resources.ApplyResources(this.barButtonItemPrev, "barButtonItemPrev");
            this.barButtonItemPrev.Id = 5;
            this.barButtonItemPrev.ImageIndex = 6;
            this.barButtonItemPrev.Name = "barButtonItemPrev";
            this.barButtonItemPrev.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrev_ItemClick);
            // 
            // barButtonItemNext
            // 
            resources.ApplyResources(this.barButtonItemNext, "barButtonItemNext");
            this.barButtonItemNext.Id = 6;
            this.barButtonItemNext.ImageIndex = 5;
            this.barButtonItemNext.Name = "barButtonItemNext";
            this.barButtonItemNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemNext_ItemClick);
            // 
            // barButtonItemLast
            // 
            resources.ApplyResources(this.barButtonItemLast, "barButtonItemLast");
            this.barButtonItemLast.Id = 11;
            this.barButtonItemLast.ImageIndex = 12;
            this.barButtonItemLast.Name = "barButtonItemLast";
            this.barButtonItemLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLast_ItemClick);
            // 
            // barButtonItemPrint
            // 
            resources.ApplyResources(this.barButtonItemPrint, "barButtonItemPrint");
            this.barButtonItemPrint.Id = 7;
            this.barButtonItemPrint.ImageIndex = 7;
            this.barButtonItemPrint.Name = "barButtonItemPrint";
            this.barButtonItemPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrint_ItemClick);
            // 
            // barButtonItemAudit
            // 
            resources.ApplyResources(this.barButtonItemAudit, "barButtonItemAudit");
            this.barButtonItemAudit.Id = 14;
            this.barButtonItemAudit.ImageIndex = 8;
            this.barButtonItemAudit.Name = "barButtonItemAudit";
            this.barButtonItemAudit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAudit_ItemClick);
            // 
            // barButtonItemAttachment
            // 
            resources.ApplyResources(this.barButtonItemAttachment, "barButtonItemAttachment");
            this.barButtonItemAttachment.Id = 15;
            this.barButtonItemAttachment.ImageIndex = 9;
            this.barButtonItemAttachment.Name = "barButtonItemAttachment";
            this.barButtonItemAttachment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemAttachment_ItemClick);
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
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BaseEditForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseEditForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        protected DevExpress.XtraBars.Bar bar1;
        protected DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNew;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrev;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNext;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUndo;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItemQuery;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFirst;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLast;
        protected DevExpress.XtraBars.BarManager barManager1;
        public DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAudit;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAttachment;

    }
}