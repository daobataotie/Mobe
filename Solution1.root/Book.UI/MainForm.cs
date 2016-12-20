using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Helper;
using DevExpress.XtraBars;
using Book.BL;
using System.Linq;

namespace Book.UI
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// ������������
        /// 
        /// </summary>
        int examingsum = 0;

        /// <summary>
        /// �������
        /// </summary>
        //    int examsum = 0;
        private const string helpfile = "erp.chm";
        private RoleManager role = new RoleManager();
        private RoleOperationManager RoleOperationManager = new RoleOperationManager();
        private OperationRoleManager OperationRoleManager = new OperationRoleManager();
        //BL.wfrecordManager wfrecordmanage = new Book.BL.wfrecordManager();
        private BL.RoleManager roleManager = new RoleManager();
        bool BG_Jurisdiction = false;

        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 18000000;
        }

        public MainForm(string url)
            : this()
        {
            //for (int i = 0; i <= this.bar2.ItemLinks.Count; i++)
            //{
            //   if(this.bar2.ItemLinks.)
            //}
            IList<Model.Role> roleList;
            roleList = this.roleManager.Select(BL.V.ActiveOperator.OperatorsId);
            int a = this.bar2.ItemLinks.Count;
            foreach (Control con in this.Controls)
            {
                //if (con is DevExpress.XtraBars.BarButtonItem)
                { }
                //  foreach( BarItem item in  bar)
            }
            //this.appUpdater1.UpdateUrl = url;
        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag == null || e.Item.Tag.ToString() == "")
                return;

            Operations.Open(e.Item.Tag.ToString(), this);
        }

        private void barButtonItemCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }
        }

        private void barButtonItemLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MdiLayout layout = (MdiLayout)Enum.Parse(typeof(MdiLayout), e.Item.Tag.ToString());
            this.LayoutMdi(layout);
        }

        private void barButtonItemCalculator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.calculatorForm == null || this.calculatorForm.IsDisposed)
                this.calculatorForm = new Tools.CalculatorForm();

            this.calculatorForm.Owner = this;
            this.calculatorForm.Show();
            this.calculatorForm.Activate();

        }

        private Form calculatorForm;

        private void MainForm_Load(object sender, EventArgs e)
        {
            IList<Model.Role> roleList = BL.V.RoleList;
            IList<Model.RoleOperation> RoleOPList = this.RoleOperationManager.SelectIsSearch(BL.V.ActiveOperator);
            //if (RoleOPList != null && RoleOPList.Count > 0)
            //{
            IList<string> tagList = new List<string>();
            foreach (Model.RoleOperation roleop in RoleOPList)
            {
                if (roleop.Operation.KeyUrl != null && !tagList.Contains(roleop.Operation.KeyUrl))
                {
                    tagList.Add(roleop.Operation.KeyUrl.ToLower());
                }
            }

            int flag = 0;
            foreach (Model.Role item in roleList)
            {
                if (item.Id == Settings.BasicData.Employees.EmployeeParameters.SYSTEMMANAGER)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                int a = this.bar2.ItemLinks.Count;
                foreach (BarItemLink bs in this.bar2.ItemLinks)
                {
                    SetBarItemLink(tagList, bs);
                }
                //控制大按钮
                foreach (BarItemLink bs in this.bar1.ItemLinks)
                {
                    if (bs.Item is BarLargeButtonItem)
                    {
                        if (bs.Item.Tag != null)
                        {
                            if (tagList.Contains(bs.Item.Tag.ToString().ToLower()))
                            {
                                bs.Item.Enabled = true;
                            }
                            else
                            {
                                bs.Item.Enabled = false;
                            }
                        }
                    }
                }
            }


            // int c = ((BarSubItem)this.bar2.ItemLinks[0].Item).ItemLinks.Count;

            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
                repositoryItemComboBox1.Items.Add(skin.SkinName);

            if (!string.IsNullOrEmpty(BL.Settings.CompanyChineseName))
                this.Text = BL.Settings.CompanyChineseName;

            this.barStaticItem3.Caption = BL.V.ActiveOperator.ToString();

            string OperatorsId = BL.V.ActiveOperator.OperatorsId.ToString();

            //  barSubItem68.Enabled = false;
            ////  IList<Model.Role> rolelist = role.Select(OperatorsId);

            //  foreach (Model.Role item in roleList)
            //  {
            //      if (item.Id == Settings.BasicData.Employees.EmployeeParameters.SYSTEMMANAGER)
            //      {
            //          barSubItem68.Enabled = true;
            //      }
            //  }

            //架构图
            //FlowChartForm f = new FlowChartForm();
            //f.MdiParent = this;
            //f.Show();


            //// ��ʾ�ҵĹ��
            // Workflow.currentwork.EditForm myworkfrom = new Book.UI.Workflow.currentwork.EditForm();
            // myworkfrom.MdiParent = this;

            // myworkfrom.Show();


            foreach (Book.UI.General.Module module in Book.UI.General.Module.Modules)
            {
                BarButtonItem b = new BarButtonItem(this.barManager1, module.ModuleText);
                b.ItemClick += new ItemClickEventHandler(b_ItemClick);
                b.Tag = module.ClassName;
                b.Name = module.ModuleName;
                this.barManager1.Items.Add(b);
                this.barSubItem32.AddItem(b);
            }

            this.BG_Jurisdiction = this.barButtonItem486.Enabled;


            #region 暂时注释判断薪资权限
            //if (flag == 0)    //非管理员
            //{
            //    Model.Role mRole = this.SelectOperatorKeyTag(BL.V.ActiveOperator);
            //    if (mRole != null)
            //    {
            //        if (!mRole.IsEmployeeBasicInfo.Value && !mRole.IsSalaryViewCalc.Value)
            //        {
            //            this.barSubItem54.Enabled = false;
            //        }
            //        else
            //        {
            //            this.barSubItem33.Enabled = false;
            //            this.barSubItem71.Enabled = false;
            //            this.barSubItem67.Enabled = false;
            //            this.barSubItem68.Enabled = false;
            //            if (mRole.IsEmployeeBasicInfo.Value)
            //            {
            //                this.barSubItem33.Enabled = true;
            //                this.barSubItem71.Enabled = true;
            //                this.barSubItem67.Enabled = true;
            //            }
            //            if (mRole.IsSalaryViewCalc.Value)
            //            {
            //                this.barSubItem33.Enabled = true;
            //                this.barSubItem71.Enabled = true;
            //                this.barSubItem67.Enabled = true;
            //                this.barSubItem68.Enabled = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        this.barSubItem54.Enabled = false;
            //    }
            //}
            #endregion


            examingsum = getexamingsum();
            if (examingsum > 0)
            {
                pictureBox1.ImageLocation = "open.bmp";
            }
            else
            {
                pictureBox1.ImageLocation = "close.bmp";
            }

            //库存预警
            if (BL.Settings.StockPromptFlag == "1")
            {
                StockPrompt form = new StockPrompt();
                if (form.productList.Count > 0)
                    form.ShowDialog();
            }
            //长期未出仓商品
            if (BL.Settings.NoDepotOutProducts == "1")
            {
                Settings.StockLimitations.NoDepotOutProducts form = new Book.UI.Settings.StockLimitations.NoDepotOutProducts();
                //if (form.dt.Rows.Count > 0)
                form.ShowDialog();
            }

            //手册预警
            //IList<Model.Role> rolist = this.OperationRoleManager.SelectRole(BL.V.ActiveOperator);
            //if (rolist.Where(w => w.Id == "004" || w.Id == "006" || w.RoleName == "报关").Count() > 0)
            if (this.BG_Jurisdiction)
            {
                CustomsClearance.BGPromptForm bGForm = new Book.UI.CustomsClearance.BGPromptForm();
                if (bGForm.dt.Rows.Count > 0)
                    bGForm.ShowDialog();
            }
        }

        private Model.Role SelectOperatorKeyTag(Model.Operators mOperators)
        {
            IList<Model.Role> roleList = this.roleManager.Select(mOperators.OperatorsId);
            if (roleList == null || roleList.Count == 0) return null;
            Model.Role mRole = new Book.Model.Role();
            mRole.IsCOCount = false;
            mRole.IsCOPrice = false;
            mRole.IsEmployeeBasicInfo = false;
            mRole.IsProductCost = false;
            mRole.IsSalaryViewCalc = false;
            mRole.IsStockCount = false;
            mRole.IsStockPrice = false;
            mRole.IsXOPrice = false;
            mRole.IsXOQuantity = false;
            foreach (Model.Role item in roleList)
            {
                if (item.IsCOCount.HasValue && item.IsCOCount.Value)
                {
                    mRole.IsCOCount = true;
                }
                if (item.IsCOPrice.HasValue && item.IsCOPrice.Value)
                {
                    mRole.IsCOPrice = true;
                }
                if (item.IsEmployeeBasicInfo.HasValue && item.IsEmployeeBasicInfo.Value)
                {
                    mRole.IsEmployeeBasicInfo = true;
                }
                if (item.IsProductCost.HasValue && item.IsProductCost.Value)
                {
                    mRole.IsProductCost = true;
                }
                if (item.IsSalaryViewCalc.HasValue && item.IsSalaryViewCalc.Value)
                {
                    mRole.IsSalaryViewCalc = true;
                }
                if (item.IsStockCount.HasValue && item.IsStockCount.Value)
                {
                    mRole.IsStockCount = true;
                }
                if (item.IsStockPrice.HasValue && item.IsStockPrice.Value)
                {
                    mRole.IsStockPrice = true;
                }
                if (item.IsXOPrice.HasValue && item.IsXOPrice.Value)
                {
                    mRole.IsXOPrice = true;
                }
                if (item.IsXOQuantity.HasValue && item.IsXOQuantity.Value)
                {
                    mRole.IsXOQuantity = true;
                }
            }
            return mRole;
        }

        private void SetBarItemLink(IList<string> tagList, BarItemLink bs)
        {
            if (bs.Item is BarButtonItem)
            {
                if (bs.Item.Tag != null)
                {
                    if (tagList.Contains(bs.Item.Tag.ToString().ToLower()))
                        bs.Item.Enabled = true;
                    else
                        if (bs.Item.Tag.ToString().Contains('-'))
                        {
                            if (tagList.Contains(bs.Item.Tag.ToString().Substring(0, bs.Item.Tag.ToString().IndexOf('-')).ToLower()))
                                bs.Item.Enabled = true;
                            else
                                bs.Item.Enabled = false;
                        }
                        else
                        {
                            bs.Item.Enabled = false;
                        }
                }
            }
            //menu
            else if (bs.Item is BarSubItem)
            {
                foreach (BarItemLink link in ((BarSubItem)bs.Item).ItemLinks)
                {
                    if (link.Item is BarButtonItem)
                    {
                        if (link.Item.Tag != null)
                        {
                            if (tagList.Contains(link.Item.Tag.ToString().ToLower()))
                                link.Item.Enabled = true;
                            else
                                if (link.Item.Tag.ToString().Contains('-'))
                                {
                                    if (tagList.Contains(link.Item.Tag.ToString().Substring(0, link.Item.Tag.ToString().IndexOf('-')).ToLower()))
                                        link.Item.Enabled = true;
                                    else
                                        link.Item.Enabled = false;
                                }
                                else
                                {
                                    link.Item.Enabled = false;
                                }
                        }
                    }
                    else
                    {
                        foreach (BarItemLink childlink in ((BarSubItem)link.Item).ItemLinks)
                        {
                            SetBarItemLink(tagList, childlink);
                        }
                    }
                }
            }
        }

        void b_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Form form = (Form)assembly.CreateInstance(e.Item.Tag.ToString());
            form.Show();
        }

        private void barManager1_HighlightedLinkChanged(object sender, DevExpress.XtraBars.HighlightedLinkChangedEventArgs e)
        {

            if (e.Link == null)
                this.barStaticItem1.Caption = "";
            else
            {
                this.barStaticItem1.Caption = e.Link.Item.Hint == "" ? e.Link.Item.Caption : e.Link.Item.Hint;
            }
        }
        /// <summary>
        /// �ܹ�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem128_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form f = null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType().FullName.EndsWith("FlowChartForm"))
                {
                    f = form;
                    break;
                }
            }
            if (f == null)
            {
                f = new FlowChartForm();
            }
            f.MdiParent = this;
            f.Show();
            f.BringToFront();
        }

        // Workflow.currentwork.PersonworkForm myworkfrom;

        /// <summary>
        /// �鿴��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Book.UI.Workflow.AuditForm myworkfrom = new Book.UI.Workflow.AuditForm();
            myworkfrom.MdiParent = this;
            myworkfrom.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            examingsum = getexamingsum();
            if (examingsum > 0)
            {
                pictureBox1.ImageLocation = "open.bmp";
            }
            else
            {
                pictureBox1.ImageLocation = "close.bmp";
            }
        }

        public int getexamingsum()
        {
            //    IList<Model.wfrecord> listall;
            //    listall = wfrecordmanage.Select();
            //    List<Model.wfrecord> Mylist = new List<Book.Model.wfrecord>();
            //    foreach (Model.wfrecord wfr in listall)
            //    {
            //        if (wfr.applyuserid == BL.V.ActiveOperator.OperatorsId)
            //        {
            //            Mylist.Add(wfr);
            //        }
            //    }
            return 0; // Mylist.Count;
        }

        private void barButtonItem190_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, helpfile);
        }

        private Form f1;

        private void barButtonItem238_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (f1 == null || f1.IsDisposed || !f1.IsHandleCreated)
            //{
            //    f1 = new Book.UI.Hr.Salary.Salaryset.CalCrystalReportForm1();
            //    f1.Show();
            //}
            //f1.BringToFront();

        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            this.defaultLookAndFeel1.LookAndFeel.SkinName = barEditItem1.EditValue.ToString();
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            //保存皮肤
            String configFile = Application.ExecutablePath + ".config";
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.Load(configFile);
            System.Xml.XmlNodeList nodes = document.SelectNodes("/configuration/userSettings/Book.UI.Properties.Settings/setting");
            foreach (System.Xml.XmlNode node in nodes)
            {
                if (node.Attributes["name"].Value == "Skin")
                {
                    node.FirstChild.InnerText = barEditItem1.EditValue.ToString();
                }
            }
            document.Save(configFile);

            //Invoices.BaseEditForm f=new Invoices.BaseEditForm();

            //f.defaultLookAndFeel1.LookAndFeel.SkinName = barEditItem1.EditValue.ToString();

            //.defaultLookAndFeel.LookAndFeel.SkinName = barEditItem1.EditValue.ToString();

        }

        private void barButtonItem309_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem318_ItemClick(object sender, ItemClickEventArgs e)
        {
            MailForm form = new MailForm();
            form.ShowDialog();
        }

        private void barButtonItemPronoteHeader1_ItemClick(object sender, ItemClickEventArgs e)
        {
            produceManager.PronoteHeader.EditForm f = new produceManager.PronoteHeader.EditForm(5);
            f.MdiParent = this;
            f.Text = Properties.Resources.GZZhiShi;
            f.Show();

        }

        private void barButtonItem327_ItemClick(object sender, ItemClickEventArgs e)
        {
            produceManager.PronoteHeader.EditForm f = new produceManager.PronoteHeader.EditForm(4);
            f.MdiParent = this;
            f.Text = Properties.Resources.ZZJiaGong;
            f.Show();

        }

        #region 冲击单据
        //BS/EN
        private void barButtonItem333_ItemClick(object sender, ItemClickEventArgs e)
        {
            produceManager.PCDoubleImpactCheck.EditForm f = new produceManager.PCDoubleImpactCheck.EditForm(1);
            f.MdiParent = this;
            f.Text = Properties.Resources.BSENcjcsd;
            f.Show();
        }

        //CSA
        private void barButtonItem332_ItemClick(object sender, ItemClickEventArgs e)
        {
            produceManager.PCDoubleImpactCheck.EditForm f = new produceManager.PCDoubleImpactCheck.EditForm(0);
            f.MdiParent = this;
            f.Text = Properties.Resources.CSAcjcsd;
            f.Show();
        }

        //AS_NZA
        private void barButtonItem335_ItemClick(object sender, ItemClickEventArgs e)
        {
            produceManager.PCDoubleImpactCheck.EditForm f = new produceManager.PCDoubleImpactCheck.EditForm(2);
            f.MdiParent = this;
            f.Text = Properties.Resources.ASNZScjcsd;
            f.Show();
        }
        #endregion

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {

        }



    }
}
