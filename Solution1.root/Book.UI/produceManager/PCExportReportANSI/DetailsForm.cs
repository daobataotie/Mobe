using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.IO;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class DetailsForm : Book.UI.Settings.BasicData.BaseEditForm
    {
        public Model.PCExportReportANSIDetail _pcExpANSIDetail = null;

        public BL.PCExportReportANSIDetailManager _pcExpANSIDetailManager = new Book.BL.PCExportReportANSIDetailManager();

        Hashtable htFormName = new Hashtable();
        string _FromPC = string.Empty;
        string _ServerSavePath = string.Empty;
        IList<MailAccessoriesHelper> _mailAccessorList = new List<MailAccessoriesHelper>();

        public DetailsForm()
        {
            InitializeComponent();

            //组成窗体集合
            htFormName.Add("MSJY", "目視檢驗");
            htFormName.Add("QXD", "清晰度");
            htFormName.Add("LJDS", "棱鏡度數&&棱鏡平衡度數");
            htFormName.Add("KJGTSL", "可見光透視率");
            htFormName.Add("ZWXTSL", "紫外線透視率");
            htFormName.Add("QMDS", "球面度數");
            htFormName.Add("SGDS", "散光度數");
            htFormName.Add("GSCJCS", "高速衝擊測試");
            htFormName.Add("YZZLZJCS", "圓錐墜落撞擊測試");
            htFormName.Add("JPCTCS", "鏡片穿透測試");
            htFormName.Add("WDCS", "霧度測試");
            htFormName.Add("NRXCS", "耐燃性測試");
            htFormName.Add("CSAGX", "屈光/光學");
            htFormName.Add("CSAPGPCL", "偏光偏差量");
            htFormName.Add("CSAQXD", "清晰度");
            htFormName.Add("CSAWDCS", "霧度測試");
            htFormName.Add("CSAKJGTSL", "可見光透視率");
            htFormName.Add("CSAGSCJCS", "高速衝擊測試");
            htFormName.Add("CEENCONSTRUCTION", "结构");
            htFormName.Add("CEENQMDS", "球面度數");
            htFormName.Add("CEENSGDS", "散光度數");
            htFormName.Add("CEENLJDS", "棱鏡度數");
            htFormName.Add("CEENZB", "座標");
            htFormName.Add("CEENTSL", "透視率");
            htFormName.Add("CEENBMPZ", "表面品質");
            htFormName.Add("CEENZSCJ", "直式衝擊");
            htFormName.Add("CEENGSCJ", "高速衝擊");
            htFormName.Add("CEENJH", "記號");
            htFormName.Add("CEENZX", "資訊");
            htFormName.Add("CEENUVCF", "UV成份");
            htFormName.Add("ASNL", "年輪");
            htFormName.Add("ASCCSL", "抽測數量");
            htFormName.Add("ASWGCS", "外觀測試");
            htFormName.Add("ASJRWDX", "加熱穩定性");
            htFormName.Add("ASZB", "座標");
            htFormName.Add("ASQMDSZSD", "球面度數折射度");
            htFormName.Add("ASWD", "霧度");
            htFormName.Add("ASZSCJSL", "中速衝擊數率");
            htFormName.Add("ASGSCJSL", "高速衝擊數率");
            htFormName.Add("ASTGCJSL", "特高速衝擊數率");
            htFormName.Add("ASCTCS", "穿透測試");
            htFormName.Add("ASNRX", "耐燃性");
            htFormName.Add("ASNSX", "耐蝕性");
            htFormName.Add("ASJH", "記號");
            htFormName.Add("JISJPWG", "鏡片外觀");
            htFormName.Add("JISJPLJD", "鏡片棱鏡度");
            htFormName.Add("JISJPQGD", "鏡片曲光度");
            htFormName.Add("JISJPSGD", "鏡片散光度");
            htFormName.Add("JISJPTGL", "鏡片透光率");
            htFormName.Add("JISJPNCCX", "鏡片耐衝擊性");
            htFormName.Add("JISJPBMNMHDK", "鏡片表面耐磨耗抵抗");
            htFormName.Add("JISJPNREX", "鏡片耐熱性");
            htFormName.Add("JISJPNSX", "鏡片耐蝕性");
            htFormName.Add("JISJPNRAX", "鏡片耐燃性");
            htFormName.Add("JISWCPWG", "完成品外觀");
            htFormName.Add("JISWCPNCCX", "完成品耐衝擊性");
            htFormName.Add("JISWCPJMXDYSY", "完成品緊密性第一試驗鏡腳拉伸");
            htFormName.Add("JISWCPJMXDESY", "完成品緊密性第二試驗中樑吊掛測試機");
            htFormName.Add("JISWCPTDQD", "完成品頭帶強度");
            htFormName.Add("JISWCPNXDX", "完成品耐消毒性");
            htFormName.Add("JISWCPGZ", "完成品構造");
            htFormName.Add("JISWCPCL", "完成品材料");
            htFormName.Add("JISWCPJHBS", "完成品記號標示");
            htFormName.Add("JISWCPBZSJH", "完成品包裝上記號");
            htFormName.Add("JISWCPSYSC", "完成品使用手冊");



            this.requireValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_PCExportReportANSIDetailId, new AA(Properties.Resources.NumsIsNotNull, this.txtPCExportReportANSIDetailId));
            this.requireValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_CheckDate, new AA(Properties.Resources.DateNotNull, this.dateEditCheckDate));
            this.requireValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_InvoiceCusXOId, new AA("客戶訂單編號,不能為空", this.txtInvoiceCusXOId));
            this.requireValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_HasCheckSum, new AA("取樣數量不能為空", this.calcHasCheckSum));
            this.requireValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_MustCheckSum, new AA("受測數量不能為空", this.calcMustCheckSum));

            this.invalidValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_HasCheckSum, new AA("取樣數量填寫有誤", this.calcHasCheckSum));
            this.invalidValueExceptions.Add(Model.PCExportReportANSIDetail.PRO_PassSum, new AA("合格數量填寫有誤", this.calcPassSum));

            this.nccCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.nccEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();

            //取得服务器附件存储地址
            if (System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None) != null && System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"] != null)
            {
                this._ServerSavePath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None).AppSettings.Settings["accessoriesPath"].Value;
            }

            this.action = "view";
            this.bindingSourceUnit.DataSource = (new BL.ProductUnitManager()).Select();
        }

        public DetailsForm(string formname)
            : this()
        {
            this._FromPC = formname.Substring(formname.IndexOf('-') + 1);
            this.Text = htFormName[this._FromPC].ToString();
        }
        int sign = 0;
        public DetailsForm(Model.PCExportReportANSIDetail model)
            : this()
        {
            if (model != null)
            {
                this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(model.PCExportReportANSIDetailId);
                if (this._pcExpANSIDetail != null)
                    sign = 1;
                this.Text = model.Type;
                this.action = "view";
            }
        }

        protected override void AddNew()
        {
            this._pcExpANSIDetail = new Book.Model.PCExportReportANSIDetail();
            this._pcExpANSIDetail.PCExportReportANSIDetailId = this._pcExpANSIDetailManager.GetId();
            this._pcExpANSIDetail.FromPC = this._FromPC.Substring(this._FromPC.IndexOf('-') + 1);
            this._pcExpANSIDetail.CheckDate = DateTime.Now;
            this._pcExpANSIDetail.Employee = BL.V.ActiveOperator.Employee;
            this._pcExpANSIDetail.MustCheckSum = 0;

            this._mailAccessorList.Clear();
        }

        protected override void Delete()
        {
            if (this._pcExpANSIDetail == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            //首先删除附件

            this._pcExpANSIDetailManager.Delete(this._pcExpANSIDetail.PCExportReportANSIDetailId);

            this._pcExpANSIDetail = this._pcExpANSIDetailManager.mGetNext(this._pcExpANSIDetail.InsertTime.Value, this._FromPC);
            if (this._pcExpANSIDetail == null)
            {
                this._pcExpANSIDetail = this._pcExpANSIDetailManager.mGetLast(this._FromPC);
            }
        }

        protected override void MoveLast()
        {
            if (sign == 1)
            {
                sign = 0;
                return;
            }
            this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(this._pcExpANSIDetailManager.mGetLast(this._FromPC) == null ? "" : this._pcExpANSIDetailManager.mGetLast(this._FromPC).PCExportReportANSIDetailId);
        }

        protected override void MoveFirst()
        {
            this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(this._pcExpANSIDetailManager.mGetFirst(this._FromPC) == null ? "" : this._pcExpANSIDetailManager.mGetFirst(this._FromPC).PCExportReportANSIDetailId);
        }

        protected override void MovePrev()
        {
            Model.PCExportReportANSIDetail pcExpANSIDeta = this._pcExpANSIDetailManager.mGetPrev(this._pcExpANSIDetail.InsertTime.Value, this._FromPC);
            if (pcExpANSIDeta == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(pcExpANSIDeta.PCExportReportANSIDetailId);
        }

        protected override void MoveNext()
        {
            Model.PCExportReportANSIDetail pcExpANSIDeta = this._pcExpANSIDetailManager.mGetNext(this._pcExpANSIDetail.InsertTime.Value, this._FromPC);
            if (pcExpANSIDeta == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(pcExpANSIDeta.PCExportReportANSIDetailId);
        }

        protected override bool HasRows()
        {
            return this._pcExpANSIDetailManager.mHasRows(this._FromPC);
        }

        protected override bool HasRowsNext()
        {
            return this._pcExpANSIDetailManager.mHasRowsAfter(this._pcExpANSIDetail, this._FromPC);
        }

        protected override bool HasRowsPrev()
        {
            return this._pcExpANSIDetailManager.mHasRowsBefore(this._pcExpANSIDetail, this._FromPC);
        }

        public override void Refresh()
        {
            if (this._pcExpANSIDetail == null)
            {
                this.AddNew();
                this.action = "insert";
            }

            this.txtPCExportReportANSIDetailId.Text = this._pcExpANSIDetail.PCExportReportANSIDetailId;
            this.txtInvoiceCusXOId.Text = this._pcExpANSIDetail.InvoiceCusXOId;
            this.dateEditCheckDate.EditValue = this._pcExpANSIDetail.CheckDate.HasValue ? this._pcExpANSIDetail.CheckDate.Value : DateTime.Now;
            this.calcMustCheckSum.EditValue = this._pcExpANSIDetail.MustCheckSum.HasValue ? this._pcExpANSIDetail.MustCheckSum.Value : 0;
            this.calcInvoiceQuantity.EditValue = this._pcExpANSIDetail.InvoiceQuantity.HasValue ? this._pcExpANSIDetail.InvoiceQuantity.Value : 0;
            this.calcPassSum.EditValue = this._pcExpANSIDetail.PassSum.HasValue ? this._pcExpANSIDetail.PassSum.Value : 0;
            this.calcHasCheckSum.EditValue = this._pcExpANSIDetail.HasCheckSum.HasValue ? this._pcExpANSIDetail.HasCheckSum.Value : 0;
            this.calcFaildSum.EditValue = this.calcHasCheckSum.Value - this.calcPassSum.Value;
            this.btnEditProduct.EditValue = this._pcExpANSIDetail.Product;
            this.nccEmployee.EditValue = this._pcExpANSIDetail.Employee;
            this.nccCustomer.EditValue = this._pcExpANSIDetail.Customer;
            this.txtRemark.Text = this._pcExpANSIDetail.Remark;
            this.lookUpEditUnit.EditValue = this._pcExpANSIDetail.ProductUnitId;
            //组合附件源 附件记录形式:FileName|FileName *文件名=>'\FilePath\FileName'
            //if (!string.IsNullOrEmpty(this._pcExpANSIDetail.accessoriesList))
            //{
            //    string[] accessories = this._pcExpANSIDetail.accessoriesList.Split('|');
            //    foreach (string acc in accessories)
            //    {
            //        MailAccessoriesHelper mah = new MailAccessoriesHelper();
            //        //mah.FileName = this._ServerSavePath + acc;  
            //        mah.FileFullName = acc;
            //        FileInfo fi = new FileInfo(this._ServerSavePath + acc);
            //        mah.FileSize = fi.Length.ToString();
            //        this._mailAccessorList.Add(mah);
            //    }
            //}

            this._mailAccessorList.Clear();     //清除所有项目

            //查看附件,通过主键路径查找,如果没有则数据为空
            if (Directory.Exists(this._ServerSavePath + "\\" + this._pcExpANSIDetail.PCExportReportANSIDetailId))
            {
                string[] filenames = Directory.GetFiles(this._ServerSavePath + "\\" + this._pcExpANSIDetail.PCExportReportANSIDetailId);
                foreach (string fn in filenames)
                {
                    MailAccessoriesHelper mah = new MailAccessoriesHelper();
                    mah.FileName = fn.Substring(fn.LastIndexOf("\\") + 1);  //这里直接使用文件名.不包含路径
                    mah.FileFullName = fn;
                    FileInfo fi = new FileInfo(mah.FileFullName);
                    mah.FileSize = fi.Length.ToString();
                    this._mailAccessorList.Add(mah);
                }
            }

            this.bindingSource1.DataSource = this._mailAccessorList;
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barBtnSearch.Enabled = false;
                    this.barBtnChooseInvoiceXO.Enabled = true;
                    break;
                case "update":
                    this.barBtnSearch.Enabled = false;
                    this.barBtnChooseInvoiceXO.Enabled = true;
                    break;
                case "view":
                    this.barBtnSearch.Enabled = true;
                    this.barBtnChooseInvoiceXO.Enabled = false;
                    break;
            }

            this.txtPCExportReportANSIDetailId.Enabled = false;
            this.calcMustCheckSum.Enabled = false;          //受测数量不能修改,计算带出
            this.calcFaildSum.Enabled = false;              //不合格数量通过合格数量算出,不需要修改,不需要存储
            this.calcInvoiceQuantity.Enabled = false;
            this.txtInvoiceCusXOId.Enabled = false;
            this.nccCustomer.Enabled = false;
            this.btnEditProduct.Enabled = false;
            //this.barManager1.Items["barButtonitemAllAttachment"].Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //this.bar1.ItemLinks[11].Visible = false;
        }

        protected override void Save()
        {
            this._pcExpANSIDetail.PCExportReportANSIDetailId = this.txtPCExportReportANSIDetailId.Text;
            this._pcExpANSIDetail.CheckDate = this.dateEditCheckDate.DateTime;
            this._pcExpANSIDetail.InvoiceCusXOId = this.txtInvoiceCusXOId.Text;
            this._pcExpANSIDetail.Remark = this.txtRemark.Text;
            this._pcExpANSIDetail.FromPC = this._FromPC;

            this._pcExpANSIDetail.MustCheckSum = this.calcMustCheckSum.EditValue == null ? 0 : int.Parse(this.calcMustCheckSum.EditValue.ToString());
            this._pcExpANSIDetail.HasCheckSum = this.calcHasCheckSum.EditValue == null ? 0 : int.Parse(this.calcHasCheckSum.EditValue.ToString());
            this._pcExpANSIDetail.PassSum = this.calcPassSum.EditValue == null ? 0 : int.Parse(this.calcPassSum.EditValue.ToString());
            this._pcExpANSIDetail.InvoiceQuantity = this.calcInvoiceQuantity.EditValue == null ? 0 : double.Parse(this.calcInvoiceQuantity.EditValue.ToString());

            this._pcExpANSIDetail.Customer = this.nccCustomer.EditValue as Model.Customer;
            if (this._pcExpANSIDetail.Customer != null)
                this._pcExpANSIDetail.CustomerId = this._pcExpANSIDetail.Customer.CustomerId;

            this._pcExpANSIDetail.Employee = this.nccEmployee.EditValue as Model.Employee;
            if (this._pcExpANSIDetail.Employee != null)
                this._pcExpANSIDetail.EmployeeId = this._pcExpANSIDetail.Employee.EmployeeId;

            this._pcExpANSIDetail.Product = this.btnEditProduct.EditValue as Model.Product;
            if (this._pcExpANSIDetail.Product != null)
                this._pcExpANSIDetail.ProductId = this._pcExpANSIDetail.Product.ProductId;

            if (this._mailAccessorList == null || this._mailAccessorList.Count == 0)
                this._pcExpANSIDetail.accessoriesList = string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (MailAccessoriesHelper mah in this._mailAccessorList)
                    sb.Append(mah.FileFullName + "|");      //这里使用全路径名称.如果是新上传那就是本地路径,若是已有文件,就是服务器路径

                this._pcExpANSIDetail.accessoriesList = sb.ToString().Substring(0, sb.ToString().Length - 1);
            }

            //if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            //    return;
            this._pcExpANSIDetail.TestType = JudgeType(this._FromPC);
            this._pcExpANSIDetail.ProductUnitId = this.lookUpEditUnit.EditValue == null ? null : this.lookUpEditUnit.EditValue.ToString();
            switch (this.action)
            {
                case "insert":
                    this._pcExpANSIDetailManager.Insert(this._pcExpANSIDetail);
                    break;
                case "update":
                    this._pcExpANSIDetailManager.Update(this._pcExpANSIDetail);
                    break;
            }
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DetailsList f = new DetailsList(this._FromPC);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.PCExportReportANSIDetail currentModel = f.SelectItem as Model.PCExportReportANSIDetail;
                if (currentModel != null)
                {
                    this._pcExpANSIDetail = currentModel;
                    this._pcExpANSIDetail = this._pcExpANSIDetailManager.Get(this._pcExpANSIDetail.PCExportReportANSIDetailId);
                    this.Refresh();
                }
            }
            f.Dispose();
            GC.Collect();
        }

        private void barBtnChooseInvoiceXO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            createProduce.EditForm f = new Book.UI.produceManager.createProduce.EditForm();
            if (f.ShowDialog(this) != DialogResult.OK)
                return;
            if (f.SelectList == null || f.SelectList.Count == 0)
                return;
            Model.InvoiceXODetail xod = f.SelectList[0];

            this.calcInvoiceQuantity.EditValue = xod.InvoiceXODetailQuantity;
            this.btnEditProduct.EditValue = xod.Product;
            this.nccCustomer.EditValue = xod.Invoice.xocustomer;
            this.txtInvoiceCusXOId.Text = xod.Invoice.CustomerInvoiceXOId;
            this._pcExpANSIDetail.InvoiceCusXOId = xod.Invoice.CustomerInvoiceXOId;
            this._pcExpANSIDetail.ProductId = xod.ProductId;


            if (!xod.InvoiceXODetailQuantity.HasValue)
                xod.InvoiceXODetailBeenQuantity = 0;

            int mInvoiceXoDetailQuantity = int.Parse(xod.InvoiceXODetailQuantity.Value.ToString());

            double mMustCheck = 0;

            if (mInvoiceXoDetailQuantity < 500)
                mMustCheck = 1;
            else
                mMustCheck = mInvoiceXoDetailQuantity % 500 == 0 ? mInvoiceXoDetailQuantity / 500 : mInvoiceXoDetailQuantity / 500 + 1;
            this.calcMustCheckSum.EditValue = mMustCheck > 12 ? 12 : mMustCheck;    //受测数量1/500订单数量,上限12个,无条件进位
            this.calcHasCheckSum.EditValue = this.calcMustCheckSum.EditValue;       //默认取样数量等于受测数量
            this.calcFaildSum.EditValue = this.calcHasCheckSum.EditValue;

            //if (this._pcExpANSIDetail.InvoiceCusXOId == null || this._pcExpANSIDetail.ProductId == null || this._pcExpANSIDetail.FromPC == null)
            //{
            //    this.calcHasCheckSum.EditValue = this.calcMustCheckSum.EditValue;
            //}
            //else
            //{
            //    int HasCheck = this._pcExpANSIDetailManager.HasCheckSum(this._pcExpANSIDetail.InvoiceCusXOId, this._pcExpANSIDetail.ProductId, this._pcExpANSIDetail.FromPC);
            //    this.calcHasCheckSum.Properties.MaxValue = this._pcExpANSIDetail.MustCheckSum.Value - HasCheck;
            //    this.calcHasCheckSum.Properties.MinValue = 0;
            //    this.calcHasCheckSum.EditValue = mMustCheck - HasCheck;
            //}
        }

        //点击附件名称,打开附件
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            MailAccessoriesHelper mah = this.bindingSource1.Current as MailAccessoriesHelper;
            System.Diagnostics.Process.Start(mah.FileFullName);
        }

        private void calcPassSum_EditValueChanged(object sender, EventArgs e)
        {
            int hassum = int.Parse(this.calcHasCheckSum.EditValue == null ? "0" : this.calcHasCheckSum.EditValue.ToString());
            int passsum = int.Parse(this.calcPassSum.EditValue == null ? "0" : this.calcPassSum.EditValue.ToString());
            this.calcFaildSum.EditValue = hassum - passsum;
        }

        //判断是那种类型的
        private string JudgeType(string str)
        {
            ArrayList items_ANSI = new ArrayList { "MSJY", "QXD", "LJDS", "KJGTSL", "ZWXTSL", "QMDS", "SGDS", "GSCJCS", "YZZLZJCS", "JPCTCS", "WDCS", "NRXCS" };
            ArrayList items_CSA = new ArrayList { "CSAGX", "CSAQXD", "CSAPGPCL", "CSAWDCS", "CSAKJGTSL", "CSAGSCJCS" };
            ArrayList items_CEEN = new ArrayList { "CEENCONSTRUCTION", "CEENQMDS", "CEENSGDS", "CEENLJDS", "CEENZB", "CEENTSL", "CEENBMPZ", "CEENZSCJ", "CEENGSCJ", "CEENJH", "CEENZX", "CEENUVCF" };
            ArrayList items_AS = new ArrayList { "ASNL", "ASCCSL", "ASWGCS", "ASJRWDX", "ASZB", "ASQMDSZSD", "ASWD", "ASZSCJSL", "ASGSCJSL", "ASTGCJSL", "ASCTCS", "ASNRX", "ASNSX", "ASJH" };
            ArrayList items_JIS = new ArrayList { "JISJPWG", "JISJPLJD", "JISJPQGD", "JISJPSGD", "JISJPTGL", "JISJPNCCX", "JISJPBMNMHDK", "JISJPNREX", "JISJPNSX", "JISJPNRAX", "JISWCPWG", "JISWCPNCCX", "JISWCPJMXDYSY", "JISWCPJMXDESY", "JISWCPTDQD", "JISWCPNXDX", "JISWCPGZ", "JISWCPCL", "JISWCPJHBS", "JISWCPBZSJH", "JISWCPSYSC" };

            if (items_ANSI.Contains(str))
                return "ANSI";
            else if (items_CSA.Contains(str))
                return "CSA";
            else if (items_CEEN.Contains(str))
                return "CEEN";
            else if (items_AS.Contains(str))
                return "AS";
            else if (items_JIS.Contains(str))
                return "JIS";
            return str;
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.PCExportReportANSIDetail.PRO_PCExportReportANSIDetailId;
        }

        protected override string tableCode()
        {
            return "PCExportReportANSIDetail" + "," + this._pcExpANSIDetail.PCExportReportANSIDetailId;
        }
        #endregion
    }

    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class MailAccessoriesHelper
    {
        public Image FileImage
        {
            get
            {
                string extendname = this.FileName.Substring(this.FileName.LastIndexOf(".") + 1).ToLower();
                if (extendname.Equals("txt", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_txt;
                if (extendname.Equals("doc", StringComparison.OrdinalIgnoreCase) || extendname.Equals("docx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Word;
                if (extendname.Equals("ppt", StringComparison.OrdinalIgnoreCase) || extendname.Equals("pptx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_PowerPoint;
                if (extendname.Equals("mdb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("accdb", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Access;
                if (extendname.Equals("xls", StringComparison.OrdinalIgnoreCase) || extendname.Equals("xlsx", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_Excel;
                if (extendname.Equals("mp3", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wma", StringComparison.OrdinalIgnoreCase) || extendname.Equals("wmv", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_muisc;
                if (extendname.Equals("avi", StringComparison.OrdinalIgnoreCase) || extendname.Equals("rmvb", StringComparison.OrdinalIgnoreCase) || extendname.Equals("flv", StringComparison.OrdinalIgnoreCase) || extendname.Equals("mkv", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_realplay;
                if (extendname.Equals("jpg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("jpeg", StringComparison.OrdinalIgnoreCase) || extendname.Equals("bmp", StringComparison.OrdinalIgnoreCase) || extendname.Equals("gif", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_pic;
                if (extendname.Equals("pdf", StringComparison.OrdinalIgnoreCase))
                    return Properties.Resources.img_pdf;

                return Properties.Resources.img_default;
            }
        }

        private string _FileFullName;

        public string FileFullName
        {
            get { return _FileFullName; }
            set { _FileFullName = value; }
        }

        private string _FileName;

        /// <summary>
        /// 绑定的要显示的单文件名称 {文件名.后缀名}
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        private string _FileSize;

        public string FileSize
        {
            get
            {
                string Result = string.Empty;
                double mSize = double.Parse(string.IsNullOrEmpty(this._FileSize) ? "0" : this._FileSize) / 1024;
                if (mSize < 1024)
                    Result = string.Format("{0:f}", mSize) + " kb";
                else
                    Result = string.Format("{0:f}", mSize / 1024) + " M";
                return Result;
            }
            set { _FileSize = value; }
        }
    }
}